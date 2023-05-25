using NMF.Expressions;
using NMF.Expressions.Linq;
using System.Linq;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    public class ProcessPlate
    {
        private static ObservingFunc<ProcessPlate, bool> _hasValidSample = new ObservingFunc<ProcessPlate, bool>( p => p.Columns.Any( c => c.AnyValidSample.Value ) );

        public ProcessPlate( string name, IEnumerableExpression<ProcessColumn> columns )
        {
            Name = name;
            Columns = columns;
            AllSamples = columns.SelectMany( c => c.AllSamples );
            AnyValidSample = _hasValidSample.Observe( this );
            AnyValidSample.Successors.SetDummy();
        }

        public IEnumerableExpression<ISample> AllSamples
        {
            get;
        }

        public INotifyValue<bool> AnyValidSample
        {
            get;
        }

        public string Name
        {
            get;
        }

        public IEnumerableExpression<ProcessColumn> Columns
        {
            get;
        }

        public override string ToString()
        {
            return $"[Plate {Name}]";
        }
    }
}
