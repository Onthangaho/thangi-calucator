public class CalculatorHistoryException : Exception
{
    public CalculatorHistoryException()
    {
    }

    // constructor that accepts a message string parameter for detailed error information
    public CalculatorHistoryException(string message)
        : base("Calculation History is empty")
    {
    }

    
}