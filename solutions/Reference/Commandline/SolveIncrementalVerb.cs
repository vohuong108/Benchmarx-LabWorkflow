using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using TTC2021.LabWorkflows.Solutions;

namespace TTC2021.LabWorkflows.Commandline
{
    [Verb("incremental")]
    internal class SolveIncrementalVerb : SolveVerbBase
    {
        protected override ISolution CreateSolution()
        {
            return new IncrementalSolution();
        }
    }
}
