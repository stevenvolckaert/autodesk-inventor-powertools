using System;
using System.Windows.Data;
using System.Windows.Markup;
using StevenVolckaert.Windows.Data;

namespace StevenVolckaert.Windows.Markup
{
    /// <summary>
    /// A XAML markup extension that provides access to a resource class,
    /// for looking up localized strings, etc.
    /// </summary>
    public class ResourceExtension : MarkupExtension
    {
        private static CaseConverter _caseConverter = new CaseConverter();

        /// <summary>
        /// Gets or sets the object to use as the binding source.
        /// </summary>
        public object Source { get; set; }

        public string Path { get; set; }

        /// <summary>
        /// Gets or sets a string that specifies how to format the binding if it displays
        /// the bound value as a string.
        /// </summary>
        public string StringFormat { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies how to convert case.
        /// </summary>
        public CaseConvertOperation ConvertCase { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return
                new Binding(Path)
                {
                    Converter = _caseConverter,
                    ConverterParameter = ConvertCase,
                    FallbackValue = "%" + Path + "%",
                    Source = Source,
                    StringFormat = StringFormat
                };
        }
    }
}
