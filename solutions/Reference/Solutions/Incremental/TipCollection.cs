using NMF.Collections.ObjectModel;
using NMF.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTC2021.LabWorkflows.JobCollection;

namespace TTC2021.LabWorkflows.Solutions
{
    public class TipCollection : CustomCollection<ITipLiquidTransfer>
    {
        public TipCollection(ICollectionExpression<ITipLiquidTransfer> originalCollection)
            : base(originalCollection)
        {
            _originalCollection = originalCollection;
        }

        private readonly ICollection<ITipLiquidTransfer> _originalCollection;

        public override void Add( ITipLiquidTransfer item )
        {
            _originalCollection.Add( item );
        }

        public override void Clear()
        {
            foreach(var item in _originalCollection.Where(tip => tip.Status == JobStatus.Planned).ToArray())
            {
                _originalCollection.Remove( item );
            }
        }

        public override bool Remove( ITipLiquidTransfer item )
        {
            if (item.Status == JobStatus.Planned)
            {
                return _originalCollection.Remove( item );
            }
            return false;
        }
    }
}
