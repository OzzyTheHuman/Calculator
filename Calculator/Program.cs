using System.ComponentModel;
using System.Text;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            GetGrittings();
            GetOperations();

        }
        public static void GetGrittings()
        {
            Console.WriteLine();
            Console.WriteLine("\t\t\tCalculator by Ozzy");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("> Obsługiwane operacje |+| |-| |*| |/| |√|");
            Console.WriteLine("> Aby obliczyć pierwiastek kwadratowy wystarczy '√' lub 'sqrt'");
            Console.WriteLine("> Aby wkleić tekst należy wcisnąć prawy przycisk myszy");
            Console.WriteLine("> 'CTRL + C' aby zakończyć");
            Console.WriteLine();
            Console.WriteLine("1. Podaj liczbę + ENTER");
            Console.WriteLine("2. Podaj operację + ENTER");
            Console.WriteLine("3. Podaj liczbę + ENTER");
            Console.WriteLine("4. Wynik");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();
        }
        public static void GetOperations()
        {
            bool isPercentage;
            while (true)
            {
                decimal number = GetNumber();
                decimal result = GetResult(number);

                Console.Write($"\n    =\t{result}");
                Console.WriteLine("\n\n---------------------------------------------------------------");
                Console.WriteLine("Podaj liczbę, aby kontynuować lub 'CTRL + C', aby zakończyć.");
                Console.WriteLine();
            }
        }
        public static decimal GetNumber()
        {
            bool isValidInput = false;
            decimal number = 0;

            //Check if the received number is correct, if not, ask again
            while (!isValidInput)
            {
                Console.Write("\t");
                isValidInput = decimal.TryParse(Console.ReadLine(), out decimal result);
                if (!isValidInput)
                {
                    Console.Write("- Nieobsługiwany format liczby, spróbuj ponownie -");
                    Console.WriteLine();
                }
                number = result;
            }
            return number;
        }
        
        public static decimal GetResult(decimal number)
        {
            decimal number2;
            //Check if the received string is correct, if not, ask again
            while (true)        
            {
                Console.Write("\t");
                string? input = Console.ReadLine();
                
                if (input == null)
                {
                    Console.WriteLine("- Nie można odczytać pustej wartości, spróbuj ponownie -");
                    continue;
                }
                string operation = input.ToLower().Trim();
                try
                {
                    switch (operation)
                    {
                        case "+":
                            number2 = GetNumber();
                            return checked(number + number2);
                        case "-":
                            number2 = GetNumber();
                            return checked(number - number2);
                        case "*":
                            number2 = GetNumber();
                            return checked(number * number2);
                        case "/":
                            number2 = GetNumber();
                            if (number2 == 0)
                            {
                                Console.WriteLine("- Błąd przy dzieleniu przez zero. Wybierz inną operacje -");
                                continue;
                            }
                            return number / number2;
                        case "√":
                        case "sqrt":
                            return (decimal)Math.Sqrt((double)number);
                        default:
                            Console.WriteLine($"- Nieobsługiwana operacja, spróbuj ponownie -");
                            break;
                    }
                }
                catch (OverflowException)
                {
                    Console.WriteLine("- Wystąpiło przepełnienie. Spróbuj podać mniejszą liczbę -");
                }
            }
        }
    }
}
