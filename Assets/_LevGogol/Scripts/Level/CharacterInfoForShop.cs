using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoForShop : MonoBehaviour
{
    [SerializeField] private Image _selectedSpriteImage;
    [SerializeField] private GameObject[] _lifeImages;
    [SerializeField] private GameObject _flyImage;
    [SerializeField] private GameObject _destroyImage;

    public void DrawInfo(Character character)
    {
        _selectedSpriteImage.sprite = character.Sprite;
        _selectedSpriteImage.SetNativeSize();
        ChangeAbilityImages(character);
    }

    private void ChangeAbilityImages(Character character)
    {
        for (int i = 0; i < _lifeImages.Length; i++) {
            _lifeImages[i].SetActive(i < character.LifeCount);
        }
        
        _flyImage.SetActive(character.CanJump);
        _destroyImage.SetActive(character.CanDestroyCloud);
    }
}