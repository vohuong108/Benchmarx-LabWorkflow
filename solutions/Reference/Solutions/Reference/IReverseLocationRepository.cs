using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    public interface IReverseLocationRepository
    {
        ISample IdentifySample( ILabware labware, int cavity );
    }
}
