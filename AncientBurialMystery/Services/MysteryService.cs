namespace AncientBurialMystery.Services
{
    public class MysteryService : IMysteryService
    {
        public double CalculateLastNumber(int numbersCount, double firstNumber)
        {
            const int DeviationStep = 2;
            const int NumbersCalculationCount = 2;

            var currentNumber = firstNumber;
            double currentDeviation = 1;

            for (int i = 1; i < numbersCount; i++)
            {
                currentNumber = Math.Round(currentNumber * NumbersCalculationCount + currentDeviation, Constants.DoubleDigitsRound);
                currentDeviation *= DeviationStep;
            }

            return currentNumber;
        }
    }
}
