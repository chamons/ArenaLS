using NUnit.Framework;
using ArenaLS.Tests.Utilities;
using ArenaLS.Model;
using Optional;
using ArenaLS.Engine;

namespace ArenaLS.Tests
{
	[TestFixture]
    public class CombatEngineTests
    {
		CombatEngine CombatEngine;
		Skill Skill;
		GameState State;

		[SetUp]
		public void Setup ()
		{
			TestDependencies.SetupTestDependencies ();

			CombatEngine = new CombatEngine ();
			Skill = TestFactory.CreateTestSkill ();
			State = TestFactory.CreateTestState ();
			State.PlayerCharacter.AddSkill (Skill);
		}

		[TestCase]
		public void CastSkills_AddedToCharacter ()
		{
			CombatEngine.CastSkill (State.PlayerCharacter, Skill, Option.None<Character> (), 10);
			Assert.AreEqual (Skill, State.PlayerCharacter.CastSkill.Skill);
		}

		[TestCase]
		public void CastSkills_NotAddedToCharacter_Throws ()
		{
		}

		[TestCase]
		public void CastSkills_AddedToCharacterWithSkill_Throws ()
		{
			CombatEngine.CastSkill (State.PlayerCharacter, Skill, Option.None<Character> (), 10);
			var secondSkill = TestFactory.CreateTestSkill ();
			State.PlayerCharacter.AddSkill (secondSkill);
			Assert.Throws<System.InvalidOperationException> (() =>
			{
				CombatEngine.CastSkill (State.PlayerCharacter, secondSkill, Option.None<Character> (), 10);
			});
		}

		[TestCase]
		public void CastSkills_AreRemoved_WhenTimeElapses ()
		{
			CombatEngine.CastSkill (State.PlayerCharacter, Skill, Option.None<Character> (), 10);
			Assert.True (State.PlayerCharacter.IsCasting);
			CombatEngine.ProcessThrough (State, 20);
			Assert.False (State.PlayerCharacter.IsCasting);
		}

		[TestCase]
		public void CastSkills_Fire_WhenTimeElapses ()
		{
			bool fired = false;
			var skill = new Skill ("Test", 10, 10, (c, t) => { fired = true; });
			CombatEngine.CastSkill (State.PlayerCharacter, skill, Option.None<Character> (), 10);
			CombatEngine.ProcessThrough (State, 20);
			Assert.True (fired);
		}
	}
}
