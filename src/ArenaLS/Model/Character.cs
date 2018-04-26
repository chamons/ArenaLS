using System.Collections.Generic;

namespace ArenaLS.Model
{
	public class Character
	{
		public List<Skill> Skills { get; private set; } = new List<Skill> ();

		internal Character ()
		{

		}

		internal void AddSkill (Skill s)
		{
			Skills.Add (s);
		}
	}
}