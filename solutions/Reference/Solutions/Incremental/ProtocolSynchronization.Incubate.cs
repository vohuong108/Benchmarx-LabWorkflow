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
        public class IncubateToJobsRule : MicroplateProtocolStepRule<Incubate, IncubateToIncubateJob, IncubateJob>
        {
        }

        public class IncubateToIncubateJob : MicroplateJobRule<Incubate, IncubateJob>
        {
            protected override Expression<Func<IncubateJob, IMicroplate>> MicroplateProperty => incubate => incubate.Microplate;

            public override void DeclareSynchronization()
            {
                base.DeclareSynchronization();

                SynchronizeLeftToRightOnly( tuple => tuple.Item1.Duration, incubate => incubate.Duration );
                SynchronizeLeftToRightOnly( tuple => tuple.Item1.Temperature, incubate => incubate.Temperature );
            }
        }
    }
}
