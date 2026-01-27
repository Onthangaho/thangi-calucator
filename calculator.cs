/// <summary>
/// This class represents the DOMAIN BEHAVIOUR.
/// 
/// In real systems:
/// - This is where rules live
/// - This is where decisions are made
/// 
/// In the booking system, this is similar to:
/// - Booking management logic
/// </summary>
public class Calculator
{
    /// <summary>
    /// This property stores state INSIDE the object.
    /// 
    /// Notice:
    /// - Public getter
    /// - Private setter
    /// 
    /// This means:
    /// - Other code can read the value
    /// - Only the Calculator can change it
    /// 
    /// This protects the object from invalid changes.
    /// </summary>

    private readonly List<CalculationRequest> _calculationHistory = new();


    public IReadOnlyList<CalculationRequest> CalculationHistory { get { return _calculationHistory.AsReadOnly(); } }
    public int LastResult { get; private set; }

    /// <summary>
    /// Every calculator must have a name.
    /// 
    /// Constructors define what MUST exist
    /// for an object to be valid.
    /// </summary>
    public string Name { get; }

    public Calculator(string name)
    {
        // Guard clause:
        // We do NOT allow invalid objects to exist.
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Calculator must have a name");

        Name = name;
    }
    // Provide a method to get a copy of the calculation history as a read-only list so that external code cannot modify the internal list
    public IReadOnlyList<CalculationRequest> GetCopyOfCalculationHistory()
    {
        return _calculationHistory.ToList().AsReadOnly();
    }
    /// <summary>
    /// This method applies business rules.
    /// 
    /// It does NOT:
    /// - Read from the console
    /// - Print output
    /// 
    /// This separation is important because:
    /// - Console apps are temporary
    /// - Business logic must survive
    /// 
    /// In the booking system:
    /// - This would decide if a booking is allowed
    /// - This would enforce capacity rules
    /// </summary>
    public int Calculate(int a, int b, OperationType operation)
    {
        // Switch expression ensures ALL enum cases are handled
        LastResult = operation switch
        {
            OperationType.Add => a + b,
            OperationType.Subtract => a - b,
            OperationType.Multiply => a * b,
            OperationType.Divide => a / b,

            // This should never happen if enums are used correctly
            _ => throw new InvalidOperationException("Invalid operation")
        };
        // Store calculation in history as a record after calculation has been performed
        CalculationRequest request = new CalculationRequest(a, b, operation);
        _calculationHistory.Add(request);

        return LastResult;
    }
    //Get the additions operations that were done on this calculator using LINQ query syntax
    public List<CalculationRequest> GetAdditionOperationsLinq()
    {
        List<CalculationRequest> additions = (from record in _calculationHistory
                                              where record.Operation == OperationType.Add
                                              select record).ToList();
        return additions;
    }
    //Get the additions operations that were done on this calculator using LINQ and lambda expression
    public List<CalculationRequest> GetAdditionOperations()
    {
        return _calculationHistory.Where(r => r.Operation == OperationType.Add).ToList();
    }

    public List<CalculationRequest> ReturnDivide()
    {
        var divisions = _calculationHistory.Where(r => r.Operation == OperationType.Divide).ToList();
        return divisions;
    }

    //Get the additions operations using a loop
    public List<CalculationRequest> GetAdditionOperationsLoop()
    {
        List<CalculationRequest> additions = new List<CalculationRequest>();
        foreach (var record in _calculationHistory)
        {
            if (record.Operation == OperationType.Add)
            {
                additions.Add(record);
            }
        }
        return additions;
    }


    // has division ever been used
    public bool HasDivisionBeenUsed()
    {
        bool isUsed = false;
        foreach (var record in _calculationHistory)
        {
            if (record.Operation == OperationType.Divide)
            {
                isUsed = true;
            }
        }
        return isUsed;
    }

    //has division ever been used using LINQ
    public bool HasDivisionBeenUsedLinq()
    {
        bool hasDivision = _calculationHistory.Any(r => r.Operation == OperationType.Divide);
        return hasDivision;
    }

    //writing custom Exceptions
    public double Divide(CalculationRequest request1)
    {
        if (request1.B == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        return (double)request1.A / request1.B;
    }

    // Group our caclulation requests by operation type
    public Dictionary<OperationType, List<CalculationRequest>> GroupCalculationsByOperation()
    {
        Dictionary<OperationType, List<CalculationRequest>> groupingCalculationsRequests = new Dictionary<OperationType, List<CalculationRequest>>();
        foreach (CalculationRequest record in _calculationHistory)
        {
            if (!groupingCalculationsRequests.ContainsKey(record.Operation))
            {
                groupingCalculationsRequests[record.Operation] = new List<CalculationRequest>();
            }
            groupingCalculationsRequests[record.Operation].Add(record);
        }
        return groupingCalculationsRequests;
    }
}