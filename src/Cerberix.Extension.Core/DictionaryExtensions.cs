using System.Collections.Generic;

namespace Cerberix.Extension.Core
{
	/// <summary>
	///		Extensions for use with dictionarys
	/// </summary>
	public static class DictionaryExtensions
	{
		#region Private

		/// <summary>
		///		Tries to get the value from the dictionary specified by lookupValue, returns defaultValue if not found
		/// </summary>
		private static TValue TryGet<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey lookupValue, TValue defaultValue, out bool valueFound)
		{
			valueFound = false;

			if (dictionary == null) return defaultValue;
			if (((object)lookupValue) == null || lookupValue.Equals(default(TKey)) || !dictionary.ContainsKey(lookupValue))
				return defaultValue;

			TValue actualValue;

			valueFound = dictionary.TryGetValue(lookupValue, out actualValue);

			return valueFound ? actualValue : defaultValue;
		}

		#endregion Private

		/// <summary>
		///		Tries to get the integer value from the dictionary, returns a null if the value is not found
		/// </summary>
		public static int? TryGet<TKey>(this IDictionary<TKey, int> dictionary, TKey lookupValue)
		{
			bool valueFound;
			var returnValue = TryGet(dictionary, lookupValue, 0, out valueFound);
			return valueFound ? (int?)returnValue : null;
		}

		/// <summary>
		///		Tries to get the generic value from the dictionary, returns the default if the value is not found
		/// </summary>
		public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey lookupValue)
		{
			bool valueFound;
			return TryGet(dictionary, lookupValue, default(TValue), out valueFound);
		}

		/// <summary>
		///		Tries to get the generic value from the dictionary, returns the default if the value is not found
		/// </summary>
		public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey? lookupValue)
			where TKey : struct
		{
			bool valueFound;
			return TryGet(dictionary, lookupValue.HasValue ? lookupValue.Value : default(TKey), default(TValue), out valueFound);
		}

		/// <summary>
		///		Tries to get the generic value from the dictionary, returns defaultValue if the value is not found
		/// </summary>
		public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey lookupValue, TValue defaultValue)
		{
			bool valueFound;
			return TryGet(dictionary, lookupValue, defaultValue, out valueFound);
		}
	}
}
