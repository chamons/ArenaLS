using System.Collections.Generic;

namespace ArenaLS.Model
{
	public class Character
	{
		public int Slot { get; private set; }
		public string Name { get; private set; }
		public List<Skill> Skills { get; private set; } = new List<Skill> ();
		public Health Health { get; private set; }

		internal Character (string name, int slot, Health health)
		{
			Slot = slot;
			Name = name;
			Health = health;
		}

		internal void SetHealth (Health h)
		{
			Health = h;
		}

		internal void AddSkill (Skill s)
		{
			Skills.Add (s);
		}
	}
}