using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaLS
{
	// TestData
	public sealed class Skill
	{
		public readonly string Name;

		public Skill (string name)
		{
			Name = name;
		}

		public bool UnderCooldown => false;
		public int Cooldown => 0;
		public int MaxCooldown => 500;
	}
}
