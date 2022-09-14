using TMPro;
using UnityEngine;

public class StartScreen : ScreenBase
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _maxScoreText;

    public void ChangeMoney(int money)
    {
        _moneyText.text = money.ToString();
    }

    public void ChangeMaxScore(int score)
    {
        _maxScoreText.text = score.ToString();
    }
}
