using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.LaboratoryAutomation
{
    public partial class DistributeSample : IProtocolStep
    {
        public override IEnumerable<IJob> CreateJobs( IEnumerable<ISample> samples, ILocationRepository locationRepository, Action<ISample, IJob> trace )
        {
            return from sample in samples
                   let origin = locationRepository.LocateSampleOrigin( sample )
                   let location = locationRepository.LocateSampleProcessing( sample )
                   group (origin, location, sample) by (location.Plate, origin.TubeRunner, location.Cavity / 8, origin.Cavity / 8) into transferChunk
                   select TraceAll( AddTips( new LiquidTransferJob
                   {
                       ProtocolStepName = Id,
                       Source = transferChunk.Key.TubeRunner,
                       Target = transferChunk.Key.Plate
                   }, transferChunk.Select( l => (l.origin.Cavity, l.location.Cavity) ) ), trace, transferChunk.Select( g => g.sample ) );
        }

        private static IJob TraceAll( IJob job, Action<ISample, IJob> traceMethod, IEnumerable<ISample> samples )
        {
            foreach(var sample in samples)
            {
                traceMethod( sample, job );
            }
            return job;
        }

        private LiquidTransferJob AddTips( LiquidTransferJob source, IEnumerable<(int,int)> cavities )
        {
            foreach(var cavity in cavities)
            {
                source.Tips.Add( new TipLiquidTransfer
                {
                    Volume = Volume,
                    SourceCavityIndex = cavity.Item1,
                    TargetCavityIndex = cavity.Item2
                } );
            }
            return source;
        }
    }
}
