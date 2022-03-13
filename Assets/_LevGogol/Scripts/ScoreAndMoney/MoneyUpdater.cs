using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUpdater : MonoBehaviour
{

    private void OnEnable() {
        MoneyStorage.MoneyChanged += UpdateMoney;
        UpdateMoney(MoneyStorage.Count);
    }

    private void OnDisable() {
        MoneyStorage.MoneyChanged -= UpdateMoney;
    }

    private void UpdateMoney(int val) {
        GetComponent<TextMeshProUGUI>().text = val.ToString();
    }
}
