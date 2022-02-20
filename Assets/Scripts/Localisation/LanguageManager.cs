using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset json;

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


    private void WriteSampleJson()
    {
        textUtility = new TextUtility();
        var json = textUtility.GenerateSampleJson();
        var filePath = Application.dataPath + @"\Jsons\Localisation.json";
        textUtility.WriteToJson(filePath, json);
    }

    private void InitTexts()
    {
        texts = new Dictionary<string, LangText>();
        textUtility = new TextUtility();
        texts = textUtility.JsonToDictionary(this.json.text);
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
