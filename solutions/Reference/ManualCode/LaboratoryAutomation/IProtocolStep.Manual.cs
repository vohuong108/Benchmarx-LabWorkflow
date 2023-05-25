using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.LaboratoryAutomation
{
    public partial interface IProtocolStep
    {
        IEnumerable<IJob> CreateJobs( IEnumerable<ISample> samples, ILocationRepository locationRepository, Action<ISample, IJob> trace );
    }
}
