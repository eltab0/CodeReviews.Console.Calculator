using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculator.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public int UsageCount { get; private set; }
    public List<CalculatorRecord> History { get; private set; } = new List<CalculatorRecord>();

    public double DoOperation(double num1, double? num2, string op)
    {
        UsageCount++;
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        switch (op)
        {
            case "a":
                result = num1 + num2.Value;
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2.Value;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2.Value;
                writer.WriteValue("Multiply");
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2.Value;
                }
                writer.WriteValue("Divide");
                break;
            case "pow":
                result = Math.Pow(num1, num2.Value);
                writer.WriteValue("Power");
                break;
            case "10x":
                result = Math.Pow(10, num1);
                writer.WriteValue("TenPower");
                break;
            case "sqrt":
                result = Math.Sqrt(num1);
                writer.WriteValue("SquareRoot");
                break;
            case "sin":
                result = Math.Sin(double.DegreesToRadians(num1));
                writer.WriteValue("Sine");
                break;
            case "cos":
                result = Math.Cos(double.DegreesToRadians(num1));
                writer.WriteValue("Cosine");
                break;
            case "tan":
                if (num1 % 180 == 90)
                    result = double.NaN;
                else
                    result = Math.Tan(double.DegreesToRadians(num1));
                writer.WriteValue("Tangent");
                break;
            case "cot":
                if (num1 % 180 == 0)
                    result = double.NaN;
                else
                    result = 1.0 / Math.Tan(double.DegreesToRadians(num1));
                writer.WriteValue("Cotangent");
                break;
            default:
                break;
        }

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        if (!double.IsNaN(result))
        {
            History.Add(new CalculatorRecord
            {
                Num1 = num1,
                Num2 = num2,
                Op = op,
                Result = result
            });
        }

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public void ClearHistory () => History.Clear();
}

