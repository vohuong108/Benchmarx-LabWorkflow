using CommandLine;
using NMF.Models;
using NMF.Models.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Commandline
{
    [Verb("generate", HelpText = "Generates input models and change sequences")]
    internal class GenerateModelVerb : VerbBase
    {
        [Value(0, HelpText = "The number of samples", Required = true)]
        public int NumberOfSamples { get; set; }

        [Option('n', "replicate-assay", HelpText = "Gets the number of assay replicas", Default = 1)]
        public int ReplicateAssay { get; set; }

        [Value(1, HelpText = "The output directory path", Required = true)]
        public string Output { get; set; }

        [Value(2, HelpText = "The number of change sequences to generate", Required = false)]
        public int ChangeSequences { get; set; }

        [Option('d', "pDist", Default = 0.05)]
        public double ProbabilityOfDistributeFailure { get; set; }

        [Option('r', "pReagent", Default = 0.03)]
        public double ProbabilityOfAddReagentFailure { get; set; }

        [Option('i', "pIncubate", Default = 0)]
        public double ProbabilityOfIncubateFailure { get; set; }

        [Option('w', "pWash", Default = 0.01)]
        public double ProbabilityOfWashFailure { get; set; }

        [Option('s', "pSample", Default = 0.0)]
        public double ProbabilityOfNewSample { get; set; }

        protected override void ExecuteCore()
        {
            var jobRequest = new JobRequest();
            var conjugate = new Reagent { Name = "Conjugate" };
            var substrate = new Reagent { Name = "Substrate" };
            jobRequest.Assay = new Assay
            {
                Name = "AbstractElisa",
                Reagents =
                {
                    conjugate,
                    substrate
                },
                Steps =
                {
                    new DistributeSample
                    {
                        Volume = 100,
                        Id = "AddSample"
                    },
                    new Incubate
                    {
                        Duration = 40,
                        Temperature = 310.15,
                        Id = "BindAntibodies"
                    },
                    new Wash
                    {
                        Id = "WashAfterAntibodiesBound"
                    },
                    new AddReagent
                    {
                        Volume = 100,
                        Reagent = conjugate,
                        Id = "AddConjugate"
                    },
                    new Incubate
                    {
                        Duration = 40,
                        Temperature = 310.15,
                        Id = "BindConjugate"
                    },
                    new Wash
                    {
                        Id = "WashConjugate"
                    },
                    new AddReagent
                    {
                        Volume = 100,
                        Reagent = substrate,
                        Id = "AddSubstrate"
                    },
                    new Incubate
                    {
                        Duration = 20,
                        Temperature = 293.15,
                        Id = "WaitForColorReaction"
                    }
                }
            };
            for(int i = 2; i <= ReplicateAssay; i++)
            {
                jobRequest.Assay.Steps.Add(
                new DistributeSample
                {
                    Volume = 100,
                    Id = "AddSample" + i
                } );
                jobRequest.Assay.Steps.Add( new Incubate
                {
                    Duration = 40,
                    Temperature = 310.15,
                    Id = "BindAntibodies" + i
                } );
                jobRequest.Assay.Steps.Add( new Wash
                {
                    Id = "WashAfterAntibodiesBound" + i
                } );
                jobRequest.Assay.Steps.Add( new AddReagent
                {
                    Volume = 100,
                    Reagent = conjugate,
                    Id = "AddConjugate" + i
                } );
                jobRequest.Assay.Steps.Add( new Incubate
                {
                    Duration = 40,
                    Temperature = 310.15,
                    Id = "BindConjugate" + i
                } );
                jobRequest.Assay.Steps.Add( new Wash
                {
                    Id = "WashConjugate" + i
                } );
                jobRequest.Assay.Steps.Add( new AddReagent
                {
                    Volume = 100,
                    Reagent = substrate,
                    Id = "AddSubstrate" + i
                } );
                jobRequest.Assay.Steps.Add( new Incubate
                {
                    Duration = 20,
                    Temperature = 293.15,
                    Id = "WaitForColorReaction" + i
                } );
            }
            for(int i = 1; i < jobRequest.Assay.Steps.Count; i++)
            {
                jobRequest.Assay.Steps[i].Previous = jobRequest.Assay.Steps[i - 1];
            }
            for(int i = 1; i <= NumberOfSamples; i++)
            {
                jobRequest.Samples.Add( new Sample
                {
                    SampleID = $"Sample{i:0000}",
                    State = SampleState.Waiting
                } );
            }

            var modelName = Path.GetFileName( Output );
            var model = new Model
            {
                RootElements = { jobRequest },
                ModelUri = new Uri( $"http://www.transformation-tool-contest.eu/ttc21/labAutomation/models/{modelName}" )
            };

            MetaRepository.Instance.Serializer.Serialize( model, Path.Combine(Output, "initial.xmi") );

            var random = new Random();
            var platesDecided = -jobRequest.Assay.Steps.Count + 1;
            
            for(int i = 1; i <= ChangeSequences && platesDecided * 96 <= NumberOfSamples; i++)
            {
                var sb = new StringBuilder();
                for(int jobIndex = 0; jobIndex < jobRequest.Assay.Steps.Count; jobIndex++)
                {
                    var plate = platesDecided + jobRequest.Assay.Steps.Count - jobIndex;
                    if (plate > 0 && (plate - 1) * 96 <= NumberOfSamples)
                    {
                        var step = jobRequest.Assay.Steps[jobIndex];
                        sb.AppendLine( $"{step.Id}_Plate{plate:00}_{GetState( step, random )}" );
                    }
                }
                var newSamples = (int)Math.Ceiling( random.NextDouble() * ProbabilityOfNewSample * 12 );
                for(int s = 0; s < newSamples; s++)
                {
                    sb.AppendLine( $"NewSample_Sample{NumberOfSamples + s:0000}" );
                }
                NumberOfSamples += newSamples;
                var path = Path.Combine( Output, $"change{i:00}.txt" );
                File.WriteAllText( path, sb.ToString() );
                platesDecided++;
            }
        }

        private string GetState(IProtocolStep step, Random random)
        {
            return step switch
            {
                AddReagent addReagent => new string( Enumerable.Range( 0, 96 ).Select( _ => random.NextDouble() < ProbabilityOfAddReagentFailure ? 'F' : 'S' ).ToArray() ),
                DistributeSample distribute => new string( Enumerable.Range( 0, 96 ).Select( _ => random.NextDouble() < ProbabilityOfDistributeFailure ? 'F' : 'S' ).ToArray() ),
                Incubate incubate => random.NextDouble() < ProbabilityOfIncubateFailure ? "F" : "S",
                Wash wash => random.NextDouble() < ProbabilityOfWashFailure ? "F" : "S",
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
