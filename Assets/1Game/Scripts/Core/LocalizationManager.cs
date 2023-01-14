using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private string _currentlanguage;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage==SystemLanguage.Russian||Application.systemLanguage==SystemLanguage.Ukrainian||Application.systemLanguage==SystemLanguage.Belarusian)
            {
                PlayerPrefs.SetString("Language","ru_RU");
            }
            else if (Application.systemLanguage == SystemLanguage.Turkish)
            {
                PlayerPrefs.SetString("Language", "tr_TR");
            }
            else 
            {
                PlayerPrefs.SetString("Language", "en_E");
            }
        }
    }
}