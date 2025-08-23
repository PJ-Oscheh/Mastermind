using System;
using System.Text;

namespace Mastermind
{
    internal class Program
    {
        private const string HelpText = "HOW TO PLAY:\n" +
                                        "============\n" +
                                        "The mastermind (that's me!) will think of a number. This number:\n" +
                                        "- Is always four digits long\n" +
                                        "- Consists only of digits 1-6, inclusive\n\n" +
                                        "For example, a valid answer could be '1234'. An invalid answer might be " +
                                        "'12345', '7777', or 'eggs'.\n\n" +
                                        "After each guess, I'll give you a hint (you'll need it!) If my hint\n" +
                                        "includes a '+' character, that means a digit is in the correct location.\n" +
                                        "If it includes a '-' character, that means a digit is correct but in the\n" +
                                        "incorrect position.\n\n" +
                                        "Good luck!";
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mastermind!");
            Console.WriteLine(HelpText);

            int remainingGuesses = 10;
            string answer = GenerateAnswer();
            
            while (remainingGuesses > 0)
            {
                string inputGuess = GetPlayerInput();

                if (inputGuess != answer)
                {
                    
                }
                // TODO: Handle success case
                
                remainingGuesses--;
            }
        }

        /// <summary>
        /// Generates a random answer.
        ///
        /// An answer consists of four digits ranging from 1-6, inclusive.
        /// </summary>
        /// <returns>The answer as a string</returns>
        public static string GenerateAnswer()
        {
            Random rand = new();
            StringBuilder tmpAnswer = new();

            for (int i = 0; i < 4; i++)
            {
                tmpAnswer.Append(rand.Next(1, 7));
            }
            
            return tmpAnswer.ToString();
        }

        public static string GetPlayerInput()
        {
            Console.WriteLine("\nTake a guess!");

            while (true)
            {
                Console.Write(">");
                string? input = Console.ReadLine();
                if (input != null && int.TryParse(input, out _) && input.Length == 4)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("I couldn't understand your input. If you're confused, type 'h' for help.");
                }
            }
        }

        public static string GenerateHint(string inputGuess, string answer)
        {
            StringBuilder tmpHint = new();
            for (int i = 0; i < inputGuess.Length; i++)
            {
                if (inputGuess[i] == answer[i])
                {
                    tmpHint.Append('+');
                }
                else if (answer.Contains(inputGuess[i]))
                {
                    tmpHint.Append('-');
                }
            }
            
            return tmpHint.ToString();
        }
    }
}