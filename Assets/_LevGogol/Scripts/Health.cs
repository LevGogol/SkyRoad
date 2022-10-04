using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _showDuration;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private TextMeshPro _health;
    
    private int _lifeCount;

    public event Action Ended;
    
    public int Value
    {
        get { return _lifeCount; }
        set
        {
            _lifeCount = value;
            _health.text = _lifeCount.ToString();

            var sequenceText = DOTween.Sequence();
            sequenceText.Append(_health.DOFade(1f, _fadeDuration)).
                AppendInterval(_showDuration).
                Append(_health.DOFade(0f, _fadeDuration));

            var sequenceSprite = DOTween.Sequence();
            sequenceSprite.Append(_sprite.DOFade(1f, _fadeDuration)).
                AppendInterval(_showDuration).
                Append(_sprite.DOFade(0f, _fadeDuration));

            if (_lifeCount <= 0)
                Ended?.Invoke();
        }
    }
}