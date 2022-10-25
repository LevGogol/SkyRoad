using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopGrid : MonoBehaviour
{
    [SerializeField] private ShopCell _shopCellPrefab;
    [SerializeField] private RectTransform _root;
    [SerializeField] private SwipeContent _swipeContent;

    private List<ShopCell> _cells = new List<ShopCell>(50);
    
    [HideInInspector] public Character SelectedCharacter; //TODO rude

    public event Action<Character> Touched;

    public void Initialize(Characters characters, Character selectedCharacter)
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
        
        _swipeContent.Initialize(selectedCharacter.ID);
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
        
        _swipeContent.Initialize(SelectedCharacter.ID);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _cells.Count; i++)
            _cells[i].Touched -= OnTouched;
    }
}
