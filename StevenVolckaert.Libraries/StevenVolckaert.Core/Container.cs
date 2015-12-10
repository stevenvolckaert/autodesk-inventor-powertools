using System.ComponentModel.Composition.Hosting;

namespace StevenVolckaert
{
    /// <summary>
    /// Represents the application's dependency injection (IoC) container.
    /// </summary>
    public static class Container
    {
        public static CompositionContainer Current { get; set; }
    }
}
