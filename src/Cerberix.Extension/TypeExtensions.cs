using System;

namespace Cerberix.Extension
{
	/// <summary>
	///		Extensions for use with CLR types
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		///		Change Type (handles Nullable types gracefully)
		/// </summary>
		public static TValue ChangeType<TValue>(object value)
		{
			var t = typeof(TValue);
			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				if (value == null)
					return default(TValue);

				t = Nullable.GetUnderlyingType(t);
			}

			return (TValue)Convert.ChangeType(value, t);
		}
	}
}
