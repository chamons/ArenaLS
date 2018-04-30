using System;
using ArenaLS.Platform;

namespace ArenaLS.Tests.Utilities
{
	class TestLogger : ILogger
	{
		public LogMask DiagnosticMask { get => throw new NotImplementedException (); set => throw new NotImplementedException (); }

		public void Log (string message, LogMask mask, Servarity sevarity = Servarity.Normal)
		{
		}

		public void Log (Func<string> messageProc, LogMask mask, Servarity sevarity = Servarity.Normal)
		{
		}
	}

	static class TestDependencies
	{
		internal static void SetupTestDependencies ()
		{
			Dependencies.Clear ();
			Dependencies.Register<ILogger> (typeof (TestLogger));
		}
	}
}
