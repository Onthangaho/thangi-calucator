using System;
using System.IO;
using System.Threading.Tasks;

// See https://aka.ms/new-console-template for more information



static async Task Main(string[] args)
{


Console.WriteLine("=== Calculator – End of Day 2 (Defensive Copy Version) ===");

try
{
    var calculator = new Calculator("Training Calculator");
    CalculationRequest req = calculator.GetLastCalculation();
   await calculator.SaveHistoryAsync("calculationHistory.json");
   Console .WriteLine("Calculation history saved successfully.");
    //Console.WriteLine($"This the last operation: {req.A} {req.Operation} {req.B}");
}
catch (Exception ex)
{
    Console.WriteLine("Error Occurred: " + ex.Message);
}
/*
var calculator = new Calculator("Training Calculator");

calculator.Calculate(10, 5, OperationType.Add);
calculator.Calculate(20, 4, OperationType.Divide);
calculator.Calculate(7, 3, OperationType.Multiply);

// ---------------------------------
// HISTORY ACCESS (COPY)
// ---------------------------------
var historySnapshot = calculator.GetHistory();

Console.WriteLine("\n--- Calculation History (Snapshot) ---");
foreach (var request in historySnapshot)
{
    Console.WriteLine($"{request.A} {request.Operation} {request.B}");
}

// Even if someone modifies this list,
// it does NOT affect the calculator.
// (Try clearing it to prove the point.)
// historySnapshot.Clear(); // <-- does NOT break the calculator

Console.WriteLine("\n--- Business Questions ---");
Console.WriteLine($"Has division been used? {calculator.HasUsedDivision()}");

var last = calculator.GetLastCalculation();
if (last != null)
{
    Console.WriteLine($"Last calculation: {last.A} {last.Operation} {last.B}");
}

Console.WriteLine("\n=== End ===");*/
}