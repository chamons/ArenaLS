using System;
using System.Linq;
using ArenaLS.Model;
using Optional;

namespace ArenaLS.Engine
{
	public interface ICombatEngine
	{
		void CastSkill (Character c, Skill s, Option<Character> target, long frame);
		void ProcessThrough (GameState state, long frame);
	}

	public class CombatEngine : ICombatEngine
	{
		public void CastSkill (Character c, Skill s, Option<Character> target, long frame)
		{
			if (c.IsCasting)
				throw new InvalidOperationException ($"Character {c.Name} attempted to cast {s.Name} when {c.CastSkill.Name} is already being cast");

			c.CastSkill = new CastSkill (s, target, frame);
		}

		public void ProcessThrough (GameState state, long frame)
		{
			foreach (Character invoker in state.AllCharacters.Where (x => x.IsCasting && x.CastSkill.IsReady (frame)))
			{
				// HACK - All sorts of wrong with dots.
				invoker.CastSkill.Skill.Use (invoker, invoker.CastSkill.Target);
				invoker.CastSkill = null;
			}
		}
	}
}
