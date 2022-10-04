using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelScreen : ScreenBase
{
    [SerializeField] private float _moneyScaleDuration = 0.3f;
    [SerializeField] private float _moneySpeedDuration = 1f;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private RectTransform _coinImagePrefab;
    [SerializeField] private RectTransform _coinImage;
    [SerializeField] private Camera _camera;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _maxScoreText;

    public void ChangeScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void ChangeMoney(int money)
    {
        _moneyText.text = money.ToString();
    }
    
    public void ChangeMaxScore(int score)
    {
        _maxScoreText.text = score.ToString();
    }
    
    public void PlayCoinAnimation(Vector3 worldPosition)
    {
        var startPointOnScreen = _camera.WorldToScreenPoint(worldPosition);

        var coinForAnimation = Instantiate(_coinImagePrefab, transform);
        coinForAnimation.position = startPointOnScreen;
        coinForAnimation.DOScale(_moneyScaleDuration, _moneySpeedDuration);
        coinForAnimation.DOMove(_coinImage.position, _moneySpeedDuration).SetEase(Ease.OutQuint).OnComplete(() => Destroy(coinForAnimation.gameObject));
    }
}
