using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataBowling
{
    public class Roll
    {
        public Roll(int points, int bonusRollCount)
        {
            Points = points;
            BonusRollCount = bonusRollCount;
        }

        public Roll(int points)
            : this(points, 0)
        {
        }

        public int Points { get; private set; }

        public int BonusRollCount { get; private set; }
    }
}
