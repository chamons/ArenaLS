using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArenaLS.Model;
using NUnit.Framework;

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
			Cast = new CastSkill (skill, StartingFrame);
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
	}
}
