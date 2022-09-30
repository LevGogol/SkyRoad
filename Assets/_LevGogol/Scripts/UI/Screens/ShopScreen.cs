using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class ShopScreen : ScreenBase
{
    [SerializeField] private Characters _characters;
    [SerializeField] private CharacterInfoForShop _characterInfoForShop;
    [SerializeField] private ShopGrid _shopGrid;
    [SerializeField] private int _buyCost;
    [SerializeField] private TextMeshProUGUI _buyCostText;
    [SerializeField] private AnimationCurve _showEasing;
    [SerializeField] private TextMeshProUGUI _moneyText;

    public event Action<Character> CharacterSelected;
    
    public int BuyCost => _buyCost;
    public Character SelectedCharacter { get; private set; }

    private void Start()
    {
        _shopGrid.Initialization(_characters, SelectedCharacter);
        _characterInfoForShop.DrawInfo(SelectedCharacter);

        _shopGrid.Touched += SelectCharacter;

        _buyCostText.text = _buyCost.ToString();
    }

    public void Initialize(Character selectedCharacter)
    {
        SelectedCharacter = selectedCharacter;
    }
    
    public void ChangeMoney(int money)
    {
        _moneyText.text = money.ToString();
    }

    public override void Show()
    {
        base.Show();
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero;
        rectTransform.DOScale(1, 1f).SetEase(_showEasing);
        _characterInfoForShop.DrawInfo(SelectedCharacter);
        _shopGrid.Refresh();
    }

    public override void Hide()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.DOScale(0, 1f).OnComplete(base.Hide);
    }

    public bool BuyRandom()
    {
        var randomIndex = GetRandomIndex();
        if (randomIndex == -1) return false;

        Character character = _characters.GetCharacter(randomIndex);

        character.IsBuy = true;

        SelectedCharacter = character;
        _characterInfoForShop.DrawInfo(character);

        _shopGrid.SelectedCharacter = character;
        _shopGrid.Refresh();

        return true;
    }

    private void SelectCharacter(Character character)
    {
        _characterInfoForShop.DrawInfo(character);
        SelectedCharacter = character;
        CharacterSelected.Invoke(character);
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
}