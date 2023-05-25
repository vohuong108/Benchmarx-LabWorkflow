using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.LaboratoryAutomation;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.JobCollection
{
    public partial class WashJob
    {
        public override IEnumerable<ISample> GetFailedSamples( IReverseLocationRepository locationRepository )
        {
            if (State != JobStatus.Failed)
            {
                return Enumerable.Empty<ISample>();
            }
            return Cavities.Select( cavity => locationRepository.IdentifySample( Microplate, cavity ) ).Where( s => s != null );
        }
    }
}
