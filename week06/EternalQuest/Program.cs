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

    public abstract int RecordEvent();  // Returns points gained
    public abstract bool IsComplete();
    public abstract string GetStatus();  // For displaying progress
    public abstract string Save();       // For saving to file
}


public class SimpleGoal : Goal
{
    private bool _completed = false;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!_completed)
        {
            _completed = true;
            return _points;
        }
        return 0;
    }

    public override bool IsComplete() => _completed;

    public override string GetStatus() => _completed ? "[X]" : "[ ]";

    public override string Save() => $"Simple|{_name}|{_description}|{_points}|{_completed}";
}


public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => _points;

    public override bool IsComplete() => false;

    public override string GetStatus() => "[∞]";

    public override string Save() => $"Eternal|{_name}|{_description}|{_points}";
}



public class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _targetCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _timesCompleted = 0;
    }

    public override int RecordEvent()
    {
        if (_timesCompleted < _targetCount)
        {
            _timesCompleted++;
            if (_timesCompleted == _targetCount)
                return _points + _bonusPoints;
            return _points;
        }
        return 0;
    }

    public override bool IsComplete() => _timesCompleted >= _targetCount;

    public override string GetStatus() => $"[{(_timesCompleted >= _targetCount ? "X" : " ")}] Completed {_timesCompleted}/{_targetCount}";

    public override string Save() => $"Checklist|{_name}|{_description}|{_points}|{_timesCompleted}|{_targetCount}|{_bonusPoints}";
}

