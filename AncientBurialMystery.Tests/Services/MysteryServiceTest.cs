using AncientBurialMystery.Services;

namespace AncientBurialMystery.Tests.Services
{
    public class MysteryServiceTest
    {
        [Theory]
        [InlineData(3, 1, 8)]
        [InlineData(6, 2, 144)]
        [InlineData(50, 50, 41939771529887740)]
        [InlineData(5, -1, 16)]
        [InlineData(4, -2.2, -5.6)]
        [InlineData(4, 3.4, 39.2)]
        [InlineData(1, 3, 3)]
        [InlineData(9999, 9999, double.PositiveInfinity)]
        public void CalculateLastNumber_ReturnsCorrectResult(int numbersCount, double firstNumber, double expected)
        {
            var mysteryService = new MysteryService();
            double result = mysteryService.CalculateLastNumber(numbersCount, firstNumber);

            Assert.Equal(expected, result);
        }
    }
}