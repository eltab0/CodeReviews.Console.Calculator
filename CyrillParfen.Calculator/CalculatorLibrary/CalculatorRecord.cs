namespace CalculatorLibrary;

public class CalculatorRecord
{
    public double Num1 { get; set; }
    public double? Num2 { get; set; }
    public string Op { get; set; }
    public double Result { get; set; }

    public override string ToString()
    {
        return Num2.HasValue
            ? $"{Num1} {Op} {Num2} = {Result:0.##}"
            : $"{Op} ({Num1}) = {Result:0.##}";
    }
}
