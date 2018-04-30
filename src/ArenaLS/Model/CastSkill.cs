using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaLS.Model
{
	public class CastSkill
	{
		public Skill Skill { get; private set; }
		public long StartingFrame { get; private set; }
		public long FinalFrame => StartingFrame + CastTime;

		public string Name => Skill.Name;
		public int CastTime => Skill.CastTime;

		public CastSkill (Skill skill, long frame)
		{
			Skill = skill;
			StartingFrame = frame;
		}

		public int PercentageCast (long frame)
		{
			long framesRemaining = CastTime - (frame - StartingFrame);
			double durationPercentage = ((double)framesRemaining / CastTime);
			return 100 - (int)Math.Round (durationPercentage * 100);
		}
	}
}
