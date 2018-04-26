using System.Collections.Generic;

namespace ArenaLS.Utilities
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Yield<T> (this T item)
		{
			yield return item;
		}
	}
}