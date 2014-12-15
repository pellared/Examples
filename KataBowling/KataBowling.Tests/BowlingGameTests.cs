using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KataBowling.Tests
{
    
    [TestClass]
    public class BowlingGameTests
    {
        // Input format:
        // - miss
        // / spare
        // X strike
        private readonly BowlingGame game = new BowlingGame(new ScorecardParser());

        [TestMethod]
        public void does_not_count_points_when_miss()
        {
            TestScoreCalulation("-", 0);
        }

        [TestMethod]
        public void counts_points_for_each_knocked_pin_in_simple_scenario()
        {
            foreach (int digit in Enumerable.Range(1, 9))
            {
                TestScoreCalulation(digit.ToString(), digit);
            }
        }

        [TestMethod]
        public void counts_10_points_when_Strike()
        {
            TestScoreCalulation("X", 10);
        }

        [TestMethod]
        public void sums_points_for_knocked_pins_in_simple_scenario()
        {
            TestScoreCalulation("12", 1 + 2);
        }

        [TestMethod]
        public void counts_points_equal_to_the_remiang_pins_when_Spare()
        {
            TestScoreCalulation("5/", 5 + 5);
        }

        [TestMethod]
        public void calulcates_bonus_points_for_Roll_after_Spare()
        {
            TestScoreCalulation("5/6", 5 + 5 + (6+6));
        }

        [TestMethod]
        public void calulcates_bonus_points_for_two_Rolls_after_Strike()
        {
            TestScoreCalulation("X21", 10 + (2+2) + (1+1));
        }

        [TestMethod]
        public void calulcates_bonus_points_after_Strike_and_Spare()
        {
            TestScoreCalulation("X2/5", 10 + (2+2) + (8+8) + (5+5));
        }

        [TestMethod]
        public void game_consists_of_20_Pointing_Rolls_by_default()
        {
            TestScoreCalulation("9-9-9-9-9-9-9-9-9-9-", 90);
        }

        [TestMethod]
        public void adds_one_more_Bonus_Roll_when_the_last_Roll_was_Spare()
        {
            TestScoreCalulation("5/5/5/5/5/5/5/5/5/5/5", 150);
        }

        [TestMethod]
        public void adds_two_more_Bonus_Rolls_when_the_last_Roll_was_Strike()
        {
            TestScoreCalulation("9-9-9-9-9-9-9-9-9-X11", 93);
        }

        [TestMethod]
        public void each_strike_shortens_the_Pointing_Rolls_by_one()
        {
            TestScoreCalulation("XXXXXXXXXXXX", 300);
        }

        private void TestScoreCalulation(string input, int expectedScore)
        {
            Assert.AreEqual(expectedScore, game.CalculateScore(input));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_InvalidArgumentException_for_invalid_Scorecard_format()
        {
            game.CalculateScore(".");
        }
    }
}
