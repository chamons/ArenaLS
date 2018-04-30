using ArenaLS.Model;
using ArenaLS.Tests.Utilities;
using NUnit.Framework;
using Optional;

namespace ArenaLS.Tests.Model
{
	[TestFixture]
	class CastSkillTests
	{
		CastSkill Cast;
		const int StartingFrame = 100;

		[SetUp]
		public void Setup ()
		{
			var skill = TestFactory.CreateTestSkill ();
			Cast = new CastSkill (skill, Option.None<Character> (), StartingFrame);
		}

		[Test]
		public void FinalFrame_BasedOnCastTime ()
		{
			Assert.AreEqual (StartingFrame + Cast.CastTime, Cast.FinalFrame);
		}

		[Test]
		public void CastPercentage ()
		{
			Assert.AreEqual (0, Cast.PercentageCast (StartingFrame));
			Assert.AreEqual (50, Cast.PercentageCast (StartingFrame + (Cast.CastTime / 2)));
			Assert.AreEqual (100, Cast.PercentageCast (StartingFrame + Cast.CastTime));
		}
		[Test]
		public void IsReady ()
		{
			Assert.False (Cast.IsReady (StartingFrame));
			Assert.False (Cast.IsReady (StartingFrame + (Cast.CastTime / 2)));
			Assert.True (Cast.IsReady (StartingFrame + Cast.CastTime));
		}

	}
}
