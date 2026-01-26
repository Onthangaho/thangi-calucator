public class Calculator
{
    private string name;
    public string Name
    {
        get
        {
            name = string.IsNullOrWhiteSpace(name) ? "Default Calculator" : name;
            return name;


        }
        set
        {
            name = value;
        }

    }
    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    public double Result { get; set; }

    public Calculator(string name)
    {
        Name = name;

    }
    public Calculator()
    {
        Name = "Default Calculator";
    }

    public double Calculate(int num1, int num2, Operation operation)
    {
        switch (operation)
        {
            case Operation.Add:
                return num1 + num2;
            case Operation.Subtract:
                return num1 - num2;
            case Operation.Multiply:
                return num1 * num2;
            case Operation.Divide:
                if (num2 == 0)
                {
                    throw new DivideByZeroException("Cannot divide by zero");
                }
                return (double)num1 / num2;
            default:
                throw new InvalidOperationException("Invalid operation");
        }
    }
}