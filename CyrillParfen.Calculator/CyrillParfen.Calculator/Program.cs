using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram;

internal class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            ShowMenu();

            string? op = Console.ReadLine();

            if (op == "h")
            {
                ShowHistory(calculator);
                continue;
            }

            if (op == "cls")
            {
                calculator.ClearHistory();
                Console.WriteLine("Calculator history has been cleared.\n");
                continue;
            }

            if (op == "q")
            {
                endApp = true;
                break;
            }

            if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|pow|sqrt|10x|sin|cos|tan|cot)$"))
            {
                Console.WriteLine("Error: Unrecognized input.\n");
                continue;
            }

            double cleanNum1 = ReadNumber("Type a number, and then press Enter:");
            double? cleanNum2 = null;

            if (IsBinaryOparation(op))
                cleanNum2 = ReadNumber("Type another number, and then press Enter: ");

            try
            {
                double result = calculator.DoOperation(cleanNum1, cleanNum2, op);

                if (double.IsNaN(result))
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                else
                    Console.WriteLine($"Your result {result:0.##}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine($"Calculator was used {calculator.UsageCount} times");
        }

        calculator.Finish();
    }

    private static bool IsBinaryOparation(string op)
    {
        switch (op)
        {
            case "a":
            case "s":
            case "m":
            case "d":
            case "pow":
                return true;
            default:
                return false;
        }
    }

    private static double ReadNumber(string prompt)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine();
        double result;

        while (!double.TryParse(input, out result))
        {
            Console.WriteLine("This is not valid input. Please enter a numeric value: ");
            input = Console.ReadLine();
        }

        return result;
    }

    private static void ShowMenu()
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        Console.WriteLine("Choose an operation from the following list:");
        Console.WriteLine("\ta - Add (x+y)");
        Console.WriteLine("\ts - Subtract (x-y)");
        Console.WriteLine("\tm - Multiply (x*y)");
        Console.WriteLine("\td - Divide (x/y)");
        Console.WriteLine("\tpow - Power (x^y)");
        Console.WriteLine("\t10x - Power of 10");
        Console.WriteLine("\tsqrt - Square Root");
        Console.WriteLine("\tsin - Sine");
        Console.WriteLine("\tcos - Cosine");
        Console.WriteLine("\ttan - Tangent");
        Console.WriteLine("\tcot - Cotangent");
        Console.WriteLine("\th - Show operations history");
        Console.WriteLine("\tcls - Clear history");
        Console.WriteLine("\tq - Close the calculator");
        Console.Write("Your option? ");
    }

    private static void ShowHistory(Calculator calculator)
    {
        if (calculator.History.Count == 0)
        {
            Console.WriteLine("No records found.\n");
            return;
        }

        Console.WriteLine("\n--- Calculation History ---");

        for ( int i = 0; i < calculator.History.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {calculator.History[i]}");
        }

        Console.WriteLine("-----------------------------");
    }
}
