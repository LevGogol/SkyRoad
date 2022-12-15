using System;
using TMPro;
using UnityEngine;
using YG;

public class YandexLocalization : MonoBehaviour
{
    [SerializeField] private string _testLanguage;
    [SerializeField] private TextMeshProUGUILocalization[] _textMeshProUGUILocalization;
     
     private string _language;
     
     private void OnEnable()
     {
         Translate();
         YandexGame.GetDataEvent += Translate;
     }

     private void Translate()
     {
         if (String.IsNullOrEmpty(_language))
         {
             _language = YandexGame.EnvironmentData.language;
#if UNITY_EDITOR
             _language = _testLanguage;
#endif
         }
         
         if(_language == "ru")
             TranslateToRussia();
     }

     private void TranslateToRussia()
     {
         foreach (var localization in _textMeshProUGUILocalization)
        {
            localization.textMeshProUGUI.text = localization.ru;
            localization.textMeshProUGUI.font = localization.ruFont;
        }
    }

    private void TranslateToEnglish()
    {
        foreach (var localization in _textMeshProUGUILocalization)
        {
            localization.textMeshProUGUI.text = localization.en;
            localization.textMeshProUGUI.font = localization.enFont;
        }
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= Translate;
    }
}

[Serializable]
public struct TextMeshProUGUILocalization
{
    public TextMeshProUGUI textMeshProUGUI;
    public string ru;
    public TMP_FontAsset ruFont;
    public string en;
    public TMP_FontAsset enFont;
}
