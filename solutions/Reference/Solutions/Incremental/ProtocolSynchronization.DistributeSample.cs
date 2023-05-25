using NMF.Expressions.Linq;
using NMF.Synchronizations;
using NMF.Transformations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    internal partial class ProtocolSynchronization
    {
        public class DistributeSampleToJobsRule : ProtocolStepRule<DistributeSample>
        {
            public override void DeclareSynchronization()
            {
                MarkInstantiatingFor( SyncRule<ProtocolStepToJobsRule>() );

                SynchronizeManyLeftToRightOnly(
                    SyncRule<DistributeSampleLiquidTransferToLiquidTransfer>(),
                    ( step, context ) => GetPlates( context )
                                         .SelectMany( plate => plate.Columns, ( plate, column ) => new DistributeSampleLiquidTransfer( plate, column, GetSourceTube( context, column ), step ) )
                                         .Where( transfer => transfer.Column.AnyValidSample.Value ),
                    ( jobsOfStep, _ ) => jobsOfStep.Jobs.OfType<IJob, LiquidTransferJob>() );
            }

            private static Tubes GetSourceTube( ITransformationContext context, ProcessColumn column )
            {
                return GetTubes( context ).AsEnumerable().FirstOrDefault( t => t.Samples.AsEnumerable().Any( s => column.Samples.AsEnumerable().Any( s2 => s.Sample == s2.Sample ) ) );
            }
        }

        public class DistributeSampleLiquidTransferToLiquidTransfer : SynchronizationRule<DistributeSampleLiquidTransfer, LiquidTransferJob>
        {
            public override void DeclareSynchronization()
            {
                SynchronizeLeftToRightOnly( SyncRule<SamplesToTubeRunner>(), distribute => distribute.TubeRunner, liquidTransfer => liquidTransfer.Source as TubeRunner );
                SynchronizeLeftToRightOnly( SyncRule<ProcessPlateToMicroplate>(), step => step.Plate, liquidTransfer => liquidTransfer.Target as Microplate );

                SynchronizeManyLeftToRightOnly( SyncRule<DispenseWellsToTipTransfer>(),
                    step => step.Column.Samples
                            .Where(s => s.Sample.State != SampleState.Error)
                            .Select(s => new DistributeSampleTip( step, s )),
                    liquidTransfer => new TipCollection( liquidTransfer.Tips ) );

                SynchronizeManyLeftToRightOnly(
                    ( step, _ ) => step.Column.AllSamples,
                    ( liquidTransfer, context ) => GetAffectedSamples( context, liquidTransfer ) );
            }
        }

        public class DispenseWellsToTipTransfer : SynchronizationRule<DistributeSampleTip, ITipLiquidTransfer>
        {
            public override void DeclareSynchronization()
            {
                SynchronizeLeftToRightOnly( well => well.DistributeSample.Volume, transfer => transfer.Volume );
                SynchronizeLeftToRightOnly( well => well.SourceWell.Well, transfer => transfer.SourceCavityIndex );
                SynchronizeLeftToRightOnly( well => well.TargetWell.Well, transfer => transfer.TargetCavityIndex );


                SynchronizeRightToLeftOnly( well => IsSampleFailed( well.TargetWell.Sample ), transfer => transfer.Status == JobStatus.Failed );
            }
        }

        public class DistributeSampleLiquidTransfer
        {
            public DistributeSampleLiquidTransfer( ProcessPlate plate, ProcessColumn column, Tubes tubeRunner, DistributeSample distribute )
            {
                Plate = plate;
                Column = column;
                TubeRunner = tubeRunner;
                Distribute = distribute;
            }

            public ProcessPlate Plate { get; set; }
            public ProcessColumn Column { get; }

            public Tubes TubeRunner { get; set; }

            public DistributeSample Distribute { get; }
        }

        public class DistributeSampleTip
        {
            public DistributeSampleTip( DistributeSampleLiquidTransfer distributeSample, ProcessWell targetWell )
            {
                DistributeSample = distributeSample.Distribute;
                TargetWell = targetWell;
                SourceWell = distributeSample.TubeRunner.Samples.AsEnumerable().FirstOrDefault( w => w.Sample == targetWell.Sample );
            }

            public DistributeSample DistributeSample
            {
                get;
            }

            public ProcessWell TargetWell { get; }

            public ProcessWell SourceWell { get; }
        }
    }
}
