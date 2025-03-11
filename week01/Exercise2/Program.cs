using System;

namespace Exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Core Requirements
            Console.Write("Enter your grade percentage: ");
            string userInput = Console.ReadLine();
            int percentage = int.Parse(userInput);

            string letter = "";
            string sign = "";

            // Determine the letter grade
            if (percentage >= 90)
            {
                letter = "A";
            }
            else if (percentage >= 80)
            {
                letter = "B";
            }
            else if (percentage >= 70)
            {
                letter = "C";
            }
            else if (percentage >= 60)
            {
                letter = "D";
            }
            else
            {
                letter = "F";
            }

            // Check if the user passed
            if (percentage >= 70)
            {
                Console.WriteLine("Congratulations! You passed the course.");
            }
            else
            {
                Console.WriteLine("Don't give up! Keep trying and you'll get there.");
            }

            // Stretch Challenges
            if (percentage >= 60 && percentage < 90)
            {
                int lastDigit = percentage % 10;
                if (lastDigit >= 7)
                {
                    sign = "+";
                }
                else if (lastDigit < 3)
                {
                    sign = "-";
                }
            }

            // Handling A+ and F signs
            if (letter == "A" && sign == "+")
            {
                sign = ""; // No A+
            }
            if (letter == "F")
            {
                sign = ""; // No F+ or F-
            }

            // Display the final grade
            Console.WriteLine($"Your grade is: {letter}{sign}");
        }
    }
}


