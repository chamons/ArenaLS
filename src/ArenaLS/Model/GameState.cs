using System.Collections.Generic;
using System.Linq;
using ArenaLS.Platform;

namespace ArenaLS.Model
{
	public class GameState
	{
		ILogger Log;

		public Character PlayerCharacter { get; private set; }
		public List<Character> Mercenaries { get; private set; } = new List<Character> ();
		public List<Character> Enemies { get; private set; } = new List<Character> ();

		public string CurrentMap { get; private set; }

		long LastProcessedFrame = long.MinValue;

		public IEnumerable<Character> PlayerCharacters
		{
			get
			{
				yield return PlayerCharacter;
				foreach (var m in Mercenaries)
					yield return m;
			}
		}

		public IEnumerable<Character> AllCharacters
		{
			get
			{
				foreach (var p in PlayerCharacters)
					yield return p;
				foreach (var e in Enemies)
					yield return e;
			}
		}

		internal GameState ()
		{
			Log = Dependencies.Get<ILogger> ();

			CreateTestData ();
		}

		public Character GetCharacter (int position)
		{
			if (position < 5)
			{
				return PlayerCharacters.ElementAt (position);
			}
			else
			{
				if (Enemies.Count > position - 5)
					return Enemies.ElementAt (position - 5);
				return null;
			}			
		}

		// TestData
		void CreateTestData ()
		{
			PlayerCharacter = new Character ("Player", 0, new Health (100, 100));
			foreach (var skill in new Skill [] { new Skill ("Heal"), new Skill ("Shield"), new Skill ("Fire"), new Skill ("Lightning"), new Skill ("Poison") })
				PlayerCharacter.AddSkill (skill);

			for (int i = 0; i < 4; ++i)
				Mercenaries.Add (new Character ($"Character {i}", i + 1, new Health (100, 100)));
			Enemies.Add (new Character ("Enemy", 5, new Health (100, 100)));

			CurrentMap = "BeachMap";
		}

		bool AlreadyProcessedFrame (long frame) => frame == LastProcessedFrame;

		void CheckSkippedFrames (long frame)
		{
			if (LastProcessedFrame != long.MinValue && frame - LastProcessedFrame != 1)
				Log.Log ($"Skipped frame {frame - LastProcessedFrame}", LogMask.Engine, Servarity.Normal);				
			LastProcessedFrame = frame;
		}

		public void ProcessTick (long frame)
		{
			if (AlreadyProcessedFrame (frame))
				return;

			CheckSkippedFrames (frame);
		}
	}
}
