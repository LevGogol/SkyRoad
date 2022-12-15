using System;
using UnityEngine;
using YG;

public class YandexLiderboard : MonoBehaviour
{
    [SerializeField] private Game _game;

    private void OnEnable()
    {
        _game.ScoreStorage.MaxScoreChanged += SendMaxScore;
    }

    private void SendMaxScore(int maxScore)
    {
        YandexGame.NewLeaderboardScores( "main", maxScore);
    }

    private void OnDisable()
    {
        _game.ScoreStorage.MaxScoreChanged -= SendMaxScore;
    }
}
