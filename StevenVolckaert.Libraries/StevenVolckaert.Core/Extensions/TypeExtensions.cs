using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StevenVolckaert
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns a value that indicates whether the type is a System.Boolean or nullable System.Boolean.
        /// </summary>
        /// <param name="type">The System.Type instance this extension method affects.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is a System.Boolean or nullable System.Boolean; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static bool IsBoolean(this Type type)
        {
            return type.Is<Boolean>();
        }

        /// <summary>
        /// Returns a value that indicates whether the type is a System.Decimal or nullable System.Decimal.
        /// </summary>
        /// <param name="type">The System.Type instance this extension method affects.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is a System.Decimal or nullable System.Decimal; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static bool IsDecimal(this Type type)
        {
            return type.Is<Decimal>();
        }

        /// <summary>
        /// Returns a value that indicates whether the type is a System.Int32 or nullable System.Int32.
        /// </summary>
        /// <param name="type">The System.Type instance this extension method affects.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is a System.Int32 or nullable System.Boolean; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static bool IsInt32(this Type type)
        {
            return type.Is<Int32>();
        }

        private static bool Is<T>(this Type type)
            where T : struct
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return type.Equals(typeof(T)) || type.Equals(typeof(Nullable<T>));
        }

        /// <summary>
        /// Returns a value that indicates whether the type is a nullable type.
        /// </summary>
        /// <param name="type">The System.Type instance this extension method affects.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is nullable; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static bool IsNullable(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Returns a dictionary containing the name and metadata of all public properties of the specified type, recursively.
        /// </summary>
        /// <param name="type">The System.Type instnace this extension method affects.</param>
        /// <returns>A System.Collections.Generic.IDictionary&lt;System.String, System.Reflection.PropertyInfo&gt;
        /// containing the name and metadata of all public properties of the specified type and its children.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static IDictionary<String, PropertyInfo> PropertyMetadata(this Type type)
        {
            return type.PropertyMetadata(parent: null);
        }

        /// <summary>
        /// Returns a dictionary containing the name and metadata of all public properties of the specified type, recursively.
        /// </summary>
        /// <param name="type">The System.Type instance this extension method affects.</param>
        /// <param name="parent">The name of the parent that contains <paramref name="type"/>.</param>
        /// <returns>A System.Collections.Generic.IDictionary&lt;System.String, System.Reflection.PropertyInfo&gt;
        /// containing the name and metadata of all public properties of the specified type and its children.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static IDictionary<String, PropertyInfo> PropertyMetadata(this Type type, String parent)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return GetPropertyMetadata(type, parent).ToDictionary(x => x.Key, x => x.Value);
        }

        private static IEnumerable<KeyValuePair<String, PropertyInfo>> GetPropertyMetadata(Type type, String parent)
        {
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var name = parent == null
                    ? propertyInfo.Name
                    : parent + "." + propertyInfo.Name;

                if ((propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(String)) || propertyInfo.PropertyType.IsInterface)
                    foreach (var value in propertyInfo.PropertyType.PropertyMetadata(name))
                        yield return value;
                else
                    yield return new KeyValuePair<String, PropertyInfo>(
                        key: name,
                        value: propertyInfo
                    );
            }
        }

        /// <summary>
        /// Returns the metadata of all public properties of the specified type that have a specified attribute.
        /// </summary>
        /// <param name="type">The System.Type instance this extension method affects.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>A System.Collections.Generic.IEnumerable&lt;System.Reflection.PropertyInfo&gt; containing
        /// the metadata of all public properties of the type that have the specified attribute.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="attributeType"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="attributeType"/> is not derived from System.Attribute.</exception>
        public static IEnumerable<PropertyInfo> PropertyMetadata(this Type type, Type attributeType)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (attributeType == null)
                throw new ArgumentNullException("attributeType");

            return type.GetProperties().Where(propertyInfo => Attribute.IsDefined(propertyInfo, attributeType));
        }

        /// <summary>
        /// Returns the names of all public properties of the specified type that have a specified attribute.
        /// </summary>
        /// <param name="type">The System.Type instance this extension method affects.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>A System.Collections.Generic.IEnumerable&lt;System.String&gt; containing
        /// The names of all public properties of the type that have the specified attribute.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="attributeType"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="attributeType"/> is not derived from System.Attribute.</exception>
        public static IEnumerable<string> PropertyNames(this Type type, Type attributeType)
        {
            return type.PropertyMetadata(attributeType).Select(propertyInfo => propertyInfo.Name);
        }
    }
}
