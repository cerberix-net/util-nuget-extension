using System;

namespace Cerberix.Extension.Core
{
	/// <summary>
	///		Extensions for use with arrays
	/// </summary>
	public static class ArrayExtensions
	{
		/// <summary>
		///		Apply action to each element within array
		/// </summary>
		public static void ForEach<T>(this T[] values, Action<T> action)
		{
			Array.ForEach(values, action);
		}
	}
}
