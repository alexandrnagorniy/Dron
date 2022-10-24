using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Localization 
{
    public string key;
    public string[] localization;

}

public class LocalizationController : MonoBehaviour
{
    public static LocalizationController Instance;
    public Localization[] localizations;
    public bool language;
    private void Awake()
    {
        Instance = this;
        language = System.Convert.ToBoolean(PlayerPrefs.GetInt("lang"));
    }

    public string GetLanguageText(string value) 
    {
        string s = null;
        foreach (var item in localizations)
        {
            if (item.key == value)
                s = item.localization[System.Convert.ToInt32(language)];
        }
        return s;
    }
    public void CheckLanguage(bool value) 
    {
        language = value;
        PlayerPrefs.SetInt("lang", System.Convert.ToInt32(language));
    }
}
