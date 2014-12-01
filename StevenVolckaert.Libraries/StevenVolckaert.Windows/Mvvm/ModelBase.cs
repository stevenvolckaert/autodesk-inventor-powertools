using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;

namespace StevenVolckaert.Windows.Mvvm
{
    /// <summary>
    /// Base class for MVVM models.
    /// </summary>
    [CLSCompliant(false)]
    public abstract class ModelBase : BindableBase, IPartImportsSatisfiedNotification
    {
        private readonly string[] _propertyNamesToNotifyOnCultureChange;
        private readonly string[] _propertyNamesToNotifyOnSystemOfMeasurementChange;
        private readonly string[] _propertyNamesToNotifyOnTemperatureMeasurementUnitChange;

        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        [Import]
        public IEventAggregator EventAggregator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barco.CineCare.Client.Infrastructure.Models.ModelBase"/> class.
        /// </summary>
        protected ModelBase()
        {
            var type = GetType();
            _propertyNamesToNotifyOnCultureChange = type.PropertyNames(typeof(NotifyPropertyChangedOnCultureChangeAttribute)).ToArray();
            _propertyNamesToNotifyOnSystemOfMeasurementChange = type.PropertyNames(typeof(NotifyPropertyChangedOnSystemOfMeasurementChangeAttribute)).ToArray();
            _propertyNamesToNotifyOnTemperatureMeasurementUnitChange = type.PropertyNames(typeof(NotifyPropertyChangedOnTemperatureMeasurementUnitChangeAttribute)).ToArray();
        }

        /// <summary>
        /// When overridden in a derived class, is invoked when all imports
        /// have been satisfied and are safe to use.
        /// </summary>
        public virtual void OnImportsSatisfied()
        {
            EventAggregator.GetEvent<PreferenceChangedEvent>().Subscribe(OnPreferenceChanged);
            RegisterEvents();
        }

        /// <summary>
        /// Registers the model's events.
        /// <para>The base implementation does nothing.</para>
        /// </summary>
        protected virtual void RegisterEvents()
        {
        }

        /// <summary>
        /// Handles the event aggregator's <see cref="PreferenceChangedEvent"/> event.
        /// </summary>
        /// <param name="preferenceType">The type of preference that has changed.</param>
        public void OnPreferenceChanged(PreferenceType preferenceType)
        {
            switch (preferenceType)
            {
                case PreferenceType.Culture:
                    OnCultureChanged();
                    this.OnPropertyChanged(_propertyNamesToNotifyOnCultureChange);
                    break;
                case PreferenceType.SystemOfMeasurement:
                    OnSystemOfMeasurementChanged();
                    this.OnPropertyChanged(_propertyNamesToNotifyOnSystemOfMeasurementChange);
                    break;
                case PreferenceType.TemperatureMeasurementUnit:
                    OnTemperatureMeasurementUnitChanged();
                    this.OnPropertyChanged(_propertyNamesToNotifyOnTemperatureMeasurementUnitChange);
                    break;
            }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked when the application's
        /// culture has changed.
        /// <para>The base implementation does nothing.</para>
        /// </summary>
        protected virtual void OnCultureChanged()
        {
        }

        /// <summary>
        /// When overridden in a derived class, is invoked when the application's
        /// system of measurement has changed.
        /// <para>The base implementation does nothing.</para>
        /// </summary>
        protected virtual void OnSystemOfMeasurementChanged()
        {
        }

        /// <summary>
        /// When overridden in a derived class, is invoked when the application's
        /// unit of measurement for temperature has changed.
        /// <para>The base implementation does nothing.</para>
        /// </summary>
        protected virtual void OnTemperatureMeasurementUnitChanged()
        {
        }
    }
}
