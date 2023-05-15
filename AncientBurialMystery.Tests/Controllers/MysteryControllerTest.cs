using AncientBurialMystery.Controllers;
using AncientBurialMystery.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AncientBurialMystery.Tests.Controllers
{
    public class MysteryControllerTest
    {
        private readonly MysteryController _controller;
        private readonly Mock<IMysteryService> _mysteryServiceMock;

        private const int NValue = 1;
        private const int KValue = 1;

        public MysteryControllerTest()
        {
            _mysteryServiceMock = new Mock<IMysteryService>();
            var loggerMock = new Mock<ILogger<MysteryController>>();
            _controller = new MysteryController(_mysteryServiceMock.Object, loggerMock.Object);
        }

        [Fact]
        public void GetNumberInLastRow_WithValidInput_ReturnsOkResult()
        {
            const double LastNumberResult = 8;
            _mysteryServiceMock.Setup(x => x.CalculateLastNumber(NValue, KValue)).Returns(LastNumberResult);

            var result = _controller.GetNumberInLastRow(NValue, KValue);

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(LastNumberResult, okResult.Value);
        }

        [Theory]
        [InlineData(0, KValue)]
        [InlineData(-5, KValue)]
        public void GetNumberInLastRow_WithInvalidN_ReturnsBadRequest(int n, double k)
        {
            var result = _controller.GetNumberInLastRow(n, k);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal("Invalid value for 'n'. 'n' should be a positive integer.", badRequestResult.Value);
        }

        [Fact]
        public void GetNumberInLastRow_WithInvalidK_ReturnsBadRequest()
        {
            const double InvalidK = 1.23456789012;

            var result = _controller.GetNumberInLastRow(NValue, InvalidK);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal($"Invalid floating point digits count for 'k' which must be less than or equal to {Constants.DoubleDigitsRound}.", badRequestResult.Value);
        }

        [Fact]
        public void GetNumberInLastRow_WithLongEnoughKDigits_ReturnsOkResult()
        {
            const double ValidK = 1.2345678901;

            var result = _controller.GetNumberInLastRow(NValue, ValidK);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetNumberInLastRow_WithInfinityResult_ReturnsBadRequest()
        {
            _mysteryServiceMock.Setup(x => x.CalculateLastNumber(NValue, KValue)).Returns(double.PositiveInfinity);

            var result = _controller.GetNumberInLastRow(NValue, KValue);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal($"The calculated number is too large to fit into an available max number: {double.MaxValue}.", badRequestResult.Value);
        }
    }
}