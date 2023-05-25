using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.LaboratoryAutomation;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.JobCollection
{
    public partial class Job : IJob
    {
        private readonly List<ISample> _processedSamples = new List<ISample>();

        public abstract IEnumerable<ISample> GetFailedSamples( IReverseLocationRepository locationRepository );

        public ICollection<ISample> GetProcessedSamples()
        {
            return _processedSamples;
        }
    }
}
