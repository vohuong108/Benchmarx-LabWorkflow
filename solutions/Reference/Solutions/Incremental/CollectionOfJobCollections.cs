using NMF.Collections.ObjectModel;
using NMF.Expressions.Linq;
using NMF.Transformations.Core;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TTC2021.LabWorkflows.JobCollection;

namespace TTC2021.LabWorkflows.Solutions
{
    public class CollectionOfJobCollections : CustomCollection<JobsOfProtocolStep>
    {
        private readonly IJobCollection _jobCollection;

        public CollectionOfJobCollections( IJobCollection jobCollection, ITransformationContext context )
            : base( jobCollection.Jobs.GroupBy( j => j.ProtocolStepName ).Select( group => new JobsOfProtocolStep( group.Key, group, context ) ) )
        {
            _jobCollection = jobCollection;
        }

        public override void Add( JobsOfProtocolStep item )
        {
            foreach(var job in item.Jobs)
            {
                _jobCollection.Jobs.Add( job );
            }
            item.Jobs.CollectionChanged += JobsChanged;
        }

        private void JobsChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if(e.NewItems != null)
            {
                foreach(var job in e.NewItems.OfType<IJob>())
                {
                    _jobCollection.Jobs.Add( job );
                }
            }
        }

        public override void Clear()
        {
            for(int i = _jobCollection.Jobs.Count - 1; i >= 0; i--)
            {
                if(_jobCollection.Jobs[i].State == JobStatus.Planned)
                {
                    _jobCollection.Jobs.RemoveAt( i );
                }
            }
        }

        public override bool Remove( JobsOfProtocolStep item )
        {
            var any = false;
            foreach(var job in item.Jobs.ToList())
            {
                if(job.State == JobStatus.Planned)
                {
                    _jobCollection.Jobs.Remove( job );
                }
                any = true;
            }
            return any;
        }
    }
}
