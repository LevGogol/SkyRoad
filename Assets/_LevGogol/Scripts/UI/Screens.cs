using System.Linq;
using UnityEngine;

public class Screens : MonoBehaviour
{
    private ScreenBase[] _screens;
    
    private void Awake()
    {
        _screens = GetComponentsInChildren<ScreenBase>(true);

        foreach (var screen in _screens)
        {
            screen.gameObject.SetActive(false);
        }
    }

    public T Get<T>() where T : ScreenBase
    {
        return _screens.First(screen => screen is T) as T;
    }
}