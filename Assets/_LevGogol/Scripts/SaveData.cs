using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/SaveData", order = 2)]
public class SaveData : ScriptableObject
{
    [SerializeField] private Character _defaultCharacter;
    [SerializeField] private Character _overadedCharacter;

    public int MaxScore
    {
        get => PlayerPrefs.GetInt("MaxScore", 0);
        set => PlayerPrefs.SetInt("MaxScore", value);
    }

    public int MoneyCount
    {
        get => PlayerPrefs.GetInt("MoneyCount", 0);
        set => PlayerPrefs.SetInt("MoneyCount", value);
    }

    public Character CurrentCharacter
    {
        get
        {
            var jsonCharacter = PlayerPrefs.GetString("CurrentCharacter", String.Empty);
            if (jsonCharacter == String.Empty)
                return _defaultCharacter;
            
            JsonUtility.FromJsonOverwrite(jsonCharacter, _overadedCharacter);
            return _overadedCharacter;
        }
        set
        {
            PlayerPrefs.SetString("CurrentCharacter", JsonUtility.ToJson(value)); //TODO can't edit Charater's fields
        }
    }

    public void SaveCharacterIsBuy(int ID)
    {
        PlayerPrefs.SetInt("ID" + ID, 1);
    }

    public bool LoadCharacterIsBuy(int ID)
    {
        if (ID == _defaultCharacter.ID)
            return true;

        return PlayerPrefs.GetInt("ID" + ID, 0) == 1;
    }
}