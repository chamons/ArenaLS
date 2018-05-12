using System;
using Optional;

namespace ArenaLS.Model
{
	public class Skill
	{
		public readonly string Name;
		public readonly int CastTime;
		public readonly int SkillCooldown;
		public readonly Action<Character, Option<Character>> Action;

		public int CurrentCooldown { get; private set; } = 0;
		public bool UnderCooldown => CurrentCooldown > 0;

		internal Skill (string name, int castTime, int skillCooldown, Action<Character, Option<Character>> action)
		{
			Name = name;
			CastTime = castTime;
			SkillCooldown = skillCooldown;
			Action = action;
		}

		internal void Use ()
		{
			if (CurrentCooldown != 0)
				throw new InvalidOperationException ($"Attmpted to use Skill {Name} but CurrentCooldown was {CurrentCooldown}.");
			CurrentCooldown = SkillCooldown;
		}

		internal void Refresh (int v)
		{
			CurrentCooldown -= v;
			if (CurrentCooldown < 0)
				CurrentCooldown = 0;
		}
	}
}
