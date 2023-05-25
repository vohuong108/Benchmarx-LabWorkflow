using NMF.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TTC2021.LabWorkflows.Solutions.Incremental
{
    internal class ExpressionWrapper<T> : IEnumerableExpression<T>
    {
        private readonly INotifyEnumerable<T> _inner;

        public ExpressionWrapper(INotifyEnumerable<T> inner)
        {
            _inner = inner;
        }

        public INotifyEnumerable<T> AsNotifiable()
        {
            return _inner;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        INotifyEnumerable IEnumerableExpression.AsNotifiable()
        {
            return AsNotifiable();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
