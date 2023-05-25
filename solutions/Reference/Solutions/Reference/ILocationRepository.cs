using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    public interface ILocationRepository
    {
        (IMicroplate Plate, int Cavity) LocateSampleProcessing( ISample sample );

        (ITubeRunner TubeRunner, int Cavity) LocateSampleOrigin( ISample sample );

        ITrough LocateReagent( IReagent reagent );
    }
}
