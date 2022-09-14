using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopGrid : MonoBehaviour
{
    [SerializeField] private ShopCell _shopCellPrefab;
    [SerializeField] private RectTransform _root;

    private List<ShopCell> _cells = new List<ShopCell>(30);
    public Character SelectedCharacter;

    public event Action<Character> Touched;

    public void Initialization(Characters characters, Character selectedCharacter)
    {
        SelectedCharacter = selectedCharacter;
        
        for (int i = 0; i < characters.Count; i++)
        {
            var cell = Instantiate(_shopCellPrefab, _root);
            cell.Initialization(characters.GetCharacter(i));
            cell.Touched += OnTouched;

            if (i == SelectedCharacter.ID) 
                cell.EnableHighlight();
            
            _cells.Add(cell);
        }
    }

    private void OnTouched(Character character)
    {
        if (!character.IsBuy)
            return;

        SelectedCharacter = character;
        Refresh();
        
        Touched?.Invoke(character);
    }

    public void Refresh()
    {
        for(int i = 0; i < _cells.Count; i++)
        {
            _cells[i].Refresh();
            if (i == SelectedCharacter.ID) 
                _cells[i].EnableHighlight();
            else 
                _cells[i].DisableHighlight();
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _cells.Count; i++)
            _cells[i].Touched -= OnTouched;
    }
}
