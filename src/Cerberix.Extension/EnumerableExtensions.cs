using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Cerberix.Extension
{
    /// <summary>
    ///		Extensions for use with enumerations
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Get distinct items by property
        /// </summary>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            //
            // NOTE: taken from http://stackoverflow.com/questions/489258/linqs-distinct-on-a-particular-property
            //

            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        ///		Convert a collection of nullable items to the distinct set of those with values
        /// </summary>
        public static IEnumerable<T> DistinctWithValue<T>(this IEnumerable<T?> enumerable)
            where T : struct
        {
            return
                (enumerable == null)
                    ? null
                    : enumerable
                          .Where(e => e.HasValue)
                          .Cast<T>()
                          .Distinct();
        }

        /// <summary>
        ///     Async wrapper (no predicate)
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> HasAnyAsync<T>(this Task<IEnumerable<T>> enumTask)
        {
			var lookup = await enumTask.ConfigureAwait(false);
			var result = lookup.HasAny();
			return result;
		}

        /// <summary>
        ///     Async wrapper (with predicate)
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> HasAnyAsync<T>(this Task<IEnumerable<T>> enumTask, Func<T, bool> predicate)
        {
			var lookup = await enumTask.ConfigureAwait(false);
			var result = lookup.HasAny(predicate);
			return result;
		}

        /// <summary>
        ///		Examines the enumerable and returns whether one or more items exist. Includes performance optimization for array type objects.
        /// </summary>
        /// <returns></returns>
        public static bool HasAny<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return false;

            var array = enumerable as T[];
            return (array != null)
                ? (array.Length > 0)
                : enumerable.Any();
        }

        /// <summary>
        ///     Examines the enumerable and returns whether one or more items exist by predicate. Includes performance optimization for array type objects.
        /// </summary>
        /// <returns></returns>
        public static bool HasAny<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable == null)
                return false;

            if (predicate == null)
                return HasAny<T>(enumerable);

            return enumerable.Any(predicate);
        }

        /// <summary>
        ///		Examines the enumerable and returns it if it is already an array, otherwise calls "ToArray" and returns the result
        /// </summary>
        public static T[] EnsureArray<T>(this IEnumerable<T> enumerable)
        {
	        return
		        (enumerable == null)
			        ? null
			        : (enumerable as T[] ?? enumerable.ToArray());
        }

        /// <summary>
        ///		Asynchronous wrapper for <see cref="EnsureArray{T}(IEnumerable{T})"/>
        /// </summary>
        public static async Task<T[]> EnsureArrayAsync<T>(this Task<IEnumerable<T>> enumTask)
        {
            return (await enumTask.ConfigureAwait(false)).EnsureArray();
        }

        /// <summary>
        ///		Returns a single-element array containing the given item
        /// </summary>
        [DebuggerStepThrough]
        public static T[] Wrap<T>(this T item)
        {
            return new[] { item };
        }

        /// <summary>
        ///		Asynchronous wrapper for <see cref="Enumerable.First{TSource}(IEnumerable{TSource})"/>
        /// </summary>
        public static async Task<T> FirstAsync<T>(this Task<IEnumerable<T>> enumTask)
        {
	        var lookup = await enumTask.ConfigureAwait(false);
	        var result = lookup.First();
	        return result;
        }

        /// <summary>
        ///		Asynchronous wrapper for <see cref="Enumerable.FirstOrDefault{TSource}(IEnumerable{TSource})"/>
        /// </summary>
        public static async Task<T> FirstOrDefaultAsync<T>(this Task<IEnumerable<T>> enumTask)
        {
			var lookup = await enumTask.ConfigureAwait(false);
			var result = lookup.FirstOrDefault();
			return result;
		}

        /// <summary>
        ///		Asynchronous wrapper for <see cref="Enumerable.Single{TSource}(IEnumerable{TSource})"/>
        /// </summary>
        public static async Task<T> SingleAsync<T>(this Task<IEnumerable<T>> enumTask)
        {
			var lookup = await enumTask.ConfigureAwait(false);
			var result = lookup.Single();
			return result;
		}

        /// <summary>
        ///		Asynchronous wrapper for <see cref="Enumerable.SingleOrDefault{TSource}(IEnumerable{TSource})"/>
        /// </summary>
        public static async Task<T> SingleOrDefaultAsync<T>(this Task<IEnumerable<T>> enumTask)
        {
			var lookup = await enumTask.ConfigureAwait(false);
			var result = lookup.SingleOrDefault();
			return result;
		}
    }
}
