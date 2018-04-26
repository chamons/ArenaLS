using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArenaLS.Model;

namespace ArenaLS.Tests.Model
{
	internal static class TestFactory
	{
		public static Skill CreateTestSkill () => new Skill ("Test", 10, 10);
		public static Character CreateTestCharacter () => new Character ();
	}
}
