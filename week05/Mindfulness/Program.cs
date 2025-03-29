using System;
using System.Collections.Generic;
using System.Threading;

// Base class containing common attributes and methods
abstract class MindfulnessActivity
{
    protected int duration;

    public void StartMessage(string activityName, string description)
    {
        Console.WriteLine($"\nStarting {activityName}...");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        PauseWithAnimation(3);
    }

    public void EndMessage(string activityName)
    {
        Console.WriteLine($"\nGreat job! You completed the {activityName} for {duration} seconds.");
        PauseWithAnimation(3);
    }

    protected void PauseWithAnimation(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    public abstract void Run();
}

// Breathing Activity
class BreathingActivity : MindfulnessActivity
{
    public override void Run()
    {
        StartMessage("Breathing Activity", "This activity will help you relax by guiding you through slow breathing.");
        for (int i = 0; i < duration; i += 6)
        {
            Console.WriteLine("Breathe in...");
            PauseWithAnimation(3);
            Console.WriteLine("Breathe out...");
            PauseWithAnimation(3);
        }
        EndMessage("Breathing Activity");
    }
}

// Reflection Activity
class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };
    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What did you learn about yourself?"
    };

    public override void Run()
    {
        StartMessage("Reflection Activity", "This activity will help you reflect on times of strength and resilience.");
        Random rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        PauseWithAnimation(3);
        int timeSpent = 0;
        while (timeSpent < duration)
        {
            Console.WriteLine(questions[rand.Next(questions.Count)]);
            PauseWithAnimation(5);
            timeSpent += 5;
        }
        EndMessage("Reflection Activity");
    }
}

// Listing Activity
class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are your personal strengths?",
        "Who are people you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public override void Run()
    {
        StartMessage("Listing Activity", "This activity will help you list positive things in your life.");
        Random rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        PauseWithAnimation(3);
        Console.WriteLine("Start listing items. Press Enter after each one. Type 'done' to finish.");
        List<string> items = new List<string>();
        string input;
        while ((input = Console.ReadLine().ToLower()) != "done")
        {
            items.Add(input);
        }
        Console.WriteLine($"You listed {items.Count} items!");
        EndMessage("Listing Activity");
    }
}

// Main Program with Menu
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            MindfulnessActivity activity = null;

            switch (choice)
            {
                case "1": activity = new BreathingActivity(); break;
                case "2": activity = new ReflectionActivity(); break;
                case "3": activity = new ListingActivity(); break;
                case "4": return;
                default: Console.WriteLine("Invalid choice, try again."); continue;
            }
            activity?.Run();
        }
    }
}
