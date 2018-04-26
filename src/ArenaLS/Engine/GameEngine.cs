using ArenaLS.Platform;
using ArenaLS.Model;

namespace ArenaLS.Engine
{
	public class GameEngine
	{
		public GameState CurrentState { get; private set; }

		public GameEngine (IFileStorage storage)
		{
			Dependencies.Register<ILogger> (typeof (Logger));
			Dependencies.RegisterInstance<IFileStorage> (storage);
			Dependencies.Register<IRandomGenerator> (typeof (RandomGenerator));

			CurrentState = new GameState ();
		}
	}
}
