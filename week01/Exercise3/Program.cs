using System;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            string playAgain;

            do
            {
                // Generate a random number between 1 and 100
                Random randomGenerator = new Random();
                int magicNumber = randomGenerator.Next(1, 101);
                int guess = -1;
                int attempts = 0;

                Console.WriteLine("Welcome to the Guess My Number Game!");
                Console.WriteLine("I have picked a magic number between 1 and 100. Try to guess it!");

                // Loop until the user guesses the correct number
                while (guess != magicNumber)
                {
                    try
                    {
                        Console.Write("What is your guess? ");
                        guess = int.Parse(Console.ReadLine());
                        attempts++;

                        if (guess < magicNumber)
                        {
                            Console.WriteLine("Higher");
                        }
                        else if (guess > magicNumber)
                        {
                            Console.WriteLine("Lower");
                        }
                        else
                        {
                            Console.WriteLine($"You guessed it! It took you {attempts} attempts.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input! Please enter a valid number.");
                    }
                }

                // Ask the user if they want to play again
                Console.Write("Do you want to play again? (yes/no): ");
                playAgain = Console.ReadLine().ToLower();

            } while (playAgain == "yes");

            Console.WriteLine("Thanks for playing! Goodbye.");
        }
    }
}
