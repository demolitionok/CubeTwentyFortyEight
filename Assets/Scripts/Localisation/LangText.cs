using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language 
{
    Russian,
    English
}

[Serializable]
public class LangText
{
    public Dictionary<Language, string> translations;

    public string GetText(Language lang) 
    {
        return translations[lang];
    }

    public LangText(Dictionary<Language, string> translations) 
    {
        this.translations = translations;
    }
}
