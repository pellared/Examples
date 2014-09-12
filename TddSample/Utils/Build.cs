using Ploeh.AutoFixture;

namespace Utils
{
    public static class Build
    {
        private readonly static Fixture Fixture = new Fixture();
        
        public static T Any<T>()
        {
            return Fixture.Create<T>();
        }
    }
}
