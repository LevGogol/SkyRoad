using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartCountdown : MonoBehaviour
{
    [SerializeField] private InputFacade _input;
    [SerializeField] private Image _backgroundPanel;
    [SerializeField] private float _timeout;

    private void OnEnable()
    {
        _input._UIButtons.PauseOffDowned += StartCountdown;
    }

    private void OnDisable()
    {
        _input._UIButtons.PauseOffDowned -= StartCountdown;
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine(_timeout));
    }

    private IEnumerator CountdownCoroutine(float timeout)
    {
        var timeCount = timeout;
        var initialBgColor = _backgroundPanel.color;
        while (timeCount > 0.0f)
        {
            timeCount -= Time.deltaTime;
            _backgroundPanel.color = new Color(initialBgColor.r, initialBgColor.g, initialBgColor.b,
                initialBgColor.a * timeCount / timeout);
            yield return null;
        }
    }
}
