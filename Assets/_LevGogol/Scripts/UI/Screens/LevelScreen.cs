using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelScreen : ScreenBase
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private RectTransform _coinImagePrefab;
    [SerializeField] private RectTransform _coinImage;
    [SerializeField] private Camera _camera;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void ChangeScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void ChangeMoney(int money)
    {
        _moneyText.text = money.ToString();
    }
    
    public void PlayCoinAnimation(Vector3 worldPosition)
    {
        var startPointOnScreen = _camera.WorldToScreenPoint(worldPosition);

        var coinForAnimation = Instantiate(_coinImagePrefab, transform);
        coinForAnimation.position = startPointOnScreen;
        coinForAnimation.DOMove(_coinImage.position, 1f).SetEase(Ease.OutQuint).OnComplete(() => coinForAnimation.gameObject.SetActive(false));
    }
}
