using System;
using System.ComponentModel.Composition;

namespace StevenVolckaert.Windows.Mvvm
{
    // TODO Assess whether this class has its place: It might be provided by Prism 5.0.

    /// <summary>
    /// Specifies that the annotated class is a view.
    /// This class cannot be inherited.
    /// </summary>
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        public string RegionName { get; set; }

        public string ViewName
        {
            get { return base.ContractName; }
        }

        public ViewExportAttribute()
            : base(typeof(object))
        {
        }

        public ViewExportAttribute(string viewName)
            : base(viewName, typeof(object))
        {
        }
    }
}
