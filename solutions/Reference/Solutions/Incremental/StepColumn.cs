namespace TTC2021.LabWorkflows.Solutions
{
    public class StepColumn<TStep>
    {
        public StepColumn( TStep step, ProcessColumn column )
        {
            Step = step;
            Column = column;
        }

        public TStep Step { get; }

        public ProcessColumn Column { get; }
    }
}
