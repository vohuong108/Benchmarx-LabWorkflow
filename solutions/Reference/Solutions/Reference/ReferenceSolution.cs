using NMF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    internal class ReferenceSolution : ISolution
    {
        private JobRequest _jobRequest;
        private Model _resultModel;
        private IJobCollection _result;
        private LocationRepository _locationRepository;
        private readonly Dictionary<ISample, List<IJob>> _affectedJobsPerSample = new Dictionary<ISample, List<IJob>>();

        private int _currentCavity;
        private IMicroplate _currentPlate;
        private int _currentTube;
        private ITubeRunner _currentTubeRunner;
        private int _platesCreated;
        private int _tubeRunnersCreated;

        public Func<Model> ComputeChanges( string[] changes )
        {
            SolutionChangeHelper.CalculateActualChanges( _result, changes, out var stateChanges, out var tipChanges, out var newSamples );

            return () => PropagateChanges( stateChanges, tipChanges, newSamples );
        }
        private Model PropagateChanges(Dictionary<IJob, JobStatus> stateChanges, Dictionary<ITipLiquidTransfer, JobStatus> tipChanges, List<string> newSamples)
        {
            foreach(var pair in stateChanges)
            {
                pair.Key.State = pair.Value;
            }

            foreach(var pair in tipChanges)
            {
                pair.Key.Status = pair.Value;
            }

            var failedSamples = new List<ISample>();
            foreach(var job in stateChanges.Keys)
            {
                failedSamples.AddRange( job.GetFailedSamples( _locationRepository ) );
            }

            foreach(var sample in failedSamples.Distinct())
            {
                sample.State = SampleState.Error;
                foreach(var job in _affectedJobsPerSample[sample])
                {
                    if (job.State == JobStatus.Planned)
                    {
                        job.GetProcessedSamples().Remove( sample );
                        if(job.GetProcessedSamples().Count == 0)
                        {
                            job.Delete();
                        }
                        else if(job is LiquidTransferJob liquidTransfer)
                        {
                            var processingLocation = _locationRepository.LocateSampleProcessing( sample );
                            var tip = liquidTransfer.Tips.FirstOrDefault( t => t.TargetCavityIndex == processingLocation.Cavity );
                            if(tip != null)
                            {
                                tip.Delete();
                            }
                        }
                    }
                }
            }

            var newSamplesCreated = newSamples.Select( id => new Sample { SampleID = id, State = SampleState.Waiting } ).ToList();

            foreach(var sample in newSamplesCreated)
            {
                AddSample( sample );
            }

            AddSteps( newSamplesCreated );

            return _resultModel;
        }

        public Model Initial()
        {
            _currentCavity = 95;
            _currentPlate = null;
            _currentTube = 15;
            _currentTubeRunner = null;

            _platesCreated = 0;
            _tubeRunnersCreated = 0;

            _result = new JobCollection.JobCollection();
            _locationRepository = new LocationRepository();

            foreach(var sample in _jobRequest.Samples)
            {
                AddSample( sample );
            }

            foreach(var reagent in _jobRequest.Assay.Reagents)
            {
                AddReagent( reagent );
            }

            AddSteps(_jobRequest.Samples);

            _resultModel = new Model
            {
                RootElements = { _result },
                ModelUri = new Uri( "temp:result" )
            };

            return _resultModel;
        }

        private void AddSteps( IEnumerable<ISample> samples )
        {
            var lastSteps = null as IEnumerable<IJob>;
            foreach(var step in _jobRequest.Assay.Steps)
            {
                var createdSteps = step.CreateJobs( samples, _locationRepository, ( sample, job ) =>
                {
                    job.GetProcessedSamples().Add( sample );
                    _affectedJobsPerSample[sample].Add( job );
                } ).ToList();
                foreach(var addedStep in createdSteps)
                {
                    _result.Jobs.Add( addedStep );
                    if(lastSteps != null)
                    {
                        foreach(var lastStepDependent in lastSteps.Where( j => j.GetProcessedSamples().Intersect( addedStep.GetProcessedSamples() ).Any() ))
                        {
                            addedStep.Previous.Add( lastStepDependent );
                        }
                    }
                }
                lastSteps = createdSteps;
            }
        }

        private void AddReagent( IReagent reagent )
        {
            var reagentTrough = new Trough
            {
                Name = reagent.Name
            };
            _result.Labware.Add( reagentTrough );
            _locationRepository.SetOrigin( reagent, reagentTrough );
        }

        private void AddSample( ISample sample )
        {
            _affectedJobsPerSample.Add( sample, new List<IJob>() );
            _currentCavity++;
            _currentTube++;
            if(_currentCavity == 96)
            {
                _currentCavity = 0;
                _platesCreated++;
                _currentPlate = new Microplate()
                {
                    Name = $"Plate{_platesCreated:00}"
                };
                _result.Labware.Add( _currentPlate );
            }
            if(_currentTube == 16)
            {
                _currentTube = 0;
                _tubeRunnersCreated++;
                _currentTubeRunner = new TubeRunner
                {
                    Name = $"TubeRunner{_tubeRunnersCreated:00}"
                };
                _result.Labware.Add( _currentTubeRunner );
            }
            _currentTubeRunner.Barcodes.Add( sample.SampleID );
            _locationRepository.SetProcessingLocation( sample, _currentPlate, _currentCavity );
            _locationRepository.SetOrigin( sample, _currentTubeRunner, _currentTube );
        }

        public void Load( JobRequest jobRequest )
        {
            _jobRequest = jobRequest;
        }
    }
}
