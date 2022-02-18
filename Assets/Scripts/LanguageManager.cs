using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private Dictionary<string, LangText> texts;
    private TextUtility textUtility;

    private Language _language;
    public Language language 
    {
        get => _language;
        set 
        {
            _language = value;
            OnLanguageChange?.Invoke(_language);
        }
    }
    public event Action<Language> OnLanguageChange;

    public static LanguageManager instance { get; private set; }

    public void SetRussian() => language = Language.Russian;
    public void SetEnglish() => language = Language.English;

    public LangText GetTextById(string id) => texts[id];

    private void InitTexts()
    {
        texts = new Dictionary<string, LangText>();
        textUtility = new TextUtility();
        string filePath = Application.persistentDataPath + @"/Language.json";

        texts = textUtility.ReadJsonByPath(filePath);
    }

    private void SingletonCheck() 
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Awake()
    {
        SingletonCheck();
        InitTexts();
    }
}
