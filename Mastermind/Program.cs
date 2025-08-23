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
                                        "incorrect position. If your guess contains no correct digits, I won't" +
                                        "provide any help.\n\n" +
                                        "Good luck!";

        public enum State
        {
            Game,
            PlayAgain,
            Quit
        }
        
        public static void Main(string[] args)
        {
            State currentState = State.Game;

            while (currentState != State.Quit)
            {
                switch (currentState)
                {
                    case State.Game:
                        currentState = DoGameLoop();
                        break;
                    case State.PlayAgain:
                        currentState = DoPlayAgainLoop();
                        break;
                }
            }
        }

        public static State DoGameLoop(string? fixedAnswer=null)
        {
            Console.WriteLine("Welcome to Mastermind!");
            Console.WriteLine(HelpText);

            int remainingGuesses = 10;
            string answer = fixedAnswer ?? GenerateAnswer();
            
            while (remainingGuesses > 0)
            {
                string inputGuess = GetPlayerInput();

                if (inputGuess != answer)
                {
                    string hint = GenerateHint(inputGuess, answer);
                    Console.WriteLine($"Incorrect! Here's a hint: [{hint}]");
                }
                else
                {
                    Console.WriteLine("That's correct! Horray!");
                    break;
                }
                
                remainingGuesses--;
            }

            return State.PlayAgain;
        }

        public static State DoPlayAgainLoop()
        {
            Console.WriteLine("Well that was fun! Want to play again? (Type 'y' for yes or 'n' for no)");

            while (true)
            {
                Console.Write(">");
                string? input = Console.ReadLine();

                if (input != null && input.ToLower() == "y")
                {
                    return State.Game;
                }
                else if (input != null && input.ToLower() == "n")
                {
                    return State.Quit;
                }
                else
                {
                    Console.WriteLine("Sorry, I couldn't understand your input. Please type 'Y' to play again, or " +
                                      "'n' to quit.");
                }
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