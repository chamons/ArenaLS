using System;

namespace ArenaLS.Engine
{
	public interface IRandomGenerator
	{
		int Roll (int min, int max);
	}

	public class RandomGenerator : IRandomGenerator
	{
		Random Random;

		public RandomGenerator ()
		{
			Random = new Random ();
		}

		public RandomGenerator (int seed)
		{
			Random = new Random (seed);
		}

		public int Roll (int min, int max)
		{
			return Random.Next (min, max);
		}
	}
}
