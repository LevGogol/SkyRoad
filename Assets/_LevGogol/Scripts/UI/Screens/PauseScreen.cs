using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class PauseScreen : ScreenBase
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private int _timeout;

    public event Action Hidden;

    public override void Show()
    {
        base.Show();
        _countdownText.gameObject.SetActive(false);
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;
    }

    public override void Hide()
    {
        _canvasGroup.interactable = false;
        _countdownText.gameObject.SetActive(true);
        ResetCountdownText(_timeout);

        var sequencePanel = DOTween.Sequence();
        sequencePanel.SetUpdate(true);
        sequencePanel.Append(_canvasGroup.DOFade(0.0f, _timeout)).
            AppendCallback(() => {
                base.Hide();
                _canvasGroup.alpha = 1.0f;
                Hidden?.Invoke();
            });

        var sequenceCountdownText = DOTween.Sequence();
        sequenceCountdownText.SetUpdate(true);
        for (int i = _timeout - 1; i >= 0; --i)
        {
            var time = i;
            sequenceCountdownText.
                Append(_countdownText.transform.DOScale(0.5f, 1.0f)).
                AppendCallback(() =>
                {
                    ResetCountdownText(time);
                });
        }
    }

    private void ResetCountdownText(int val)
    {
        _countdownText.transform.localScale = Vector3.one;
        _countdownText.text = val.ToString();
    }
}
