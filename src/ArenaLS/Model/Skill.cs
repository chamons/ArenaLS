using System;

namespace ArenaLS.Model
{
	public class Skill
	{
		public readonly string Name;
		public readonly int CastTime;
		public readonly int SkillCooldown;

		public int CurrentCooldown { get; private set; } = 0;
		public bool UnderCooldown => CurrentCooldown > 0;

		// TestData
		public Skill (string name) : this (name, 200, 200)
		{
		}

		internal Skill (string name, int castTime, int skillCooldown)
		{
			Name = name;
			CastTime = castTime;
			SkillCooldown = skillCooldown;
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
