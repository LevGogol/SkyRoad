using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ShopScreen : ScreenBase
{
    [SerializeField] private Characters _characters;
    [SerializeField] private CharacterInfoForShop _characterInfoForShop;
    [SerializeField] private ShopGrid _shopGrid;

    private int _selectedCharacterIndex;

    private void Start()
    {
        InitIsBuy();

        _selectedCharacterIndex = PlayerPrefs.GetInt("Character", 0);
        _shopGrid.Initialization(_characters, _selectedCharacterIndex);
        _characterInfoForShop.DrawInfo(_characters.GetCharacter(_selectedCharacterIndex));

        _shopGrid.Touched += SelectCharacter;
    }

    private void InitIsBuy()
    {
        if (_characters.Count > 32)
        {
            Debug.LogError("Can't save all characters");
        }

        int mask = PlayerPrefs.GetInt("BoughtMask", 1);
        for (int i = 0; i < _characters.Count; i++)
        {
            if ((mask >> i) % 2 == 1)
                _characters.GetCharacter(i).IsBuy = true;
        }
    }

    private void SelectCharacter(int ID)
    {
        _characterInfoForShop.DrawInfo(_characters.GetCharacter(ID));
        
        PlayerPrefs.SetInt("Character", ID);
    }

    private void BuyRandom()
    {
        var randomIndex = GetRandomIndex();
        if (randomIndex == -1) return;
        
        Character character = _characters.GetCharacter(randomIndex);

        MoneyStorage.Count -= character.Price;
        character.IsBuy = true;

        PlayerPrefs.SetInt("Character", randomIndex);

        int mask = PlayerPrefs.GetInt("BoughtMask", 1) + (1 << randomIndex); //TODO move to save
        PlayerPrefs.SetInt("BoughtMask", mask);

        _characterInfoForShop.DrawInfo(character);
        _shopGrid.Refresh();
    }

    private int GetRandomIndex()
    {
        List<int> notPurchasedCharacters = new List<int>(); //TODO inefficient
        for (int i = 0; i < _characters.Count; i++)
        {
            if (!_characters.GetCharacter(i).IsBuy)
            {
                notPurchasedCharacters.Add(i);
            }
        }
        
        if (notPurchasedCharacters.Count == 0)
        {
            return -1;
        }

        return notPurchasedCharacters[Random.Range(0, notPurchasedCharacters.Count)];
    }

    [ContextMenu("Clear")]
    private void ClearSaveSystem() //TODO move to save System
    {
        PlayerPrefs.SetInt("Character", _selectedCharacterIndex);
        
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters.GetCharacter(i).IsBuy = false;
        }
    }

    public void Undo() {
        SceneManager.LoadScene(0);
    }
}