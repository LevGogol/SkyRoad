using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Characters", order = 3)]
public class Characters : ScriptableObject
{
    [SerializeField] private Character[] _characters;

    public int Count => _characters.Length;

    public Character GetCharacter(int index)
    {
        return _characters[index];
    }

    [ContextMenu("SetID")]
    public void SetID()
    {
        for (int i = 0; i < _characters.Length; i++)
        {
            _characters[i].ID = i;
        }
    }
}
