using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Scripture scripture = new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.");

            while (!scripture.IsFullyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());

                Console.WriteLine("\nPress Enter to hide words, or type 'quit' to exit.");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    break;

                scripture.HideRandomWords();
            }

            Console.Clear();
            Console.WriteLine("All words have been hidden.\n");
        }
    }

    class Reference
    {
        public string Book { get; }
        public int Chapter { get; }
        public int StartVerse { get; }
        public int EndVerse { get; }

        public Reference(string book, int chapter, int startVerse, int endVerse = -1)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = startVerse;
            EndVerse = endVerse == -1 ? startVerse : endVerse;
        }

        public string GetDisplayText()
        {
            if (StartVerse == EndVerse)
                return $"{Book} {Chapter}:{StartVerse}";
            else
                return $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
        }
    }

    class Word
    {
        private string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public bool IsHidden() => _isHidden;

        public void Hide() => _isHidden = true;

        public string GetDisplayText() => _isHidden ? new string('_', _text.Length) : _text;
    }

    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;
        private Random _random;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ').Select(word => new Word(word)).ToList();
            _random = new Random();
        }

        public void HideRandomWords()
        {
            int wordsToHide = 3;
            int hiddenCount = 0;

            while (hiddenCount < wordsToHide)
            {
                int index = _random.Next(_words.Count);
                if (!_words[index].IsHidden())
                {
                    _words[index].Hide();
                    hiddenCount++;
                }
            }
        }

        public bool IsFullyHidden() => _words.All(word => word.IsHidden());

        public string GetDisplayText()
        {
            string scriptureText = string.Join(' ', _words.Select(word => word.GetDisplayText()));
            return $"{_reference.GetDisplayText()}\n{scriptureText}";
        }
    }
}
