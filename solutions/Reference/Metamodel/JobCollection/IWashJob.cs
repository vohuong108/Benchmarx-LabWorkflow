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

namespace TTC2021.LabWorkflows.JobCollection
{
    
    
    /// <summary>
    /// The public interface for WashJob
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(WashJob))]
    [XmlDefaultImplementationTypeAttribute(typeof(WashJob))]
    [ModelRepresentationClassAttribute("http://www.transformation-tool-contest.eu/ttc21/jobCollection#//WashJob")]
    public interface IWashJob : IModelElement, IJob
    {
        
        /// <summary>
        /// The cavities property
        /// </summary>
        [LowerBoundAttribute(1)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [DisplayNameAttribute("cavities")]
        [CategoryAttribute("WashJob")]
        [XmlElementNameAttribute("cavities")]
        [XmlAttributeAttribute(true)]
        [ConstantAttribute()]
        IOrderedSetExpression<int> Cavities
        {
            get;
        }
        
        /// <summary>
        /// The microplate property
        /// </summary>
        [DisplayNameAttribute("microplate")]
        [CategoryAttribute("WashJob")]
        [XmlElementNameAttribute("microplate")]
        [XmlAttributeAttribute(true)]
        IMicroplate Microplate
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the Microplate property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> MicroplateChanging;
        
        /// <summary>
        /// Gets fired when the Microplate property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> MicroplateChanged;
    }
}
