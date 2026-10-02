
using Newtonsoft.Json;


namespace CalculatorLibrary;

public class Calculator{

  JsonWriter writer;

  public Calculator()
  {
    StreamWriter logFile = File.CreateText("Calculatorlog.json");
    logFile.AutoFlush = true;
    writer = new JsonTextWriter(logFile);
    writer.Formatting = Formatting.Indented;
    writer.WriteStartObject();
    writer.WritePropertyName("Operations");
    writer.WriteStartArray();
  }

  public double DoOperation(double num1, double num2, string op)
  {
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
          result = num1 + num2;
          writer.WriteValue("Add");
          break;

      case "s":
          result = num1 - num2;
          writer.WriteValue("Substract");
          break;

      case "m":
          result = num1 * num2;
          writer.WriteValue("Multiply");
          break;

      case "d":
        if (num2 != 0)
        {
          result = num1 / num2;
          writer.WriteValue("Divide");
        }
        else
        {
          writer.WriteValue("Undefined");
        }
          break;

      case "pw":
        result = Math.Pow(num1, num2);
        writer.WriteValue("Power");
        break;

      case "gnrt":
        if (num2 != 0)
        {
          result = Math.Pow(num1, 1.0 / num2);
          writer.WriteValue("General Root");
        }
        else
        {
          writer.WriteValue("Undefined");
        }
        break;

      case "rmnd":
          if (num2 != 0)
          {
            result = num1 % num2;
            writer.WriteValue("Remainder");
          }
          else
          {
            writer.WriteValue("Undefined (Remainder)");
          }
          break;
    }

    writer.WritePropertyName("Result");
    writer.WriteValue(result);
    writer.WriteEndObject();
    return result;
  }

  public void Finish()
  {
    writer.WriteEndArray();
    writer.WriteEndObject();
    writer.Close();
  }

  string ParseOperationSign(string op)
  {
    switch (op)
    {
      case "a":
        return " + ";

      case "s":
        return " - ";

      case "m":
        return " x ";

      case "d":
        return " / ";

      case "pw":
        return "^";

      case "gnrt":
        return "^1/";

      case "rmnd":
        return " % ";

      default:
        return "?"; 
    }
  }

  public void ShowLatestCalculations(List<string[]> calculations)
  {

    for (int i = 0; i < calculations.Count; i++)
    {
        Console.WriteLine($"{i+1}. {calculations[i][0]}{ParseOperationSign(calculations[i][1])}{calculations[i][2]} = {calculations[i][3]}");
    }
  }

  public void ClearResultList (List<string[]> calculations)
  {
    Console.WriteLine("\nList cleared.");
    calculations.Clear();
  }

  public double GetSelectedResult(List<string[]> calculations)
  {
    Console.Write("Enter the index of the result you want to use: ");

    while (true)
    {
      string ? stringIndex = Console.ReadLine();

      if (int.TryParse(stringIndex, out int index) && index > 0 && index <= calculations.Count)
      {
        return Convert.ToDouble(calculations[index-1][3]);
      }
      else
      {
        Console.Write("Result does not exist. Try with other result: ");
        continue;
      }      
    }
  }

}
