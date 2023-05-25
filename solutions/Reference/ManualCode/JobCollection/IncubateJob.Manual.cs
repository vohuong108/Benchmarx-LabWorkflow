using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.LaboratoryAutomation;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.JobCollection
{
    public partial class IncubateJob
    {
        public override IEnumerable<ISample> GetFailedSamples( IReverseLocationRepository locationRepository )
        {
            if(State != JobStatus.Failed)
            {
                return Enumerable.Empty<ISample>();
            }
            return Enumerable.Range(0, 96).Select( cavity => locationRepository.IdentifySample( Microplate, cavity ) ).Where( s => s != null );
        }
    }
}
