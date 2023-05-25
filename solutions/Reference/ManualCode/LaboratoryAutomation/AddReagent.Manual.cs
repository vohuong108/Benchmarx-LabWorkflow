using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.LaboratoryAutomation
{
    public partial class AddReagent : IProtocolStep
    {
        public override IEnumerable<IJob> CreateJobs( IEnumerable<ISample> samples, ILocationRepository locationRepository, Action<ISample, IJob> trace )
        {
            return from sample in samples
                   let location = locationRepository.LocateSampleProcessing( sample )
                   group (sample, location) by (location.Plate, location.Cavity / 8) into transferChunk
                   select TraceAll( AddTips( new LiquidTransferJob
                   {
                       ProtocolStepName = Id,
                       Source = locationRepository.LocateReagent( Reagent ),
                       Target = transferChunk.Key.Plate
                   }, transferChunk.Select(l => l.location.Cavity) ), trace, transferChunk.Select(g => g.sample));
        }

        private static IJob TraceAll(IJob job, Action<ISample, IJob> traceMethod, IEnumerable<ISample> samples)
        {
            foreach(var sample in samples)
            {
                traceMethod( sample, job );
            }
            return job;
        }

        private LiquidTransferJob AddTips(LiquidTransferJob source, IEnumerable<int> cavities)
        {
            foreach(var cavity in cavities)
            {
                source.Tips.Add( new TipLiquidTransfer
                {
                    Volume = Volume,
                    SourceCavityIndex = 0,
                    TargetCavityIndex = cavity
                } );
            }
            return source;
        }
    }
}
