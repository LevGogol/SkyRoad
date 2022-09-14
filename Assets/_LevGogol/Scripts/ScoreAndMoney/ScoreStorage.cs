using System;

public class ScoreStorage
{
    private int _score;
    private int _maxScore;

    public event Action<int> ScoreChanged;
    public event Action<int> MaxScoreChanged;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            ScoreChanged?.Invoke(_score);
        }
    }

    public int MaxScore
    {
        get => _maxScore;
        set
        {
            _maxScore = value;
            MaxScoreChanged?.Invoke(_maxScore);
        }
    }

    public ScoreStorage(int maxScore) //TODO where is constructor place
    {
        _maxScore = maxScore;
    }
}