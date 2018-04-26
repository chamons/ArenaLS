using System.Collections.Generic;

namespace ArenaLS.Model
{
	public class Character
	{
		public int Slot { get; private set; }
		public string Name { get; private set; }
		public List<Skill> Skills { get; private set; } = new List<Skill> ();

		internal Character (string name, int slot)
		{
			Slot = slot;
			Name = name;
		}

		internal void AddSkill (Skill s)
		{
			Skills.Add (s);
		}
	}
}