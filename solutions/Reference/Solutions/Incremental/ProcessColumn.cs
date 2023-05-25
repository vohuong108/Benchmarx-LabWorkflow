using NMF.Expressions;
using NMF.Expressions.Linq;
using System.Linq;
using TTC2021.LabWorkflows.LaboratoryAutomation;

namespace TTC2021.LabWorkflows.Solutions
{
    public class ProcessColumn
    {
        private static ObservingFunc<ProcessColumn, bool> _anyNonErrorSample = new ObservingFunc<ProcessColumn, bool>( c => c.AllSamples.Any( s => s.State != SampleState.Error ) );

        public ProcessColumn( int column, IEnumerableExpression<ProcessWell> wells )
        {
            Column = column;
            Samples = wells;
            AllSamples = wells.Select( w => w.Sample );
            AnyValidSample = _anyNonErrorSample.Observe( this );
            AnyValidSample.Successors.SetDummy();
        }

        public INotifyValue<bool> AnyValidSample
        {
            get;
        }

        public int Column
        {
            get;
        }

        public IEnumerableExpression<ProcessWell> Samples
        {
            get;
        }

        public IEnumerableExpression<ISample> AllSamples
        {
            get;
        }

        public override string ToString()
        {
            return $"[Column {Column}]";
        }
    }
}
