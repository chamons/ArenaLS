using System;
using Optional;

namespace ArenaLS.Model
{
	public class CastSkill
	{
		public readonly Skill Skill;
		public readonly long StartingFrame;
		public readonly Option<Character> Target;

		public long FinalFrame => StartingFrame + CastTime;

		public string Name => Skill.Name;
		public int CastTime => Skill.CastTime;

		public bool IsReady (long frame) => FinalFrame <= frame;

		public CastSkill (Skill skill, Option<Character> target, long frame)
		{
			Skill = skill;
			StartingFrame = frame;
			Target = target;
		}

		public int PercentageCast (long frame)
		{
			long framesRemaining = CastTime - (frame - StartingFrame);
			double durationPercentage = ((double)framesRemaining / CastTime);
			return 100 - (int)Math.Round (durationPercentage * 100);
		}
	}
}
