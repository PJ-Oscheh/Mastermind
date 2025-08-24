using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mastermind.Tests;

[TestClass]
public class MastermindTest
{
    
    /// <summary>
    /// Test to ensure generated answers are valid.
    ///
    /// This test will generate 10,000 answers and ensure each is
    /// exactly four characters long, is parsable as an integer,
    /// and each digit is between 1-6, inclusive.
    /// </summary>
    [TestMethod]
    public void GenerateAnswerTest()
    {
        for (int i = 0; i < 10000; i++)
        {
            // Generate a random answer
            string testAnswer = Program.GenerateAnswer();
            
            // Verify answer is four digits long
            if (testAnswer.Length != 4)
            {
                Assert.Fail($"{testAnswer} is not four characters long");
            }

            // Verify answer is parsable as an integer
            if (!int.TryParse(testAnswer, out _))
            {
                Assert.Fail($"{testAnswer} could not be parsed as an integer");
            }
            
            // Verify each digit is between 1 and 6
            foreach (char c in testAnswer)
            {
                int currentDigit = int.Parse(c.ToString());
                if (currentDigit < 1 || currentDigit > 6)
                {
                    Assert.Fail($"{currentDigit} in {testAnswer} is not within range: [1-6]");
                }
                
            }
        }
    }
    
    /// <summary>
    /// Test to ensure hint generation is valid.
    ///
    /// This test will generate a hint for the test cases provided in the "inputAnswerHintTriplets" list.
    /// In each triplet, the first item is the guess, the second is the answer, and the third is the expected
    /// hint.
    /// </summary>
    [TestMethod]
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    public void GenerateHintTest()
    {
        // First is input, second is answer, third is expected hint
        List<(string, string,string)> inputAnswerHintTriplets = [
            ("1234","5555",string.Empty), // No correct digits -> ""
            ("1234","5155","-"), // 1 correct digit in wrong position -> "-"
            ("1234","1555","+"), // 1 correct digit in correct position -> "+"
            ("1234","1243","++--"), // 2 correct digits in correct position, 2 correct in wrong position -> "++--"
            ("3412","4312","++--"), // Same as above but two incorrect position before 2 correct position -> "++--"
            ("3412","3451", "++-"), // 2 correct digits in correct position, 1 correct in wrong position, 1 incorrect -> "++-"
            ("4233", "1234", "++-") // Duplicate digit in incorrect position
        ];
        
        foreach (var triplet in inputAnswerHintTriplets)
        {
            string testHint = Program.GenerateHint(triplet.Item1, triplet.Item2);
            Assert.AreEqual(triplet.Item3, testHint);
        }
        
    }
}