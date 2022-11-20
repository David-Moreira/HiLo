using Hilo.Core;

namespace Hilo.UnitTests
{
    public class MysteryNumberGeneratorTests
    {

        IMysteryNumberGenerator sut;
        public MysteryNumberGeneratorTests()
        {
            sut = new MysteryNumberGenerator();
        }

        [Theory]
        [InlineData(-50, 500)]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(0,1)]
        [InlineData(1, 100)]
        [InlineData(50, 500)]

        public void Generate_Should_Return_RandomNumberBetweenMinMax(int min, int max)
        {
            var result = sut.Generate(min, max);


            Assert.True(result >= min && result <= max);

        }
    }
}