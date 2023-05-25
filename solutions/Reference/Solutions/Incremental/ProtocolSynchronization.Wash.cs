using NMF.Expressions.Linq;
using NMF.Synchronizations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    internal partial class ProtocolSynchronization
    {

        public class WashToJobsRule : MicroplateProtocolStepRule<Wash, WashToWashJob, WashJob>
        {
        }

        public class WashToWashJob : MicroplateJobRule<Wash, WashJob>
        {
            protected override Expression<Func<WashJob, IMicroplate>> MicroplateProperty => wash => wash.Microplate;

            public override void DeclareSynchronization()
            {
                base.DeclareSynchronization();

                SynchronizeManyLeftToRightOnly(
                    tuple => tuple.Item2.Columns.SelectMany( c => c.Samples.Where( s => s.Sample.State != SampleState.Error ).Select( s => s.Well ) ),
                    wash => wash.Cavities );
            }
        }
    }
}
