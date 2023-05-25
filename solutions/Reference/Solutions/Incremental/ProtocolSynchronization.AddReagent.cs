using NMF.Expressions.Linq;
using NMF.Synchronizations;
using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    internal partial class ProtocolSynchronization
    {
        public class AddReagentToJobsRule : ProtocolStepRule<AddReagent>
        {
            public override void DeclareSynchronization()
            {
                MarkInstantiatingFor( SyncRule<ProtocolStepToJobsRule>() );

                SynchronizeManyLeftToRightOnly(
                    SyncRule<AddReagentLiquidTransferToLiquidTransfer>(),
                    ( step, context ) => GetPlates( context )
                                         .SelectMany(p => p.Columns, (plate, column) => new AddReagentLiquidTransfer(column, plate, step))
                                         .Where(transfer => transfer.Column.AnyValidSample.Value),
                    ( jobsOfStep, _ ) => jobsOfStep.Jobs.OfType<IJob, LiquidTransferJob>() );
            }
        }

        public class AddReagentLiquidTransferToLiquidTransfer : SynchronizationRule<AddReagentLiquidTransfer, LiquidTransferJob>
        {
            public override void DeclareSynchronization()
            {
                SynchronizeLeftToRightOnly( SyncRule<ReagentToTrough>(), step => step.AddReagent.Reagent, liquidTransfer => liquidTransfer.Source as Trough );
                SynchronizeLeftToRightOnly( SyncRule<ProcessPlateToMicroplate>(), step => step.Plate, liquidTransfer => liquidTransfer.Target as Microplate );

                SynchronizeManyLeftToRightOnly( SyncRule<AddReagentTipToTipTransfer>(),
                    step => step.Column.Samples
                            .Where( s => s.Sample.State != SampleState.Error )
                            .Select( s => new AddReagentTip( step, s ) ),
                    liquidTransfer => new TipCollection( liquidTransfer.Tips ) );

                SynchronizeManyLeftToRightOnly(
                    ( step, _ ) => step.Column.AllSamples,
                    ( liquidTransfer, context ) => GetAffectedSamples( context, liquidTransfer ) );
            }
        }

        public class AddReagentTipToTipTransfer : SynchronizationRule<AddReagentTip, ITipLiquidTransfer>
        {
            public override void DeclareSynchronization()
            {
                SynchronizeLeftToRightOnly( well => well.AddReagent.Volume, transfer => transfer.Volume );
                SynchronizeLeftToRightOnly( well => well.TargetWell.Well, transfer => transfer.TargetCavityIndex );

                SynchronizeRightToLeftOnly( well => IsSampleFailed( well.TargetWell.Sample ), transfer => transfer.Status == JobStatus.Failed );
            }
        }

        public class AddReagentLiquidTransfer
        {
            public AddReagentLiquidTransfer( ProcessColumn column, ProcessPlate plate, AddReagent addReagent )
            {
                Column = column;
                Plate = plate;
                AddReagent = addReagent;
            }

            public ProcessColumn Column { get; set; }
            public ProcessPlate Plate { get; set; }

            public AddReagent AddReagent { get; }

            public override string ToString()
            {
                return $"[AddReagent {AddReagent.Id} to column {Column.Column}]";
            }
        }

        public class AddReagentTip
        {
            public AddReagentTip( AddReagentLiquidTransfer addReagent, ProcessWell processWell )
            {
                AddReagent = addReagent.AddReagent;
                TargetWell = processWell;
            }

            public AddReagent AddReagent { get; }
            public ProcessWell TargetWell { get; }

            public override string ToString()
            {
                return $"[AddReagent {AddReagent.Id} to well {TargetWell.Well}]";
            }
        }
    }
}
