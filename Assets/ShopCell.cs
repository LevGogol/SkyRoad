using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour, IPointerDownHandler
{
    private const float BACKGROUND_SIZE_X = 125f;
    private const float SPACING = 0.9f;

    public Character Character => _character;
    
    [SerializeField] private Image _characterImage;
    [SerializeField] private Image _highlight;

    private Character _character;

    public event Action<int> Touched;

    public void Initialization(Character character)
    {
        _character = character;
        if (character.IsBuy)
        {
            DrawCharacterSprite(character);
            _characterImage.color = Color.white;
        }
        else
        {
            DrawCharacterSprite(character);
            _characterImage.color = Color.black;
        }
        
        DisableHighlight();
    }

    public void EnableHighlight()
    {
        _highlight.gameObject.SetActive(true);
    }

    public void DisableHighlight()
    {
        _highlight.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Touched?.Invoke(_character.ID);
    }

    public void Refresh()
    {
        Initialization(_character);
    }

    private void DrawCharacterSprite(Character character)
    {
        _characterImage.sprite = character.Sprite;
        _characterImage.SetNativeSize();

        var characterSizeDelta = _characterImage.rectTransform.sizeDelta;
        var multiplyer = BACKGROUND_SIZE_X /
                         (characterSizeDelta.x > characterSizeDelta.y ? characterSizeDelta.x : characterSizeDelta.y);

        characterSizeDelta *= multiplyer * SPACING;
        _characterImage.rectTransform.sizeDelta = characterSizeDelta;
    }
}
