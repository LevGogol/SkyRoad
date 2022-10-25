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
    [SerializeField] private GameObject[] _hearts;

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void SetMoney(int money)
    {
        _moneyText.text = money.ToString();
    }
    
    public void SetMaxScore(int score)
    {
        _maxScoreText.text = score.ToString();
    }

    public void SetHearts(int value)
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            var isEnable = i < value;
            _hearts[i].SetActive(isEnable);
        }
    }
    
    public void PlayCoinAnimation(Vector3 worldPosition)
    {
        var startPointOnScreen = _camera.WorldToScreenPoint(worldPosition);

        var coinForAnimation = Instantiate(_coinImagePrefab, transform);
        coinForAnimation.position = startPointOnScreen;
        var sequenceCoin = DOTween.Sequence();
        sequenceCoin.Append(coinForAnimation.DOMove(_coinImage.position, _moneySpeedDuration).SetEase(Ease.OutQuint)).
            Join(coinForAnimation.DOScale(Vector2.zero, _moneyScaleDuration).SetEase(Ease.InExpo)).
            OnComplete(() => Destroy(coinForAnimation.gameObject));
    }
}
