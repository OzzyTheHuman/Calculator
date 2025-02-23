using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Dynamic;
using System.Text;

namespace Calculator
{
    internal class Program
    {
        public static void SetClipboard(string text) //Windows system utility for copying to clipboard
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "clip";
                processStartInfo.Arguments = "";
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;

                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();

                process.StandardInput.WriteLine(text);
                process.StandardInput.Close();

                process.WaitForExit();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("    - Skopiowano do schowka -");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"    - Błąd kopiowania do schowka: {ex.Message} -");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
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

                while (true)
                {
                    string operation1 = GetOperation();
                    if (operation1 == "M")
                    {
                        SetClipboard(value1.ToString());
                        continue;
                    }
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
                    else if (operation1 == "C")
                    {
                        Console.WriteLine("\n---------------------------------------------------------------\n");
                        break;
                    }
                    else if (operation1 == "!")
                    {
                        result = value1 * (-1);
                    }
                    else if (operation1 == "/" || operation1 == "÷")
                    {
                        Console.CursorTop--;
                        Console.WriteLine("\t     ÷   ");
                        Console.CursorTop--;
                        value2 = GetPercentage(out bool isPercentage);

                        if (isPercentage)
                        {
                            result = value1 * (value2 / 100);
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
                            result = value1 * (value2 / 100);
                        }
                        else
                        {
                            result = GetResult(value1, value2, operation1);
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\t\t\b\b\b=  {result}");
                    Console.ForegroundColor = ConsoleColor.White;

                    value1 = result;
                }

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
            Console.Write("|+| |-| |*| |÷| |√| |%| |^| |!| |C|\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("> Aby obliczyć pierwiastek kwadratowy wystarczy ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("'√' ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("lub ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("'sqrt'");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("> Aby zmienić znak liczby należy wprowadzić ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("'!'");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("> Aby zakończyć ciąg operacji należy wprowadzić ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("'C'");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("> Aby skopiować tekst należy wprowadzić ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("'M'");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("> Aby wkleić tekst należy wcisnąć ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("prawy przycisk myszy");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("> Aby zakończyć");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 'CTRL + C'");

            Console.ForegroundColor = ConsoleColor.White;
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
                switch (input.ToUpper())
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
                    case "^":
                        return "^";
                    case "C":
                        return "C";
                    case "!":
                        return "!";
                    case "M":
                        return "M";
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
