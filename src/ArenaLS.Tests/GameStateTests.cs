using ArenaLS.Model;
using NUnit.Framework;

namespace ArenaLS.Tests
{
	[TestFixture]
	class GameStateTests
	{
		GameState GameState;

		[SetUp]
		public void Setup ()
		{
			TestDependencies.SetupTestDependencies ();

			// TestData
			GameState = new GameState ();			
		}

		[TestCase]
		public void AllCharacters_ReturnsExpectedPlayer ()
		{
			Assert.AreEqual ("Player", GameState.GetCharacter (0).Name);
		}

		[TestCase]
		public void AllCharacters_ReturnsExpectedMercenary ()
		{
			Assert.AreEqual ("Character 3", GameState.GetCharacter (4).Name);
		}

		[TestCase]
		public void AllCharacters_ReturnsExpectedEnemy ()
		{
			Assert.AreEqual ("Enemy", GameState.GetCharacter (5).Name);

		}

		[TestCase]
		public void AllCharacters_ReturnsNullWhenOutOfRange ()
		{
			Assert.Null (GameState.GetCharacter (6));
		}
	}
}
