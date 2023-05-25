using NMF.Expressions;

namespace TTC2021.LabWorkflows.Solutions
{
    public class Tubes
    {
        public Tubes( string name, IEnumerableExpression<ProcessWell> samples )
        {
            Name = name;
            Samples = samples;
        }

        public string Name
        {
            get;
        }

        public IEnumerableExpression<ProcessWell> Samples
        {
            get;
        }
    }
}
