using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Cerberix.Extension
{
	/// <summary>
	///		Extensions for use with strings
	/// </summary>
	public static class StringExtensions
	{
		#region Constants/Private

		private const string StripTagsRegexString = @"<[^<]*?>";
		private static readonly Regex StripTagsRegex;

		#endregion Constants/Private

		#region .ctor()

		static StringExtensions()
		{
			StripTagsRegex = new Regex(StripTagsRegexString,
			                           RegexOptions.Compiled | RegexOptions.IgnoreCase |
			                           RegexOptions.CultureInvariant);
		}

		#endregion .ctor()

		/// <summary>
		///		Extend Contains to allow specifying StringComparison
		/// </summary>
		public static bool Contains(this string input, string value, StringComparison comparison)
		{
			if (input == null)
				return false;

			var index = input.IndexOf(value, comparison);
			var result = (index > -1);
			return result;
		}

		/// <summary>
		///		Converts a concatenated CSV string to enumerated set of values (implicit delimiters & options)
		/// </summary>
		public static IEnumerable<string> SplitCsv(this string input, char delimiter = ',')
		{
			if (string.IsNullOrWhiteSpace(input))
				return null;

			var result = input.Split(delimiter.Wrap(), StringSplitOptions.RemoveEmptyEntries);
			return result;
		}

		/// <summary>
		///		Converts a concatenated CSV string to enumerated set of values (explicit delimiters & options)
		/// </summary>
		public static IEnumerable<string> SplitCsv(this string input, char[] delimiters, StringSplitOptions options)
		{
			if (string.IsNullOrWhiteSpace(input))
				return null;

			var result = input.Split(delimiters, options);
			return result;
		}

		/// <summary>
		///		Strip XML, HTML tags from a given input string.
		/// </summary>
		public static string StripTags(this string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				return input;

			var result = StripTagsRegex.Replace(input, string.Empty);
			return result;
		}

		/// <summary>
		///		Converts an enumeration of string values into a concatenated CSV string
		/// </summary>
		public static string ToCsv<T>(this IEnumerable<T> values, char delimiter = ',')
		{
			if (values == null)
				return null;

			var result = string.Join(Convert.ToString(delimiter), values);
			return result;
		}

        /// <summary>
        ///     Converts bytes to hex format string type
        /// </summary>
        public static string ToHex(this byte[] bytes, bool upperCase = false)
        {
            var result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

		/// <summary>
		///		Trim or null clamp given string value
		/// </summary>
		public static string TrimOrDefault(this string input)
		{
			// trim or null clamp //
			return TrimOrDefault(input, null);
		}

		/// <summary>
		///		Trim or pass default string value back
		/// </summary>
		public static string TrimOrDefault(this string input, string defaultValue)
		{
			// default //
			if (string.IsNullOrWhiteSpace(input))
				return defaultValue;

			// trim //
			var result = input.Trim();
			return result;
		}

		/// <summary>
		///		Wrap Substring to return up to the specified length
		/// </summary>
		public static string Truncate(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			var length = value.Length;
			if (maxLength < length)
				return string.Empty;

			var result = value.Substring(0, maxLength);
			return result;
		}
	}
}
