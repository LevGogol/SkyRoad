using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class YandexSDKImplementation : MonoBehaviour
{
    private static YandexSDKImplementation _implementation;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_implementation == null)
        {
            _implementation = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();
        
        
    }
}
