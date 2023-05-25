using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    public class ProcessWell
    {
        public ProcessWell( int well, ISample sample )
        {
            Well = well;
            Sample = sample;
        }

        public int Well
        {
            get;
        }

        public ISample Sample
        {
            get;
        }

        public override string ToString()
        {
            return $"[Well {Well}]";
        }
    }
}
