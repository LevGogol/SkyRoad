using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] private List<Character> _characters;

    public int Count => _characters.Count;

    public Character GetCharacter(int index)
    {
        return _characters[index];
    }

    public Character GetCurrentCharacter()
    {
        var selectedCharacterIndex = PlayerPrefs.GetInt("Character", 0);
        return _characters[selectedCharacterIndex];
    }

    #if UNITY_EDITOR
    private void Start()
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].ID = i;
        }
    }
    #endif
}
