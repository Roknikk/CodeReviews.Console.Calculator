
using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{

    static void Main(string[] args)
    {
        bool endApp = false;
        int calcutatorUses = 0;
        List<string[]> latestCalculation = new();
  
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        
        Calculator calculator = new Calculator();

        double? numPrevious1 = null;
        double? numPrevious2 = null;

        while (!endApp)
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            double cleanNum1 = 0;
            double cleanNum2 = 0;

            if (numPrevious1 == null)
            {
                Console.Write("Type the first number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }
            }
            else
            {
                cleanNum1 = (double)numPrevious1;
            }

            if (numPrevious2 == null)
            {
                Console.Write("Type the second number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }
            }
            else
            {
                cleanNum2 = (double)numPrevious2;
            }

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tpw - Power");
            Console.WriteLine("\tgnrt - General root (num1 ^ (1/num2))");
            Console.WriteLine("\trmnd - Remainder");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == null || ! Regex.IsMatch(op, "^(a|s|m|d|pw|gnrt|rmnd)$"))
            {
               Console.WriteLine("Error: Unrecognized input.");
            }
            else
            { 
               try
               {
                  result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                  if (double.IsNaN(result))
                  {
                     Console.WriteLine("This operation will result in a mathematical error.\n");
                  }
                  else
                    {   Console.WriteLine("Your result: {0:0.##}\n", result);
                        Console.WriteLine($"You have use the calculator {++calcutatorUses} times.");
                        latestCalculation.Add([cleanNum1.ToString(), op ,cleanNum2.ToString(), result.ToString()]);
                    }

                }
                catch (Exception e)
                {
                   Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            if (latestCalculation.Count() > 0)
            {
                Console.WriteLine("Do you want to see the previous calculations?");
                Console.Write("Write Y for yes, any other key for no: ");

                if (Console.ReadLine() == "y")
                {
                    calculator.ShowLatestCalculations(latestCalculation);
                    Console.WriteLine("\nTo delete the list, type 'delete'. To use a previous result, type 'use'. Type anything else to skip.");
                    Console.Write("Enter your choose: ");

                    switch (Console.ReadLine()?.ToLower())
                    {
                        case "delete":
                            calculator.ClearResultList(latestCalculation);
                            break;
                        
                        case "use":
                            Console.Write("\nDo you want to use the result as the first number or second number (Enter 1 or 2): ");
                            string? resultPosition = Console.ReadLine();
                            if (resultPosition == "1")
                            {
                                numPrevious1 = calculator.GetSelectedResult(latestCalculation);
                            }else if(resultPosition == "2")
                            {
                                numPrevious2 = calculator.GetSelectedResult(latestCalculation);
                            }
                            break;
                        
                        default:
                            numPrevious1 = null;
                            numPrevious2 = null;
                            break;
                    }
                }
                else
                {
                    numPrevious1 = null;
                    numPrevious2 = null;
                }
            }

            Console.WriteLine("");

            if (numPrevious1 == null && numPrevious2 == null)
            {
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
            }

            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }
}