using NMF.Expressions.Linq;
using NMF.Models;
using NMF.Synchronizations;
using NMF.Transformations;
using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    internal class IncrementalSolution : ISolution
    {
        private IJobRequest _jobRequest;
        private IJobCollection _jobCollection;
        private ISynchronizationContext _context;
        private ProtocolSynchronization _synchronization;

        public Func<Model> ComputeChanges( string[] changes )
        {
            SolutionChangeHelper.CalculateActualChanges( _jobCollection, changes, out var stateChanges, out var tipChanges, out var newSamples );

            return () => PropagateChanges( stateChanges, tipChanges, newSamples );
        }

        private Model PropagateChanges( Dictionary<IJob, JobStatus> stateChanges, Dictionary<ITipLiquidTransfer, JobStatus> tipChanges, List<string> newSamples )
        {
            foreach(var pair in stateChanges)
            {
                pair.Key.State = pair.Value;
            }

            foreach(var pair in tipChanges)
            {
                pair.Key.Status = pair.Value;
            }

            foreach(var sample in newSamples)
            {
                _jobRequest.Samples.Add( new Sample
                {
                    SampleID = sample,
                    State = SampleState.Waiting
                } );
            }

            return _jobCollection.Model;
        }

        public Model Initial()
        {
            _context = new SynchronizationContext( _synchronization, SynchronizationDirection.LeftToRight, ChangePropagationMode.TwoWay );

            _synchronization.InitializeContext( _jobRequest.Samples, _context );
            _synchronization.Synchronize( ref _jobRequest, ref _jobCollection, _context );

            return new Model
            {
                RootElements =
                {
                    _jobCollection
                }
            };
        }

        public void Load( JobRequest jobRequest )
        {
            _jobRequest = jobRequest;
            _synchronization = new ProtocolSynchronization();
            _synchronization.Initialize();
        }
    }
}
