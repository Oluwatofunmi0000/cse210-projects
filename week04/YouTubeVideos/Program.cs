using System;
using System.Collections.Generic;

// Class to represent a comment
class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

// Class to represent a YouTube video
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(string commenterName, string commentText)
    {
        Comments.Add(new Comment(commenterName, commentText));
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void DisplayVideoDetails()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"- {comment.CommenterName}: {comment.CommentText}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        // Creating and adding comments to videos
        Video video1 = new Video("Introduction to C#", "CodeWithJoy", 600);
        video1.AddComment("Alice", "Great tutorial!");
        video1.AddComment("Bob", "Very helpful, thanks!");
        video1.AddComment("Charlie", "I love how clearly you explain things!");
        videos.Add(video1);

        Video video2 = new Video("Advanced C# Generics", "DevMaster", 900);
        video2.AddComment("David", "This was exactly what I needed!");
        video2.AddComment("Eve", "Can you do one on delegates next?");
        video2.AddComment("Frank", "Really well-structured content.");
        videos.Add(video2);

        Video video3 = new Video("C# Async Programming", "TechGuru", 750);
        video3.AddComment("Grace", "This clarified a lot of my doubts!");
        video3.AddComment("Hannah", "Async/Await is much clearer now.");
        video3.AddComment("Isaac", "Super informative, thanks!");
        videos.Add(video3);

        // Display video details
        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}
