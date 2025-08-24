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

# Who's the Mastermind?

I wasn't sure who the "mastermind" was supposed to be (the player? the 
computer?) but I liked the idea of the computer being an evil mastermind and the 
player needing to conquer its game. That's as deep as the "story-writing" goes :)