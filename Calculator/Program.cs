using System;
using System.ComponentModel;
using System.Dynamic;
using System.Text;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            GetCalculate();
            Console.ReadKey();
        }
        public static void GetCalculate()
        {
            GetStartingWords();
            while (true)
            {
                decimal value1 = GetNumber();
                string operation1 = GetOperation();
                decimal value2;

                decimal result;
                if (operation1 == "√" || operation1 == "sqrt")
                {
                    Console.CursorTop--;
                    Console.WriteLine("\t     √   ");

                    while (true)
                    {
                        try
                        {
                            result = (decimal)Math.Sqrt((double)value1);
                            break;
                        }
                        catch (OverflowException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("    - Błąd pierwiastkowania. Podaj inną liczbę -");
                            Console.ForegroundColor = ConsoleColor.White;

                            value1 = GetNumber();
                        }
                    }
                }
                else if (operation1 == "/" || operation1 == "÷")
                {
                    Console.CursorTop--;
                    Console.WriteLine("\t     ÷   ");
                    Console.CursorTop--;
                    value2 = GetPercentage(out bool isPercentage);

                    if (isPercentage)
                    {
                        value2 = value1 * (value2 / 100);
                        result = GetResult(value1, value2, operation1);
                    }
                    else
                    {
                        result = GetResult(value1, value2, operation1);
                    }
                }
                else
                {
                    Console.CursorTop--;
                    value2 = GetPercentage(out bool isPercentage);

                    if (isPercentage)
                    {
                        value2 = value1 * (value2 / 100);
                        result = GetResult(value1, value2, operation1);
                    }
                    result = GetResult(value1, value2, operation1);
                }
                GetEndingWords(result);
            }
        }
        public static void GetStartingWords()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.Write("\t\t\tCalculator by ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ozzy");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------");
            Console.Write("> Obsługiwane operacje ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("|+| |-| |*| |÷| |√| |%| |^|\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("> Aby obliczyć pierwiastek kwadratowy wystarczy ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("'√' ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("lub ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("'sqrt'\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("> Aby wkleić tekst należy wcisnąć prawy przycisk myszy");
            Console.Write("> ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("'CTRL + C' ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("aby zakończyć");
            Console.WriteLine();
            Console.WriteLine("1. Podaj liczbę + ENTER");
            Console.WriteLine("2. Podaj operację + ENTER");
            Console.WriteLine("3. Podaj liczbę + ENTER");
            Console.WriteLine("4. Wynik");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();
        }
        public static void GetEndingWords(decimal result)
        {
            Console.WriteLine($"\t\t\b\b\b=  {result}");
            Console.WriteLine("\n---------------------------------------------------------------\n");
        }
        public static decimal GetNumber()
        {
            bool isValidInput = false;
            decimal number = 0;

            while (!isValidInput)
            {
                Console.Write("\t\t");
                isValidInput = decimal.TryParse(Console.ReadLine(), out number);

                if (!isValidInput)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("    - Nieobsługiwany format liczby spróbuj ponownie -");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
            }
            return number;
        }
        public static string GetOperation()
        {
            while (true)
            {
                Console.Write("\t\t\b\b\b");
                string input = Console.ReadLine()!;
                switch (input)
                {
                    case "+":
                        return "+";
                    case "-":
                        return "-";
                    case "*":
                        return "*";
                    case "/":
                    case "÷":
                        return "/";
                    case "√":
                    case "sqrt":
                        return "√";
                    case "%":
                        return "%";
                    case "^":
                        return "^";
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"    - Nieobsługiwana operacja, spróbuj ponownie -");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
        }
        public static decimal GetResult(decimal value1, decimal value2, string operation)
        {
            while (true)
            {
                try
                {
                    switch (operation)
                    {
                        case "+":
                            return checked(value1 + value2);
                        case "-":
                            return checked(value1 - value2);
                        case "*":
                            return checked(value1 * value2);
                        case "/":
                        case "÷":
                            return checked(value1 / value2);
                        case "%":
                            return checked(value1 / 100 * value2);
                        case "^":
                            double value1d = Convert.ToDouble(value1);
                            double value2d = Convert.ToDouble(value2);
                            return checked((decimal)Math.Pow(value1d, value2d));
                    }
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("    - Wystąpiło przepełnienie. Podaj mniejszą liczbę-");
                    Console.ForegroundColor = ConsoleColor.White;

                    value2 = GetNumber();
                }
                catch (DivideByZeroException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("    - Błąd dzielenia przez 0. Podaj inną liczbę -");
                    Console.ForegroundColor = ConsoleColor.White;

                    value2 = GetNumber();
                }
            }
        }
        public static decimal GetPercentage(out bool isPercentage)
        {
            bool isValidInput = false;
            isPercentage = false;
            decimal number = 0;

            while (!isValidInput)
            {
                Console.Write("\t\t");
                string input = Console.ReadLine()!;

                if (input.Contains('%'))
                {
                    input = input.Replace('%', ' ').Trim();
                    isPercentage = true;
                }

                isValidInput = decimal.TryParse(input, out number);
                if (!isValidInput)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("    - Nieobsługiwany format liczby spróbuj ponownie -");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
            }
            return number;
        }
    }
}
