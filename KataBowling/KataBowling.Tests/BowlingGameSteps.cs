using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace KataBowling.Tests
{
    [Binding]
    public class BowlingGameSteps
    {
        private BowlingGame game;
        private int score;

        [Given(@"a bowling game")]
        public void GivenABowlingGame()
        {
            game = new BowlingGame(new ScorecardParser());
        }

        [When(@"the scorecard is (.*)")]
        public void WhenTheScorecardIs(string scorecard)
        {
            score = game.CalculateScore(scorecard);
        }

        [Then(@"the score is (\d+)")]
        public void ThenTheScoreIs(int expectedScore)
        {
            Assert.AreEqual(expectedScore, score);
        }
    }
}
