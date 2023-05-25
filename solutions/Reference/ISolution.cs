using NMF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows
{
    public interface ISolution
    {
        void Load( JobRequest jobRequest );
        Model Initial();
        Func<Model> ComputeChanges( string[] changes );
    }
}
