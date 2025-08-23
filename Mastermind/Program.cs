using System;
using System.Text;

namespace Mastermind
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mastermind!");

            int remainingGuesses = 10;

            while (remainingGuesses > 0)
            {
                Console.WriteLine("\nTake a guess!");
                Console.Write(">");
                string? input = Console.ReadLine();
                remainingGuesses--;
            }
            

            string answer = GenerateAnswer();
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
        
        
    }
}