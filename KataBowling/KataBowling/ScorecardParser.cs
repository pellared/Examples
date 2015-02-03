using System;
using System.Collections.Generic;

namespace KataBowling
{
    public interface IScorecardParser
    {
        IEnumerable<Roll> ToRolls(string input);
    }

    public class ScorecardParser : IScorecardParser
    {
        public IEnumerable<Roll> ToRolls(string input)
        {
            var lastRoll = new Roll(0);
            foreach (char roll in input)
            {
                yield return lastRoll = ParseRoll(roll, lastRoll.Points);
            }
        }

        private static Roll ParseRoll(char roll, int lastRollPoints)
        {
            switch (roll)
            {
                case '-':
                    return new Roll(0);
                case '1':
                    return new Roll(1);
                case '2':
                    return new Roll(2);
                case '3':
                    return new Roll(3);
                case '4':
                    return new Roll(4);
                case '5':
                    return new Roll(5);
                case '6':
                    return new Roll(6);
                case '7':
                    return new Roll(7);
                case '8':
                    return new Roll(8);
                case '9':
                    return new Roll(9);
                case 'X':
                    return new Roll(10, 2);
                case '/':
                    return new Roll(10 - lastRollPoints, 1);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
