using CommandLine;
using NMF.Models;
using NMF.Models.Changes;
using NMF.Models.Repository;
using System;
using System.Diagnostics;
using System.IO;
using TTC2021.LabWorkflows.Commandline;

[assembly: ModelMetadata( "http://www.transformation-tool-contest.eu/ttc21/laboratoryAutomation", "TTC2021.LabWorkflows.laboratoryAutomation.nmeta" )]
[assembly: ModelMetadata( "http://www.transformation-tool-contest.eu/ttc21/jobCollection", "TTC2021.LabWorkflows.jobCollection.nmeta" )]

namespace TTC2021.LabWorkflows
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments( args, typeof( GenerateModelVerb ), typeof( CheckModelVerb ), typeof( SolveReferenceVerb ), typeof(SolveIncrementalVerb) )
                .WithNotParsed( _ => Environment.ExitCode = 1 )
                .WithParsed( ( VerbBase verb ) => verb.Execute() );
        }
    }
}