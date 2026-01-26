using System;
// See https://aka.ms/new-console-template for more information




Calculator calculator = new Calculator("Thangis Calculator");
Console.WriteLine($"======Welcome to {calculator.Name}======\n");
Console.Write("Enter your name: ");
int num1= int.Parse(Console.ReadLine()!);
Console.Write("Enter an operator (+, -, *, /): ");
char operatorChar = char.Parse(Console.ReadLine()!);
Console.Write("Enter another number: ");
int num2= int.Parse(Console.ReadLine()!);
// Using CalculationRequest record to encapsulate the calculation details so we can easily pass them around without needing multiple parameters.
CalculationRequest request = new CalculationRequest(num1, num2, operatorChar switch
{
    '+' => Calculator.Operation.Add,
    '-' => Calculator.Operation.Subtract,
    '*' => Calculator.Operation.Multiply,
    '/' => Calculator.Operation.Divide,
    _ => throw new InvalidOperationException("Invalid operation")
});
double result = calculator.Calculate(request.Num1, request.Num2, request.Operation);
Console.WriteLine($"Result: {num1} {operatorChar} {num2} = {result}");



    


switch(operatorChar)
{
    case '+':
        result = calculator.Calculate(num1, num2, Calculator.Operation.Add) ;
        break;
    case '-':
        result = calculator.Calculate(num1, num2, Calculator.Operation.Subtract);
        break;
    case '*':
        result = calculator.Calculate(num1, num2, Calculator.Operation.Multiply);
        break;
    case '/':
        try
        {
            result = calculator.Calculate(num1, num2, Calculator.Operation.Divide);
            break;
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    default:
        Console.WriteLine("Invalid operator");
        return;
}
Console.WriteLine($"Result: {num1} {operatorChar} {num2} = {result}");

/*
switch(operatorChar)
{
    case '+':
        result = num1 + num2;
        break;
    case '-':
        result = num1 - num2;
        break;
    case '*':
        result = num1 * num2;
        break;
    case '/':
        result = num1 / num2;
        break;
    default:
        Console.WriteLine("Invalid operator");
        return;
}


Console.WriteLine($"Result: {num1} {operatorChar} {num2} = {result}");*/