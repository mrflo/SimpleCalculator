using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            Calculator calculator = new Calculator();
            ArgumentsParser parser = new ArgumentsParser();

            while (true) // Loop indefinitely
            {

                Console.WriteLine("Enter a number:");
                string strNumber1 = Console.ReadLine();         

                Console.WriteLine("Enter another number:");
                string strNumber2 = Console.ReadLine();

                Console.WriteLine("Enter an operator:");
                string strOperator = Console.ReadLine();

                //Parse inputs
                try
                {
                    CalculationArgument arguments = parser.Parse(strNumber1, strNumber2, strOperator);
                    calculator.Process(arguments);
                    Console.WriteLine("Result: {0}  (press enter to continue or 'exit' to quit)", calculator.Result);
                    // Log calculation using Trace for now, why not use Log4net?
                    Trace.WriteLine(String.Format("Calcul {0} {1} {2} ={3}  time:{4}", arguments.FirstArgument, arguments.Operator, arguments.SecondArgument, calculator.Result, DateTime.Now.ToString()));

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(String.Format("Input error: {0}",ex.Message));
                    Trace.WriteLine(String.Format(String.Format("User Input Error: {0}",ex.Message)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Error:{0}", ex.Message));
                    Trace.WriteLine(String.Format("Other error: {0}",ex.Message));

                }
                
                if(Console.ReadLine()=="exit")
                    break;
            }
        }

            }

    public class Calculator
    {
        public long Result { get; private set; }

        public void Process(CalculationArgument parsedInput)
        {
            Result = parsedInput.FirstArgument;
            
            switch (parsedInput.Operator)
            {
                case "+":
                    Result += parsedInput.SecondArgument;
                    break;

                case "-":
                    Result -= parsedInput.SecondArgument;
                    break;

                case "*":
                    Result *= parsedInput.SecondArgument;
                    break;

                default:
                    //Throw execption or console message
                    throw new ArgumentException("Wrong operator");
                    
            }
        }
    }

    public class CalculationArgument
    {
        public long FirstArgument { get; set; }
        public string Operator { get; set; }
        public long SecondArgument { get; set; }
    }



    
    public class ArgumentsParser
    {
        public CalculationArgument Parse(string arg1, string arg2, string operatorString)
        {
            long firstArgument = 0;
            long secondArgument = 0;

            //Could adapt this in a later stage using RegEx to allow other input than numbers
            if (!Int64.TryParse(arg1, out firstArgument))
            { 
                //throw execption or console mess
                throw new ArgumentException("Argument 1 should be a number");
            }
            if (!Int64.TryParse(arg2, out secondArgument))
            {
                //throw execption or console mess
                throw new ArgumentException("Argument 2 should be a number");
            }

            return new CalculationArgument()
                   {
                       FirstArgument = firstArgument,
                       Operator = operatorString,
                       SecondArgument = secondArgument
                   };
        }
    }
}

