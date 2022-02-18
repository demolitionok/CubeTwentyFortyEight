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
    public Dictionary<Language, string> LanguageText;

    public string GetText(Language lang) 
    {
        return LanguageText[lang];
    }

    public LangText(Dictionary<Language, string> translations) 
    {
        LanguageText = translations;
    }
}
