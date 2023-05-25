using NMF.Collections.ObjectModel;
using NMF.Expressions;
using NMF.Expressions.Linq;
using NMF.Transformations.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TTC2021.LabWorkflows.JobCollection;

namespace TTC2021.LabWorkflows.Solutions
{
    public class JobsOfProtocolStep
    {
        private string _protocolStepName;
        private JobsOfProtocolStep _nextJobs;
        private ITransformationContext _context;
        private readonly Dictionary<IJob, IDisposable> _hooks = new Dictionary<IJob, IDisposable>();

        public JobsOfProtocolStep(ITransformationContext context)
        {
            Jobs = new ObservableSet<IJob>();
            Jobs.CollectionChanged += JobsChanged;

            _context = context;
        }

        private void JobsChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if(e.NewItems != null && _protocolStepName != null)
            {
                foreach(var item in e.NewItems.OfType<IJob>())
                {
                    item.ProtocolStepName = _protocolStepName;

                    AddHook( item );
                }
            }
            if (e.OldItems != null)
            {
                foreach(var item in e.OldItems.OfType<IJob>())
                {
                    if (_hooks.TryGetValue(item, out var hook))
                    {
                        hook.Dispose();
                        _hooks.Remove( item );
                    }
                }
            }
        }

        private void AddHook( IJob item )
        {
            if(_nextJobs != null)
            {
                var samples = ProtocolSynchronization.GetAffectedSamples( _context, item );
                _hooks.Add( item, CollectionBinding.Create(
                    _nextJobs.Jobs.Where( j => ProtocolSynchronization.GetAffectedSamples( _context, j ).Intersect( samples ).Any(),
                                          j => ProtocolSynchronization.GetAffectedSamples( _context, j ).Intersect( samples ).Any() ),
                    item.Next ) );
            }
        }

        public JobsOfProtocolStep( string protocolStepName, IEnumerableExpression<IJob> jobs, ITransformationContext context ) : this(context)
        {
            _protocolStepName = protocolStepName;

            foreach(var item in jobs)
            {
                Jobs.Add( item );
            }
        }

        public ObservableSet<IJob> Jobs
        {
            get;
        }

        public string ProtocolStepName
        {
            get => _protocolStepName;
            set
            {
                if(_protocolStepName != value)
                {
                    _protocolStepName = value;
                    foreach(var job in Jobs)
                    {
                        job.ProtocolStepName = value;
                    }
                }
            }
        }

        public JobsOfProtocolStep NextJobs
        {
            get => _nextJobs;
            set
            {
                if(_nextJobs != value)
                {
                    if(_nextJobs != null)
                    {
                        foreach(var hook in _hooks.Values)
                        {
                            hook.Dispose();
                        }
                        _hooks.Clear();
                    }
                    _nextJobs = value;
                    if(value != null)
                    {
                        foreach(var item in Jobs)
                        {
                            AddHook( item );
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"[JobsOfProtocolStep {ProtocolStepName}]";
        }
    }
}
