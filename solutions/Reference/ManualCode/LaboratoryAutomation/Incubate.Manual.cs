using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.LaboratoryAutomation
{
    public partial class Incubate : IProtocolStep
    {
        public override IEnumerable<IJob> CreateJobs( IEnumerable<ISample> samples, ILocationRepository locationRepository, Action<ISample, IJob> trace )
        {
            return from sample in samples
                   let processingLocation = locationRepository.LocateSampleProcessing( sample )
                   group sample by processingLocation.Plate into incubateGroup
                   select TraceAll( new IncubateJob
                   {
                       ProtocolStepName = Id,
                       Duration = Duration,
                       Temperature = Temperature,
                       Microplate = incubateGroup.Key
                   }, trace, incubateGroup);
        }

        private static IJob TraceAll( IJob job, Action<ISample, IJob> traceMethod, IEnumerable<ISample> samples )
        {
            foreach(var sample in samples)
            {
                traceMethod( sample, job );
            }
            return job;
        }
    }
}
