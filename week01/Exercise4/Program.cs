using System;
using System.Collections.Generic;

namespace Exercise4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            int number;

            Console.WriteLine("Enter a list of numbers, type 0 when finished.");

            do
            {
                Console.Write("Enter number: ");
                number = int.Parse(Console.ReadLine());

                if (number != 0)
                {
                    numbers.Add(number);
                }

            } while (number != 0);

            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }

            float average = (float)sum / numbers.Count;
            int largestNumber = int.MinValue;
            int smallestPositiveNumber = int.MaxValue;

            foreach (int num in numbers)
            {
                if (num > largestNumber)
                {
                    largestNumber = num;
                }

                if (num > 0 && num < smallestPositiveNumber)
                {
                    smallestPositiveNumber = num;
                }
            }

            numbers.Sort();

            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {largestNumber}");

            if (smallestPositiveNumber == int.MaxValue)
            {
                Console.WriteLine("There is no positive number in the list.");
            }
            else
            {
                Console.WriteLine($"The smallest positive number is: {smallestPositiveNumber}");
            }

            Console.WriteLine("The sorted list is:");
            foreach (int num in numbers)
            {
                Console.WriteLine(num);
            }
        }
    }
}
