using System.Collections.Generic;
using System;


namespace IuvoUnity
{
    namespace Extensions
    {
        /// <summary>
        /// Provides generic extension methods for safe invocation, dictionary operations, and string formatting.
        /// </summary>
        public static class GenericExtensions
        {
            /// <summary>
            /// Safely invokes the specified action if the object is not null.
            /// </summary>
            /// <typeparam name="T">The type of the object.</typeparam>
            /// <param name="obj">The object to invoke the action on.</param>
            /// <param name="action">The action to invoke.</param>
            public static void SafeInvoke<T>(this T obj, Action<T> action)
            {
                if (obj != null)
                {
                    action(obj);
                }
            }

            /// <summary>
            /// Adds a key-value pair to the dictionary or updates the value if the key already exists.
            /// </summary>
            /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
            /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
            /// <param name="dictionary">The dictionary to update.</param>
            /// <param name="key">The key to add or update.</param>
            /// <param name="value">The value to set.</param>
            public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
            {
                dictionary[key] = value;
            }

            /// <summary>
            /// Joins a collection of strings into a single string, separated by the specified separator.
            /// Returns a single space if the collection is null.
            /// </summary>
            /// <param name="collection">The string collection to join.</param>
            /// <param name="separator">The separator to use.</param>
            /// <returns>A separated string or a space if the collection is null.</returns>
            public static string ToSeparatedString(this IEnumerable<string> collection, string separator = ", ")
            {
                return collection == null ? " " : string.Join(separator, collection);
            }

        }
    }
}