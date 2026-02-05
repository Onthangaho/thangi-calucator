public class InvalidCalculationException : Exception
{
    
    public InvalidCalculationException()
    {
    }

    // constructor that accepts a message string parameter for detailed error information
    public InvalidCalculationException(string message)
        : base(message)
    {
    }
}
