namespace Tenisu.Application.Exceptions
{
    public class StatisticsCalculationException : Exception
    {
        public StatisticsCalculationException() { }

        public StatisticsCalculationException(string message) : base(message) { }

        public StatisticsCalculationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
