using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.LaboratoryAutomation;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.JobCollection
{
    public partial interface IJob
    {
        IEnumerable<ISample> GetFailedSamples( IReverseLocationRepository locationRepository );

        ICollection<ISample> GetProcessedSamples();
    }
}
