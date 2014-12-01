using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Prism.Regions;

namespace StevenVolckaert.Windows.Mvvm
{
    // TODO Assess whether this class has its place: It might be provided by Prism 5.0.

    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
        [ImportMany(AllowRecomposition = true)]
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "MEF injected values")]
        public Lazy<object, IViewRegionRegistration>[] RegisteredViews { get; set; }

        protected override void OnAttach()
        {
            AddRegisteredViews();
        }

        public void OnImportsSatisfied()
        {
            AddRegisteredViews();
        }

        private void AddRegisteredViews()
        {
            if (Region == null)
                return;

            foreach (var viewEntry in RegisteredViews)
                if (viewEntry.Metadata.RegionName == Region.Name)
                {
                    var view = viewEntry.Value;

                    if (!Region.Views.Contains(view))
                        Region.Add(view);
                }
        }
    }
}

