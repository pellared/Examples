using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataBowling
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ValidateArguments(args);
                var game = new BowlingGame(new ScorecardParser());
                int result = game.CalculateScore(args[0]);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ValidateArguments(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Invalid usage. Provide the scorecard as a parameter.", "args");
            }
        }
    }
}
