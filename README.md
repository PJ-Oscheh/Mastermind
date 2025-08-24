# Mastermind
It's like wordle but with numbers and in the terminal!

# What's Mastermind?

Mastermind is a game where the player must guess a computer-generated answer. It's
like wordle but with numbers.

## Instructions

1. The computer generates a 4 digit number. Each digit is between 1-6, inclusive.
2. The player has 10 guesses to try and guess the computer's number
3. After each guess, the player gets a hint.
   1. If the player guesses a digit correctly in the correct position, a "+" is prepended to the hint.
   2. If the player guesses a digit corectly but in the incorrect position, a "-" is appended to the hint.
   3. For digits that are incorrect, nothing is appended to the hint.
   4. Note that all pluses appear before minuses - so it can be tricky to know _which_ digits are correct!
4. If the player guesses the number correctly, they win! If they run out of guesses, they can choose to play again or
quit.

# Development Notes

The first thing I thought about before touching the keyboard was how to organize this
project. For most projects of certain complexity, I opt for an object-oriented approach.
However, since this project was a relatively simple command-line application, I opted
instead to follow a procedural approach, organizing the code across different methods
in the `Program.cs` file in an effort to not "over-engineer" the code.

That being said, I still wanted to organize my code across different methods. Outside
of making the code more legible, this organization made writing tests for the program
much easier. In a hypothetical scenario in which this code would be adapted to a 
more complicated GUI rendition, this organization would ease that transition process
as well.

# Solution Overview

## Mastermind

This is the main game. `Program.cs` is the file of interest. My thought process for 
organizing the game loop was that different parts of the game could be organized into
states. Then, once that loop completes, it can return the next state the program should
move to.

For example:

1. Upon starting the program, it enters the Game state.
2. After the game completes, the program enters the "PlayAgain" state.
   1. If the player chooses to play again, the program enters the "Game" state.
   2. If the player chooses to exit, the program indicates the main loop should
exit, and therefore the program should exit.

The code for this is quite compact:

```csharp
/// <summary>
/// Program execution starts here
/// </summary>
/// <param name="args">Command line arguments</param>
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
```

The main game loop makes calls to other methods for execution. As explained in the
development notes, this was to improve code legibility, testability, and maintainability.

### Enum

- `State` - Represents the current state of the program. Consists of: 
  - `Game` - The main game
  - `PlayAgain` - Play again screen
  - `Quit` - Indicates program should exit

### Methods

- `Main:void` - Program entrypoint
- `DoGameLoop:State` - Game logic. Returns the next state once the loop completes
- `DoPlayAgainLoop:State` - Loop to ask player if they want to play another round or exit the program
- `GenerateAnswer:string` - Generates an answer that's four digits long with each digit a number from 1-6.
- `GetPlayerInput:string` - Validates player input. They must input a valid guess number, `h` for help, or `q` to quit.
- `IsValidCharacters:bool` - Validates that a character is between 1 and 6. Helper method for `GetPlayerInput`
- `GenerateHint:string` - Generates a hint given the player's guess and the correct answer.

## Mastermind.Tests

This contains tests for the game. In particular, it tests the game's methods for
generating a random answer (verifying answers are four digits long consisting of
numbers from 1-6 inclusive) and for generating a hint (verifying hints consist of
`+` characters to indicate correct digit & placement, `-` characters to indicate
correct digit but incorrect placement, no character for incorrect digits, and that
`+` characters appear before `-` characters.

Since the answer generation test relies on random number generation, it tests the 
generation procedure 10,000 times. The hint generation test is entirely deterministic,
so that consists of a few unique test cases that cover a variety of guess/answer
combinations.


# Who's the Mastermind?

I wasn't sure who the "mastermind" was supposed to be (the player? the 
computer?) but I liked the idea of the computer being an evil mastermind and the 
player needing to conquer its game. That's as deep as the "story-writing" goes :)