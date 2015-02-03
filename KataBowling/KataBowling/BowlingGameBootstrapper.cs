namespace KataBowling
{
    public static class BowlingGameBootstrapper
    {
        public static BowlingGame Create()
        {
            return new BowlingGame(new ScorecardParser());
        }
    }
}