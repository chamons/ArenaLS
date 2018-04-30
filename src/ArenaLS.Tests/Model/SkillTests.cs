using System;
using ArenaLS.Model;
using ArenaLS.Tests.Utilities;
using NUnit.Framework;
using Optional;

namespace ArenaLS.Tests.Model
{
	[TestFixture]
	class SkillTests
	{
		Skill TestSkill;

		[SetUp]
		public void Setup ()
		{
			TestSkill = TestFactory.CreateTestSkill ();
		}

		[Test]
		public void UnderCooldown_WhenUsed ()
		{
			TestSkill.Use (TestFactory.CreateTestCharacter (), Option.None<Character> ());
			Assert.True (TestSkill.UnderCooldown);
			Assert.Greater (TestSkill.CurrentCooldown, 0);
		}

		[Test]
		public void NotUnderCooldown_WhenFresh ()
		{
			Assert.False (TestSkill.UnderCooldown);
			Assert.AreEqual (0, TestSkill.CurrentCooldown);
		}

		[Test]
		public void UsingSkill_WhileOnCooldown_Throws ()
		{
			Assert.Throws<InvalidOperationException> (() =>
			{
				TestSkill.Use (TestFactory.CreateTestCharacter (), Option.None<Character> ());
				TestSkill.Use (TestFactory.CreateTestCharacter (), Option.None<Character> ());
			});
		}

		[Test]
		public void Refresh_ReducesCooldown ()
		{
			TestSkill.Use (TestFactory.CreateTestCharacter (), Option.None<Character> ());
			Assert.AreEqual (TestSkill.SkillCooldown, TestSkill.CurrentCooldown);
			TestSkill.Refresh (5);
			Assert.AreEqual (TestSkill.SkillCooldown - 5, TestSkill.CurrentCooldown);
		}

		[Test]
		public void Refresh_OverCooldown_SetsZero ()
		{
			TestSkill.Use (TestFactory.CreateTestCharacter (), Option.None<Character> ());
			TestSkill.Refresh (500);
			Assert.AreEqual (0, TestSkill.CurrentCooldown);
		}
	}
}
