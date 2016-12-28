namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    /// <summary>
    ///     Provides extension methods for <see cref="Parameters"/> instances.
    /// </summary>
    public static class ParametersExtensions
    {
        /// <summary>
        ///     Determines whether the <see cref="Parameters"/> instance contains the specified parameter.
        /// </summary>
        /// <param name="parameters">The <see cref="Parameters"/> instance that this extension method affects.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <returns><c>true</c> if the <see cref="Parameters"/> instance contains the specified parameter;
        /// otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameterName"/> is <c>null</c>, empty, or white space.
        /// </exception>
        public static bool ContainsValue(this Parameters parameters, string parameterName)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (parameterName.IsNullOrWhiteSpace())
                throw new ArgumentException(Resources.ValueNullEmptyOrWhiteSpace, nameof(parameterName));

            try
            {
                var parameter = parameters[parameterName];
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Returns the parameter associated with the specified parameter name.
        /// </summary>
        /// <param name="parameters">The <see cref="Parameters"/> instance that this extension method affects.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <returns>The <see cref="Parameter"/> instance associated with <paramref name="parameterName"/>,
        /// or <c>null</c> if no parameter is found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameterName"/> is <c>null</c>, empty, or white space.
        /// </exception>
        public static Parameter TryGetValue(this Parameters parameters, string parameterName)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (parameterName.IsNullOrWhiteSpace())
                throw new ArgumentException(Resources.ValueNullEmptyOrWhiteSpace, nameof(parameterName));

            return parameters.ContainsValue(parameterName) ? parameters[parameterName] : null;
        }
    }
}
