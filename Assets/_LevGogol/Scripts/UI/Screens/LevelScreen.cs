using DG.Tweening;
using UnityEngine;

public class LevelScreen : ScreenBase
{
    [SerializeField] private RectTransform _coinImagePrefab;
    [SerializeField] private RectTransform _coinImage;
    [SerializeField] private Camera _camera;
        
    public void PlayCoinAnimation(Vector3 worldPosition)
    {
        var startPointOnScreen = _camera.WorldToScreenPoint(worldPosition);

        var coinForAnimation = Instantiate(_coinImagePrefab, transform);
        coinForAnimation.position = startPointOnScreen;
        coinForAnimation.DOMove(_coinImage.position, 1f).SetEase(Ease.OutQuint).OnComplete(() => coinForAnimation.gameObject.SetActive(false));
    }
}
