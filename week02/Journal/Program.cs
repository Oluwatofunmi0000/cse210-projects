/*using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Journal Project.");
    }
}*/




using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    public class JournalEntry
    {
        public string Date { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }

        public JournalEntry(string prompt, string response)
        {
            Date = DateTime.Now.ToShortDateString();
            Prompt = prompt;
            Response = response;
        }

        public override string ToString()
        {
            return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
        }
    }

    public class Journal
    {
        public List<JournalEntry> Entries { get; set; } = new();

        private static readonly List<string> Prompts = new()
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        public void AddEntry()
        {
            var random = new Random();
            string prompt = Prompts[random.Next(Prompts.Count)];

            Console.WriteLine(prompt);
            Console.Write("Response: ");
            string response = Console.ReadLine();

            var entry = new JournalEntry(prompt, response);
            Entries.Add(entry);
        }

        public void DisplayEntries()
        {
            foreach (var entry in Entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveToFile(string filename)
        {
            using StreamWriter writer = new(filename);
            foreach (var entry in Entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
            Console.WriteLine("Journal saved successfully.");
        }

        public void LoadFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                Entries.Clear();
                string[] lines = File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        var entry = new JournalEntry(parts[1], parts[2]) { Date = parts[0] };
                        Entries.Add(entry);
                    }
                }

                Console.WriteLine("Journal loaded successfully.");
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nJournal Menu");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Quit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        journal.AddEntry();
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        Console.Write("Enter filename to save: ");
                        string saveFile = Console.ReadLine();
                        journal.SaveToFile(saveFile);
                        break;
                    case "4":
                        Console.Write("Enter filename to load: ");
                        string loadFile = Console.ReadLine();
                        journal.LoadFromFile(loadFile);
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
 