public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract bool IsComplete();
    public abstract string Serialize();
    public abstract void LoadData(string data);
}


public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0;
    }

    public override bool IsComplete() => _isComplete;

    public override string GetStatus() => $"[{(_isComplete ? "X" : " ")}] {_name}";

    public override string Serialize() => $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";

    public override void LoadData(string data)
    {
        var parts = data.Split('|');
        _name = parts[1];
        _description = parts[2];
        _points = int.Parse(parts[3]);
        _isComplete = bool.Parse(parts[4]);
    }
}


public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => _points;

    public override bool IsComplete() => false;

    public override string GetStatus() => $"[∞] {_name}";

    public override string Serialize() => $"EternalGoal|{_name}|{_description}|{_points}";

    public override void LoadData(string data)
    {
        var parts = data.Split('|');
        _name = parts[1];
        _description = parts[2];
        _points = int.Parse(parts[3]);
    }
}


public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _currentCount = 0;
        _bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (_currentCount < _targetCount)
        {
            _currentCount++;
            if (_currentCount == _targetCount)
                return _points + _bonusPoints;
            return _points;
        }
        return 0;
    }

    public override bool IsComplete() => _currentCount >= _targetCount;

    public override string GetStatus() => $"[{(_currentCount >= _targetCount ? "X" : " ")}] {_name} - Completed {_currentCount}/{_targetCount} times";

    public override string Serialize() => $"ChecklistGoal|{_name}|{_description}|{_points}|{_targetCount}|{_currentCount}|{_bonusPoints}";

    public override void LoadData(string data)
    {
        var parts = data.Split('|');
        _name = parts[1];
        _description = parts[2];
        _points = int.Parse(parts[3]);
        _targetCount = int.Parse(parts[4]);
        _currentCount = int.Parse(parts[5]);
        _bonusPoints = int.Parse(parts[6]);
    }
}


public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void CreateGoal(Goal goal) => _goals.Add(goal);

    public void RecordEvent(int goalIndex)
    {
        int earned = _goals[goalIndex].RecordEvent();
        _score += earned;
        Console.WriteLine($"You earned {earned} points!");
    }

    public void ShowGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()}");
    }

    public void ShowScore() => Console.WriteLine($"Current Score: {_score}");

    public void SaveToFile(string file)
    {
        using (StreamWriter sw = new StreamWriter(file))
        {
            sw.WriteLine(_score);
            foreach (var goal in _goals)
                sw.WriteLine(goal.Serialize());
        }
    }

    public void LoadFromFile(string file)
    {
        _goals.Clear();
        string[] lines = File.ReadAllLines(file);
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            Goal goal = parts[0] switch
            {
                "SimpleGoal" => new SimpleGoal("", "", 0),
                "EternalGoal" => new EternalGoal("", "", 0),
                "ChecklistGoal" => new ChecklistGoal("", "", 0, 0, 0),
                _ => null
            };
            goal.LoadData(lines[i]);
            _goals.Add(goal);
        }
    }
}


class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n1. Create Goal\n2. Record Event\n3. Show Goals\n4. Show Score\n5. Save\n6. Load\n7. Quit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Select Type: 1. Simple 2. Eternal 3. Checklist");
                    string type = Console.ReadLine();
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Description: ");
                    string desc = Console.ReadLine();
                    Console.Write("Points: ");
                    int pts = int.Parse(Console.ReadLine());

                    Goal goal = type switch
                    {
                        "1" => new SimpleGoal(name, desc, pts),
                        "2" => new EternalGoal(name, desc, pts),
                        "3" => new ChecklistGoal(name, desc, pts,
                                                  PromptInt("Target count: "),
                                                  PromptInt("Bonus: ")),
                        _ => null
                    };
                    manager.CreateGoal(goal);
                    break;

                case "2":
                    manager.ShowGoals();
                    Console.Write("Enter goal number: ");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    manager.RecordEvent(index);
                    break;

                case "3": manager.ShowGoals(); break;
                case "4": manager.ShowScore(); break;
                case "5":
                    Console.Write("Filename to save: ");
                    manager.SaveToFile(Console.ReadLine());
                    break;
                case "6":
                    Console.Write("Filename to load: ");
                    manager.LoadFromFile(Console.ReadLine());
                    break;
                case "7": running = false; break;
            }
        }
    }

    static int PromptInt(string msg)
    {
        Console.Write(msg);
        return int.Parse(Console.ReadLine());
    }
}
