using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _lifeCount;

    public event Action<int> Changed;
    public event Action Ended;
    
    public int Value
    {
        get { return _lifeCount; }
        set
        {
            _lifeCount = value;

            Changed?.Invoke(_lifeCount);

            if (_lifeCount <= 0)
                Ended?.Invoke();
        }
    }
}