using System;

namespace Cerberix.Extension
{
	/// <summary>
	///		Common extensions for DateTime objects
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		///		Go through the legwork of determining the difference between date times reported in years
		///		This also takes the Gregorian calendar into consideration (e.g. adjustments for leap days)
		/// </summary>
		public static uint GetDifferenceInYears(this DateTime first, DateTime second)
		{
			if (first == second)
				return 0;

			var span = (first > second) ? first - second : second - first;

			// adjust for Gregorian calendar //
			var zeroTime = new DateTime(1, 1, 1);
			var result = Convert.ToUInt32((zeroTime + span).Year) - 1;

			return result;
		}
	}
}
