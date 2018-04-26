using ProtoBuf;

namespace ArenaLS.Model
{
	[ProtoContract]
	public class Health
	{
		[ProtoMember (1)]
		public int Current { get; private set; }

		[ProtoMember (2)]
		public int Maximum { get; private set; }

		public Health (int health) : this (health, health)
		{
		}

		public Health (int current, int max)
		{
			Current = current;
			Maximum = max;
		}

		public override string ToString () => $"{Current}/{Maximum}";
	}
}
