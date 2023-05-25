using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.LaboratoryAutomation;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.JobCollection
{
    public partial class LiquidTransferJob
    {
        public override IEnumerable<ISample> GetFailedSamples( IReverseLocationRepository locationRepository )
        {
            return Tips.Where( t => t.Status == JobStatus.Failed )
                .Select( tip => locationRepository.IdentifySample( Target, tip.TargetCavityIndex ) )
                .Where( s => s != null );
        }
    }
}
