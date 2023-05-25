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
    /// The default implementation of the ProtocolStep class
    /// </summary>
    [XmlNamespaceAttribute("http://www.transformation-tool-contest.eu/ttc21/laboratoryAutomation")]
    [XmlNamespacePrefixAttribute("lab")]
    [ModelRepresentationClassAttribute("http://www.transformation-tool-contest.eu/ttc21/laboratoryAutomation#//ProtocolSt" +
        "ep")]
    public abstract partial class ProtocolStep : ModelElement, IProtocolStep, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Id property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private string _id;
        
        private static Lazy<ITypedElement> _idAttribute = new Lazy<ITypedElement>(RetrieveIdAttribute);
        
        private static Lazy<ITypedElement> _nextReference = new Lazy<ITypedElement>(RetrieveNextReference);
        
        /// <summary>
        /// The backing field for the Next property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private IProtocolStep _next;
        
        private static Lazy<ITypedElement> _previousReference = new Lazy<ITypedElement>(RetrievePreviousReference);
        
        /// <summary>
        /// The backing field for the Previous property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private IProtocolStep _previous;
        
        private static IClass _classInstance;
        
        /// <summary>
        /// The id property
        /// </summary>
        [DisplayNameAttribute("id")]
        [CategoryAttribute("ProtocolStep")]
        [XmlElementNameAttribute("id")]
        [XmlAttributeAttribute(true)]
        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                if ((this._id != value))
                {
                    string old = this._id;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnIdChanging(e);
                    this.OnPropertyChanging("Id", e, _idAttribute);
                    this._id = value;
                    this.OnIdChanged(e);
                    this.OnPropertyChanged("Id", e, _idAttribute);
                }
            }
        }
        
        /// <summary>
        /// The next property
        /// </summary>
        [DisplayNameAttribute("next")]
        [CategoryAttribute("ProtocolStep")]
        [XmlElementNameAttribute("next")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("previous")]
        public IProtocolStep Next
        {
            get
            {
                return this._next;
            }
            set
            {
                if ((this._next != value))
                {
                    IProtocolStep old = this._next;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnNextChanging(e);
                    this.OnPropertyChanging("Next", e, _nextReference);
                    this._next = value;
                    if ((old != null))
                    {
                        old.Previous = null;
                        old.Deleted -= this.OnResetNext;
                    }
                    if ((value != null))
                    {
                        value.Previous = this;
                        value.Deleted += this.OnResetNext;
                    }
                    this.OnNextChanged(e);
                    this.OnPropertyChanged("Next", e, _nextReference);
                }
            }
        }
        
        /// <summary>
        /// The previous property
        /// </summary>
        [DisplayNameAttribute("previous")]
        [CategoryAttribute("ProtocolStep")]
        [XmlElementNameAttribute("previous")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("next")]
        public IProtocolStep Previous
        {
            get
            {
                return this._previous;
            }
            set
            {
                if ((this._previous != value))
                {
                    IProtocolStep old = this._previous;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnPreviousChanging(e);
                    this.OnPropertyChanging("Previous", e, _previousReference);
                    this._previous = value;
                    if ((old != null))
                    {
                        old.Next = null;
                        old.Deleted -= this.OnResetPrevious;
                    }
                    if ((value != null))
                    {
                        value.Next = this;
                        value.Deleted += this.OnResetPrevious;
                    }
                    this.OnPreviousChanged(e);
                    this.OnPropertyChanged("Previous", e, _previousReference);
                }
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new ProtocolStepReferencedElementsCollection(this));
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
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://www.transformation-tool-contest.eu/ttc21/laboratoryAutomation#//ProtocolSt" +
                            "ep")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the Id property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IdChanging;
        
        /// <summary>
        /// Gets fired when the Id property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IdChanged;
        
        /// <summary>
        /// Gets fired before the Next property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> NextChanging;
        
        /// <summary>
        /// Gets fired when the Next property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> NextChanged;
        
        /// <summary>
        /// Gets fired before the Previous property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> PreviousChanging;
        
        /// <summary>
        /// Gets fired when the Previous property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> PreviousChanged;
        
        private static ITypedElement RetrieveIdAttribute()
        {
            return ((ITypedElement)(((ModelElement)(TTC2021.LabWorkflows.LaboratoryAutomation.ProtocolStep.ClassInstance)).Resolve("id")));
        }
        
        /// <summary>
        /// Raises the IdChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIdChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IdChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IdChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIdChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IdChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        private static ITypedElement RetrieveNextReference()
        {
            return ((ITypedElement)(((ModelElement)(TTC2021.LabWorkflows.LaboratoryAutomation.ProtocolStep.ClassInstance)).Resolve("next")));
        }
        
        /// <summary>
        /// Raises the NextChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnNextChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.NextChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the NextChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnNextChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.NextChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Next property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetNext(object sender, System.EventArgs eventArgs)
        {
            this.Next = null;
        }
        
        private static ITypedElement RetrievePreviousReference()
        {
            return ((ITypedElement)(((ModelElement)(TTC2021.LabWorkflows.LaboratoryAutomation.ProtocolStep.ClassInstance)).Resolve("previous")));
        }
        
        /// <summary>
        /// Raises the PreviousChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnPreviousChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.PreviousChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the PreviousChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnPreviousChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.PreviousChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Previous property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetPrevious(object sender, System.EventArgs eventArgs)
        {
            this.Previous = null;
        }
        
        /// <summary>
        /// Resolves the given URI to a child model element
        /// </summary>
        /// <returns>The model element or null if it could not be found</returns>
        /// <param name="reference">The requested reference name</param>
        /// <param name="index">The index of this reference</param>
        protected override IModelElement GetModelElementForReference(string reference, int index)
        {
            if ((reference == "NEXT"))
            {
                return this.Next;
            }
            if ((reference == "PREVIOUS"))
            {
                return this.Previous;
            }
            return base.GetModelElementForReference(reference, index);
        }
        
        /// <summary>
        /// Resolves the given attribute name
        /// </summary>
        /// <returns>The attribute value or null if it could not be found</returns>
        /// <param name="attribute">The requested attribute name</param>
        /// <param name="index">The index of this attribute</param>
        protected override object GetAttributeValue(string attribute, int index)
        {
            if ((attribute == "ID"))
            {
                return this.Id;
            }
            return base.GetAttributeValue(attribute, index);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "NEXT"))
            {
                this.Next = ((IProtocolStep)(value));
                return;
            }
            if ((feature == "PREVIOUS"))
            {
                this.Previous = ((IProtocolStep)(value));
                return;
            }
            if ((feature == "ID"))
            {
                this.Id = ((string)(value));
                return;
            }
            base.SetFeature(feature, value);
        }
        
        /// <summary>
        /// Gets the property expression for the given attribute
        /// </summary>
        /// <returns>An incremental property expression</returns>
        /// <param name="attribute">The requested attribute in upper case</param>
        protected override NMF.Expressions.INotifyExpression<object> GetExpressionForAttribute(string attribute)
        {
            if ((attribute == "ID"))
            {
                return new IdProxy(this);
            }
            return base.GetExpressionForAttribute(attribute);
        }
        
        /// <summary>
        /// Gets the property expression for the given reference
        /// </summary>
        /// <returns>An incremental property expression</returns>
        /// <param name="reference">The requested reference in upper case</param>
        protected override NMF.Expressions.INotifyExpression<NMF.Models.IModelElement> GetExpressionForReference(string reference)
        {
            if ((reference == "NEXT"))
            {
                return new NextProxy(this);
            }
            if ((reference == "PREVIOUS"))
            {
                return new PreviousProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            if ((_classInstance == null))
            {
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://www.transformation-tool-contest.eu/ttc21/laboratoryAutomation#//ProtocolSt" +
                        "ep")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the ProtocolStep class
        /// </summary>
        public class ProtocolStepReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private ProtocolStep _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public ProtocolStepReferencedElementsCollection(ProtocolStep parent)
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
                    if ((this._parent.Next != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.Previous != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.NextChanged += this.PropagateValueChanges;
                this._parent.PreviousChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.NextChanged -= this.PropagateValueChanges;
                this._parent.PreviousChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.Next == null))
                {
                    IProtocolStep nextCasted = item.As<IProtocolStep>();
                    if ((nextCasted != null))
                    {
                        this._parent.Next = nextCasted;
                        return;
                    }
                }
                if ((this._parent.Previous == null))
                {
                    IProtocolStep previousCasted = item.As<IProtocolStep>();
                    if ((previousCasted != null))
                    {
                        this._parent.Previous = previousCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Next = null;
                this._parent.Previous = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.Next))
                {
                    return true;
                }
                if ((item == this._parent.Previous))
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
                if ((this._parent.Next != null))
                {
                    array[arrayIndex] = this._parent.Next;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.Previous != null))
                {
                    array[arrayIndex] = this._parent.Previous;
                    arrayIndex = (arrayIndex + 1);
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                if ((this._parent.Next == item))
                {
                    this._parent.Next = null;
                    return true;
                }
                if ((this._parent.Previous == item))
                {
                    this._parent.Previous = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Next).Concat(this._parent.Previous).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the id property
        /// </summary>
        private sealed class IdProxy : ModelPropertyChange<IProtocolStep, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public IdProxy(IProtocolStep modelElement) : 
                    base(modelElement, "id")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override string Value
            {
                get
                {
                    return this.ModelElement.Id;
                }
                set
                {
                    this.ModelElement.Id = value;
                }
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the next property
        /// </summary>
        private sealed class NextProxy : ModelPropertyChange<IProtocolStep, IProtocolStep>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public NextProxy(IProtocolStep modelElement) : 
                    base(modelElement, "next")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IProtocolStep Value
            {
                get
                {
                    return this.ModelElement.Next;
                }
                set
                {
                    this.ModelElement.Next = value;
                }
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the previous property
        /// </summary>
        private sealed class PreviousProxy : ModelPropertyChange<IProtocolStep, IProtocolStep>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public PreviousProxy(IProtocolStep modelElement) : 
                    base(modelElement, "previous")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IProtocolStep Value
            {
                get
                {
                    return this.ModelElement.Previous;
                }
                set
                {
                    this.ModelElement.Previous = value;
                }
            }
        }
    }
}

