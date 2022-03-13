using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour {
    private void OnEnable() {
        ScoreStorage.ScoreChanged += UpdateScore;
        UpdateScore(ScoreStorage.MaxCount);
    }

    private void OnDisable() {
        ScoreStorage.ScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int val) {
        GetComponent<TextMeshProUGUI>().text = val.ToString();
    }
}