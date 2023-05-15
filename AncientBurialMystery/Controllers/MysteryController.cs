using AncientBurialMystery.Services;
using Microsoft.AspNetCore.Mvc;

namespace AncientBurialMystery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MysteryController : ControllerBase
    {
        private readonly IMysteryService _mysteryService;
        private readonly ILogger<MysteryController> _logger;

        public MysteryController(IMysteryService mysteryService, ILogger<MysteryController> logger)
        {
            _mysteryService = mysteryService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetNumberInLastRow(int n, double k)
        {
            _logger.LogInformation("GetNumberInLastRow endpoint called with n={n} and k={k}", n, k);

            if (n <= 0)
            {
                return CreateBadRequestResponse("Invalid value for 'n'. 'n' should be a positive integer.");
            }

            string firstNumberString = k.ToString("R");
            if (firstNumberString.Length - firstNumberString.IndexOf('.') - 1 > Constants.DoubleDigitsRound)
            {
                return CreateBadRequestResponse($"Invalid floating point digits count for 'k' which must be less than or equal to {Constants.DoubleDigitsRound}.");
            }

            var lastRowNumber = _mysteryService.CalculateLastNumber(n, k);
            if (double.IsInfinity(lastRowNumber))
            {
                return CreateBadRequestResponse($"The calculated number is too large to fit into an available max number: {double.MaxValue}.");
            }

            _logger.LogInformation("GetNumberInLastRow completed successfully. Result: {lastRowNumber}", lastRowNumber);

            return Ok(lastRowNumber);
        }

        private BadRequestObjectResult CreateBadRequestResponse(string errorMessage)
        {
            _logger.LogWarning(errorMessage);
            return BadRequest(errorMessage);
        }
    }
}