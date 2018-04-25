using ArenaLS.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaLS.Engine
{
	public class GameEngine
	{
		public GameEngine (IFileStorage storage)
		{
			Dependencies.Register<ILogger> (typeof (Logger));
			Dependencies.RegisterInstance<IFileStorage> (storage);
			Dependencies.Register<IRandomGenerator> (typeof (RandomGenerator));
		}
	}
}
