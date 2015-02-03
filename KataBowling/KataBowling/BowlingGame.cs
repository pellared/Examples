using System.Linq;

namespace KataBowling
{
    public class BowlingGame
    {
        public BowlingGame(IScorecardParser parser)
        {
            Parser = parser;
        }

        public IScorecardParser Parser { get; private set; }

        public int CalculateScore(string input)
        {
            int result = 0;
            Roll[] rolls = Parse(input);
            var pointingRollsCounter = new PointingRollCounter();

            for (int rollIndex = 0; rollIndex < rolls.Length; rollIndex++)
            {
                var currentRoll = rolls[rollIndex];

                if (pointingRollsCounter.IsPointing(currentRoll))
                {
                    result = SumPointsWithBonuses(result, rolls, rollIndex, currentRoll.BonusRollCount);
                }
            }

            return result;
        }

        private Roll[] Parse(string input)
        {
            return Parser.ToRolls(input).ToArray();
        }

        private static int SumPointsWithBonuses(int result, Roll[] rolls, int rollIndex, int bonusRollCount)
        {
            for (int bonusRollOffset = 0; 
                bonusRollOffset <= bonusRollCount && rollIndex + bonusRollOffset < rolls.Length; 
                bonusRollOffset++)
            {
                var pointingRoll = rolls[rollIndex + bonusRollOffset];
                result += pointingRoll.Points;
            }

            return result;
        }

        private class PointingRollCounter
        {
            private int pointingRollsCount = 20;

            public bool IsPointing(Roll roll)
            {
                bool result = pointingRollsCount > 0;

                pointingRollsCount--;
                if (roll.IsStrike)
                {
                    pointingRollsCount--;
                }

                return result;
            }
        }
    }
}
