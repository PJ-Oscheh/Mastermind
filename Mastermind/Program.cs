using System;
using System.Text;

namespace Mastermind
{
    internal class Program
    {
        private const int TotalGuesses = 10;
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
                                        "incorrect position. If your guess contains no correct digits, I won't " +
                                        "provide any help.\n\n" +
                                        "Good luck!";

        private static readonly char[] ValidChars = ['1','2','3','4','5','6'];

        /// <summary>
        /// Represents the state of the application.
        ///
        /// Game represents the game loop.
        /// PlayAgain represents the "play again" screen.
        /// Quit indicates the application should exit.
        /// </summary>
        public enum State
        {
            Game,
            PlayAgain,
            Quit
        }
        
        public static void Main(string[] args)
        {
            State nextState = State.Game;

            while (nextState != State.Quit)
            {
                switch (nextState)
                {
                    case State.Game:
                        nextState = DoGameLoop();
                        break;
                    case State.PlayAgain:
                        nextState = DoPlayAgainLoop();
                        break;
                }
            }
        }

        /// <summary>
        /// Represents the game loop. The loop progresses as follows:
        ///
        /// Ask for input. If the input is correct, go to play again screen. Otherwise,
        /// print a hint, with a "+" for any character in a correct position and a "-" for any
        /// character that exists in the answer but is in an incorrect position.
        /// </summary>
        /// <param name="fixedAnswer">Fixes answer to a certain value for testing</param>
        /// <returns>The next state of the application (State.PlayAgain)</returns>
        internal static State DoGameLoop(string? fixedAnswer=null)
        {
            Console.WriteLine("Welcome to Mastermind!");
            Console.WriteLine(HelpText);

            int remainingGuesses = TotalGuesses;
            string answer = fixedAnswer ?? GenerateAnswer();
            
            while (remainingGuesses > 0)
            {
                string inputGuess = GetPlayerInput(remainingGuesses, TotalGuesses);

                if (inputGuess == "h")
                {
                    Console.WriteLine(HelpText);
                    continue;
                }
                else if (inputGuess == "q")
                {
                    return State.Quit;
                }

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

            Console.WriteLine($"You lost :( The answer was '{answer}'");
            return State.PlayAgain;
        }

        /// <summary>
        /// Represents the play again loop. The loop progresses as follows:
        ///
        /// Asks the player if they want to play again. If so, go to game loop. If not, quit. If
        /// input couldn't be understood, ask again.
        /// </summary>
        /// <returns>The next state of the application (State.Game or State.Quit)</returns>
        internal static State DoPlayAgainLoop()
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
        internal static string GenerateAnswer()
        {
            Random rand = new();
            StringBuilder tmpAnswer = new();

            for (int i = 0; i < 4; i++)
            {
                tmpAnswer.Append(rand.Next(1, 7));
            }
            
            return tmpAnswer.ToString();
        }

        /// <summary>
        /// Gets the player's input. If the input is null, is not a valid integer, or does not have a length
        /// of 4, then we'll ask for the input again.
        /// </summary>
        /// <returns>The player's input</returns>
        internal static string GetPlayerInput(int remainingGuesses, int totalGuesses)
        {
            Console.WriteLine($"\nTake a guess! ({TotalGuesses-remainingGuesses+1}/{totalGuesses})");

            while (true)
            {
                Console.Write(">");
                string? input = Console.ReadLine();
                if (input != null && input.ToLower() == "h" || input != null && input.ToLower() == "q")
                {
                    return input;
                }
                else if (input != null && IsValidCharacters(input) && input.Length == 4)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("I couldn't understand your input. If you're confused, type 'h' for help.");
                }
            }
        }

        /// <summary>
        /// Verifies that 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsValidCharacters(string input)
        {
            foreach (char c in input) 
            {
                if (!ValidChars.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Generates a hint based on the player's input.
        ///
        /// If the input guess contains a correct character in a correct position, append a '+' to the guess. If it
        /// contains a correct character in an incorrect position, append a '-' to the guess.
        /// </summary>
        /// <param name="inputGuess">The player's input guess</param>
        /// <param name="answer">The correct answer</param>
        /// <returns>The hint string</returns>
        internal static string GenerateHint(string inputGuess, string answer)
        {
            StringBuilder tmpHint = new();
            for (int i = 0; i < inputGuess.Length; i++)
            {
                if (inputGuess[i] == answer[i])
                {
                    tmpHint.Insert(0,'+');
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