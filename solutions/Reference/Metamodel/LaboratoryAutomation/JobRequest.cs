//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NMF.Collections.Generic;
using NMF.Collections.ObjectModel;
using NMF.Expressions;
using NMF.Expressions.Linq;
using NMF.Models;
using NMF.Models.Collections;
using NMF.Models.Expressions;
using NMF.Models.Meta;
using NMF.Models.Repository;
using NMF.Serialization;
using NMF.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace TTC2021.LabWorkflows.LaboratoryAutomation
{
    
    
    /// <summary>
    /// The default implementation of the JobRequest class
    /// </summary>
    [XmlNamespaceAttribute("http://www.transformation-tool-contest.eu/ttc21/laboratoryAutomation")]
    [XmlNamespacePrefixAttribute("lab")]
    [ModelRepresentationClassAttribute("http://www.transformation-tool-contest.eu/ttc21/laboratoryAutomation#//JobRequest" +
        "")]
    public partial class JobRequest : ModelElement, IJobRequest, IModelElement
    {
        
        private static Lazy<ITypedElement> _assayReference = new Lazy<ITypedElement>(RetrieveAssayReference);
        
        /// <summary>
        /// The backing field for the Assay property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private IAssay _assay;
        
        private static Lazy<ITypedElement> _samplesReference = new Lazy<ITypedElement>(RetrieveSamplesReference);
        
        /// <summary>
        /// The backing field for the Samples property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private ObservableCompositionOrderedSet<ISample> _samples;
        
        private static IClass _classInstance;
        
        public JobRequest()
        {
            this._samples = new ObservableCompositionOrderedSet<ISample>(this);
            this._samples.CollectionChanging += this.SamplesCollectionChanging;
            this._samples.CollectionChanged += this.SamplesCollectionChanged;
        }
        
        /// <summary>
        /// The assay property
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("assay")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        public IAssay Assay
        {
            get
            {
                return this._assay;
            }
            set
            {
                if ((this._assay != value))
                {
                    IAssay old = this._assay;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnAssayChanging(e);
                    this.OnPropertyChanging("Assay", e, _assayReference);
                    this._assay = value;
                    if ((old != null))
                    {
                        old.Parent = null;
                        old.ParentChanged -= this.OnResetAssay;
                    }
                    if ((value != null))
                    {
                        value.Parent = this;
                        value.ParentChanged += this.OnResetAssay;
                    }
                    this.OnAssayChanged(e);
                    this.OnPropertyChanged("Assay", e, _assayReference);
                }
            }
        }
        
        /// <summary>
        /// The samples property
        /// </summary>
        [LowerBoundAttribute(1)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("samples")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [ConstantAttribute()]
        public IOrderedSetExpression<ISample> Samples
        {
            get
            {
                return this._samples;
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new JobRequestChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new JobRequestReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the Class model for this type
        /// </summary>
        public new static IClass ClassInstance
        {
            get
            {
                if ((_classInstance == null))
                {
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://www.transformation-tool-contest.eu/ttc21/laboratoryAutomation#//JobRequest" +
                            "")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the Assay property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> AssayChanging;
        
        /// <summary>
        /// Gets fired when the Assay property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> AssayChanged;
        
        private static ITypedElement RetrieveAssayReference()
        {
            return ((ITypedElement)(((ModelElement)(TTC2021.LabWorkflows.LaboratoryAutomation.JobRequest.ClassInstance)).Resolve("assay")));
        }
        
        /// <summary>
        /// Raises the AssayChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnAssayChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.AssayChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the AssayChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnAssayChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.AssayChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Assay property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetAssay(object sender, System.EventArgs eventArgs)
        {
            this.Assay = null;
        }
        
        private static ITypedElement RetrieveSamplesReference()
        {
            return ((ITypedElement)(((ModelElement)(TTC2021.LabWorkflows.LaboratoryAutomation.JobRequest.ClassInstance)).Resolve("samples")));
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the Samples property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void SamplesCollectionChanging(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanging("Samples", e, _samplesReference);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the Samples property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void SamplesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("Samples", e, _samplesReference);
        }
        
        /// <summary>
        /// Gets the relative URI fragment for the given child model element
        /// </summary>
        /// <returns>A fragment of the relative URI</returns>
        /// <param name="element">The element that should be looked for</param>
        protected override string GetRelativePathForNonIdentifiedChild(IModelElement element)
        {
            if ((element == this.Assay))
            {
                return ModelHelper.CreatePath("Assay");
            }
            int samplesIndex = ModelHelper.IndexOfReference(this.Samples, element);
            if ((samplesIndex != -1))
            {
                return ModelHelper.CreatePath("samples", samplesIndex);
            }
            return base.GetRelativePathForNonIdentifiedChild(element);
        }
        
        /// <summary>
        /// Resolves the given URI to a child model element
        /// </summary>
        /// <returns>The model element or null if it could not be found</returns>
        /// <param name="reference">The requested reference name</param>
        /// <param name="index">The index of this reference</param>
        protected override IModelElement GetModelElementForReference(string reference, int index)
        {
            if ((reference == "ASSAY"))
            {
                return this.Assay;
            }
            if ((reference == "SAMPLES"))
            {
                if ((index < this.Samples.Count))
                {
                    return this.Samples[index];
                }
                else
                {
                    return null;
                }
            }
            return base.GetModelElementForReference(reference, index);
        }
        
        /// <summary>
        /// Gets the Model element collection for the given feature
        /// </summary>
        /// <returns>A non-generic list of elements</returns>
        /// <param name="feature">The requested feature</param>
        protected override System.Collections.IList GetCollectionForFeature(string feature)
        {
            if ((feature == "SAMPLES"))
            {
                return this._samples;
            }
            return base.GetCollectionForFeature(feature);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "ASSAY"))
            {
                this.Assay = ((IAssay)(value));
                return;
            }
            base.SetFeature(feature, value);
        }
        
        /// <summary>
        /// Gets the property expression for the given reference
        /// </summary>
        /// <returns>An incremental property expression</returns>
        /// <param name="reference">The requested reference in upper case</param>
        protected override NMF.Expressions.INotifyExpression<NMF.Models.IModelElement> GetExpressionForReference(string reference)
        {
            if ((reference == "ASSAY"))
            {
                return new AssayProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the property name for the given container
        /// </summary>
        /// <returns>The name of the respective container reference</returns>
        /// <param name="container">The container object</param>
        protected override string GetCompositionName(object container)
        {
            if ((container == this._samples))
            {
                return "samples";
            }
            return base.GetCompositionName(container);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            if ((_classInstance == null))
            {
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://www.transformation-tool-contest.eu/ttc21/laboratoryAutomation#//JobRequest" +
                        "")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the JobRequest class
        /// </summary>
        public class JobRequestChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private JobRequest _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public JobRequestChildrenCollection(JobRequest parent)
            {
                this._parent = parent;
            }
            
            /// <summary>
            /// Gets the amount of elements contained in this collection
            /// </summary>
            public override int Count
            {
                get
                {
                    int count = 0;
                    if ((this._parent.Assay != null))
                    {
                        count = (count + 1);
                    }
                    count = (count + this._parent.Samples.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.AssayChanged += this.PropagateValueChanges;
                this._parent.Samples.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.AssayChanged -= this.PropagateValueChanges;
                this._parent.Samples.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.Assay == null))
                {
                    IAssay assayCasted = item.As<IAssay>();
                    if ((assayCasted != null))
                    {
                        this._parent.Assay = assayCasted;
                        return;
                    }
                }
                ISample samplesCasted = item.As<ISample>();
                if ((samplesCasted != null))
                {
                    this._parent.Samples.Add(samplesCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Assay = null;
                this._parent.Samples.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.Assay))
                {
                    return true;
                }
                if (this._parent.Samples.Contains(item))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Copies the contents of the collection to the given array starting from the given array index
            /// </summary>
            /// <param name="array">The array in which the elements should be copied</param>
            /// <param name="arrayIndex">The starting index</param>
            public override void CopyTo(IModelElement[] array, int arrayIndex)
            {
                if ((this._parent.Assay != null))
                {
                    array[arrayIndex] = this._parent.Assay;
                    arrayIndex = (arrayIndex + 1);
                }
                IEnumerator<IModelElement> samplesEnumerator = this._parent.Samples.GetEnumerator();
                try
                {
                    for (
                    ; samplesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = samplesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    samplesEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                if ((this._parent.Assay == item))
                {
                    this._parent.Assay = null;
                    return true;
                }
                ISample sampleItem = item.As<ISample>();
                if (((sampleItem != null) 
                            && this._parent.Samples.Remove(sampleItem)))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Gets an enumerator that enumerates the collection
            /// </summary>
            /// <returns>A generic enumerator</returns>
            public override IEnumerator<IModelElement> GetEnumerator()
            {
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Assay).Concat(this._parent.Samples).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the JobRequest class
        /// </summary>
        public class JobRequestReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private JobRequest _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public JobRequestReferencedElementsCollection(JobRequest parent)
            {
                this._parent = parent;
            }
            
            /// <summary>
            /// Gets the amount of elements contained in this collection
            /// </summary>
            public override int Count
            {
                get
                {
                    int count = 0;
                    if ((this._parent.Assay != null))
                    {
                        count = (count + 1);
                    }
                    count = (count + this._parent.Samples.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.AssayChanged += this.PropagateValueChanges;
                this._parent.Samples.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.AssayChanged -= this.PropagateValueChanges;
                this._parent.Samples.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.Assay == null))
                {
                    IAssay assayCasted = item.As<IAssay>();
                    if ((assayCasted != null))
                    {
                        this._parent.Assay = assayCasted;
                        return;
                    }
                }
                ISample samplesCasted = item.As<ISample>();
                if ((samplesCasted != null))
                {
                    this._parent.Samples.Add(samplesCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Assay = null;
                this._parent.Samples.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.Assay))
                {
                    return true;
                }
                if (this._parent.Samples.Contains(item))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Copies the contents of the collection to the given array starting from the given array index
            /// </summary>
            /// <param name="array">The array in which the elements should be copied</param>
            /// <param name="arrayIndex">The starting index</param>
            public override void CopyTo(IModelElement[] array, int arrayIndex)
            {
                if ((this._parent.Assay != null))
                {
                    array[arrayIndex] = this._parent.Assay;
                    arrayIndex = (arrayIndex + 1);
                }
                IEnumerator<IModelElement> samplesEnumerator = this._parent.Samples.GetEnumerator();
                try
                {
                    for (
                    ; samplesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = samplesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    samplesEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                if ((this._parent.Assay == item))
                {
                    this._parent.Assay = null;
                    return true;
                }
                ISample sampleItem = item.As<ISample>();
                if (((sampleItem != null) 
                            && this._parent.Samples.Remove(sampleItem)))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Gets an enumerator that enumerates the collection
            /// </summary>
            /// <returns>A generic enumerator</returns>
            public override IEnumerator<IModelElement> GetEnumerator()
            {
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Assay).Concat(this._parent.Samples).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the assay property
        /// </summary>
        private sealed class AssayProxy : ModelPropertyChange<IJobRequest, IAssay>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public AssayProxy(IJobRequest modelElement) : 
                    base(modelElement, "assay")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IAssay Value
            {
                get
                {
                    return this.ModelElement.Assay;
                }
                set
                {
                    this.ModelElement.Assay = value;
                }
            }
        }
    }
}
