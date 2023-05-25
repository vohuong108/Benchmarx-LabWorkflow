using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;

namespace TTC2021.LabWorkflows.Solutions
{
    public static class SolutionChangeHelper
    {

        public static void CalculateActualChanges( IJobCollection jobCollection, string[] changes, out Dictionary<IJob, JobStatus> stateChanges, out Dictionary<ITipLiquidTransfer, JobStatus> tipChanges, out List<string> newSamples )
        {
            stateChanges = new Dictionary<IJob, JobStatus>();
            tipChanges = new Dictionary<ITipLiquidTransfer, JobStatus>();
            newSamples = new List<string>();
            foreach(var line in changes)
            {
                if (line.StartsWith("NewSample"))
                {
                    newSamples.Add( line.Substring( "NewSample".Length + 1 ) );
                    continue;
                }
                var firstUnderscore = line.IndexOf( '_' );
                var lastUnderscore = line.LastIndexOf( '_' );

                var stepName = line.Substring( 0, firstUnderscore );
                var plateName = line.Substring( firstUnderscore + 1, lastUnderscore - firstUnderscore - 1 );
                var state = line.Substring( lastUnderscore + 1 );

                foreach(var job in jobCollection.Jobs.Where(j => j.ProtocolStepName == stepName))
                {
                    switch(job)
                    {
                        case LiquidTransferJob liquidTransfer:
                            if(liquidTransfer.Target.Name == plateName)
                            {
                                var wasSuccess = true;
                                foreach(var tip in liquidTransfer.Tips)
                                {
                                    var tipSuccess = state[tip.TargetCavityIndex] == 'S';
                                    wasSuccess &= tipSuccess;
                                    tipChanges.Add( tip, tipSuccess ? JobStatus.Succeeded : JobStatus.Failed );
                                }
                                stateChanges.Add( liquidTransfer, wasSuccess ? JobStatus.Succeeded : JobStatus.Failed );
                            }
                            break;
                        case IncubateJob incubate:
                            if(incubate.Microplate.Name == plateName)
                            {
                                stateChanges.Add( incubate, state == "S" ? JobStatus.Succeeded : JobStatus.Failed );
                            }
                            break;
                        case WashJob washJob:
                            if(washJob.Microplate.Name == plateName)
                            {
                                stateChanges.Add( washJob, state == "S" ? JobStatus.Succeeded : JobStatus.Failed );
                            }
                            break;
                    }
                }
            }
        }

    }
}
