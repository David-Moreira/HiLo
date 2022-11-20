using Hilo.Core;

using NSubstitute;

namespace Hilo.UnitTests
{
    public class GameEngineTests
    {

        GameEngine sut;

        private readonly IMysteryNumberGenerator _mysteryNumberGenerator = Substitute.For<IMysteryNumberGenerator>();
        private Func<string> _playerInput = Substitute.For<Func<string>>();
        public GameEngineTests()
        {
            sut = new GameEngine(_mysteryNumberGenerator, new HiLoPlayer[] { new("David", _playerInput) }, (output) => { });
        }

        [Fact]

        public async Task GameEngine_ShouldReturnGameResultWinner_WhenNumberIsGuessed()
        {
            _mysteryNumberGenerator.Generate(0, 10).Returns(10);
            _playerInput.Invoke().Returns("10");

            var gameResult = await sut.Start(0, 10);


            Assert.Equal(1, gameResult.Attempts);
            Assert.Equal("David", gameResult.Winner.Name);
            Assert.False(gameResult.Error);
        }


        [Fact]
        public async Task GameEngine_ShouldReturnGameResultError_WhenNoPlayers()
        {
            sut = new GameEngine(_mysteryNumberGenerator, null, (output) => { });

            _mysteryNumberGenerator.Generate(0, 10).Returns(10);
            _playerInput.Invoke().Returns("10");

            var gameResult = await sut.Start(0, 10);


            Assert.True(gameResult.Error);
            Assert.Equal(GameErrorType.NoPlayers, gameResult.ErrorType);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(0, -1)]
        public async Task GameEngine_ShouldReturnGameResultError_WhenInvalidMinMax(int min, int max)
        {
            sut = new GameEngine(_mysteryNumberGenerator, null, (output) => { });

            _mysteryNumberGenerator.Generate(0, 10).Returns(10);
            _playerInput.Invoke().Returns("10");

            var gameResult = await sut.Start(min, max);


            Assert.True(gameResult.Error);
            Assert.Equal(GameErrorType.NoPlayers, gameResult.ErrorType);
        }
    }
}