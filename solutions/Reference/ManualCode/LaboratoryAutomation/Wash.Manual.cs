using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.LaboratoryAutomation
{
    public partial class Wash
    {
        public override IEnumerable<IJob> CreateJobs( IEnumerable<ISample> samples, ILocationRepository locationRepository , Action<ISample, IJob> trace)
        {
            return from sample in samples
                   let processingLocation = locationRepository.LocateSampleProcessing( sample )
                   group (sample, processingLocation) by processingLocation.Plate into washGroup
                   select TraceAll( AddCavities( new WashJob
                   {
                       ProtocolStepName = Id,
                       Microplate = washGroup.Key
                   }, washGroup.Select( l => l.processingLocation.Cavity ) ), trace, washGroup.Select(g => g.sample));
        }

        private static WashJob AddCavities(WashJob washJob, IEnumerable<int> cavities)
        {
            foreach(var cavity in cavities)
            {
                washJob.Cavities.Add( cavity );
            }
            return washJob;
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
