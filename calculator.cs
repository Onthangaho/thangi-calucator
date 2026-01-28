using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

/// <summary>
/// Owns calculator behaviour and internal state.
/// 
/// This class:
/// - Performs calculations
/// - Applies business rules
/// - Maintains history
/// 
/// Booking analogy:
/// similar to a booking logic / rules component.
/// </summary>
public class Calculator
{
    /*
     * INTERNAL MUTABLE STATE
     * 
     * This list is intentionally mutable.
     * The calculator changes it over time.
     */
    private readonly List<CalculationRequest> _history = new();

    public string Name { get; }
    public int LastResult { get; private set; }

    public Calculator(string name)
    {
        //first line of defense - guard clause so that invalid calculator cannot be created
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Calculator must have a name.");

        Name = name;
    }

    /*
     * ============================
     * HISTORY ACCESS (COPY)
     * ============================
     * 
     * IMPORTANT DESIGN CHOICE:
     * 
     * We return a COPY of the list,
     * not the internal list itself.
     * 
     * This means:
     * - External code cannot observe live mutation
     * - External code cannot affect internal state
     * - The calculator fully controls its data
     * 
     * Trade-off:
     * - Slightly more memory usage
     * - Stronger safety and predictability
     */
    public IReadOnlyList<CalculationRequest> GetHistory()
    {
        return _history.ToList(); // defensive copy
    }

    /*
     * ============================
     * CORE BEHAVIOUR
     * ============================
     */
    public int Calculate(int a, int b, OperationType operation)
    {
        // Guard clause: fail fast
        if (operation == OperationType.Divide && b == 0)
            throw new InvalidOperationException("Cannot divide by zero.");

        int result = operation switch
        {
            OperationType.Add => a + b,
            OperationType.Subtract => a - b,
            OperationType.Multiply => a * b,
            OperationType.Divide => a / b,
            _ => throw new InvalidOperationException("Invalid operation.")
        };

        // MUTATION happens here (internally only)
        _history.Add(new CalculationRequest(a, b, operation));

        LastResult = result;
        return result;
    }

    /*
     * ============================
     * LINQ AS QUESTIONS
     * ============================
     */

    public bool HasUsedDivision()
    {
        return _history.Any(r => r.Operation == OperationType.Divide);
    }

    public CalculationRequest? GetLastMultiplication()
    {

        
        CalculationRequest request = _history.LastOrDefault(record => record.Operation == OperationType.Multiply)!;


        return request;

    }

    public CalculationRequest GetLastCalculation()
    {
        //guard clause for empty history
        if (_history.Count == 0)
            throw new CalculatorHistoryException();
        CalculationRequest request = _history.Last();
        return request;
    }

    public IEnumerable<CalculationRequest> GetByOperation(OperationType operation)
    {
        return _history.Where(r => r.Operation == operation);
    }

    /*
     * ============================
     * GROUPING WITH DICTIONARY
     * ============================
     */
    public Dictionary<OperationType, List<CalculationRequest>> GroupByOperation()
    {
        var grouped = new Dictionary<OperationType, List<CalculationRequest>>();

        foreach (var request in _history)
        {
            if (!grouped.ContainsKey(request.Operation))
            {
                grouped[request.Operation] = new List<CalculationRequest>();
            }

            grouped[request.Operation].Add(request);
        }

        return grouped;
    }

    public async Task SaveHistoryAsync( string filePath)
    {
        List<CalculationRequest> snapshot = _history.ToList();
        // Serialize the snapshot to JSON so that it can be saved to a file as text 
        //convert list to json string that json understands
        string json = JsonSerializer.Serialize(snapshot);
        // Write the JSON string to the specified file asynchronously so that it does not block the main thread if the file operation takes time 
        await File.WriteAllTextAsync(filePath, json);

    }
    public async Task<List<CalculationRequest>> LoadHistory(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new Exception("This file does not exist");
        }
        
        else
        {
           string json = await File.ReadAllTextAsync(filePath);
           List<CalculationRequest> calculations = [];
            calculations =  JsonSerializer.Deserialize<List<CalculationRequest>>(json)! ?? new List<CalculationRequest>();
           return calculations;
        }
       
    }

   
    
}