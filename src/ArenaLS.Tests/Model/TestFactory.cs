using ArenaLS.Model;

namespace ArenaLS.Tests.Model
{
	internal static class TestFactory
	{
		public static Skill CreateTestSkill () => new Skill ("Test", 10, 10);
		public static Character CreateTestCharacter () => new Character ("Test", 0, new Health (1, 2));
	}
}
