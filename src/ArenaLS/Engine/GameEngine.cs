using ArenaLS.Platform;
using ArenaLS.Model;
using System;
using System.Collections.Generic;

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

			CreateTestState ();
		}

		// TestData
		public void CreateTestState ()
		{
			Func<string, Skill> createSkill = name => new Skill (name, 200, 200, (c, t) => { });

			var player = new Character ("Player", 0, new Health (100, 100));
			foreach (var skill in new Skill [] { createSkill ("Heal"), createSkill ("Shield"), createSkill ("Fire"), createSkill ("Lightning"),
			createSkill ("Poison") })
				player.AddSkill (skill);

			List<Character> mercenaries = new List<Character> ();
			for (int i = 0; i < 4; ++i)
				mercenaries.Add (new Character ($"Character {i}", i + 1, new Health (100, 100)));
			List<Character> enemies = new List<Character> ();
			enemies.Add (new Character ("Enemy", 5, new Health (100, 100)));

			CurrentState = GameState.Create (player, mercenaries, enemies, "BeachMap");
		}
	}
}
