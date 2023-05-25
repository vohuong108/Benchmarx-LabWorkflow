using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.LaboratoryAutomation
{
    public partial class ProtocolStep : IProtocolStep
    {
        public abstract IEnumerable<IJob> CreateJobs( IEnumerable<ISample> samples, ILocationRepository locationRepository, Action<ISample, IJob> trace );
    }
}
