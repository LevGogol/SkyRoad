using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopGrid : MonoBehaviour
{
    [SerializeField] private ShopCell _shopcellPrefab;
    [SerializeField] private RectTransform _root;

    private List<ShopCell> _cells = new List<ShopCell>(30);
    private int _oldID;
    
    public event Action<int> Touched;

    public void Initialization(Characters characters, int selectedCharacterIndex)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            var cell = Instantiate(_shopcellPrefab, _root);
            cell.Initialization(characters.GetCharacter(i));
            cell.Touched += OnTouched;

            if (i == selectedCharacterIndex) cell.EnableHighlight();
            
            _cells.Add(cell);
        }

        _oldID = selectedCharacterIndex;
    }

    private void OnTouched(int id)
    {
        if (!_cells[id].Character.IsBuy)
        {
            return;
        }
        
        _cells[_oldID].DisableHighlight();
        _cells[id].EnableHighlight();
        
        Touched?.Invoke(id);

        _oldID = id;
    }

    public void Refresh()
    {
        foreach (var cell in _cells)
        {
            cell.Refresh();
        }
    }
}
