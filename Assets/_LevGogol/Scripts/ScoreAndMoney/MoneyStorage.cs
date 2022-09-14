using System;

public class MoneyStorage
{
    private int _moneyCount;

    public event Action<int> MoneyChanged;
    
    public int MoneyCount
    {
        get => _moneyCount;
        set
        {
            _moneyCount = value;
            MoneyChanged?.Invoke(_moneyCount);
        }
    }

    public MoneyStorage(int moneyCount) //TODO where is constructor place
    {
        _moneyCount = moneyCount;
    }
}