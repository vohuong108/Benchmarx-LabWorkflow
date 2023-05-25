using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.Commandline
{
    [Verb("reference")]
    internal class SolveReferenceVerb : SolveVerbBase
    {
        protected override ISolution CreateSolution()
        {
            return new ReferenceSolution();
        }
    }
}
