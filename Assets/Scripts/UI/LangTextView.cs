using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangTextView : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;
    [SerializeField]
    private string id;

    private LangText langText;

    private void ChangeText(Language language) 
    {
        textComponent.text = langText.GetText(language);
    }

    private void SubscribeToManager()
    {
        if (LanguageManager.instance != null)
            LanguageManager.instance.OnLanguageChange += ChangeText;
    }
    private void OnEnable() => SubscribeToManager();
    private void OnDisable()
    {
        LanguageManager.instance.OnLanguageChange -= ChangeText;
    }

    //OnEnable() of this script for some reason is called BEFORE LanguageManager's Awake(),
    //so it throws NullReferenceException. Subscribition in start fixes it.
    private void Start()
    {
        SubscribeToManager();

        langText = LanguageManager.instance.GetTextById(id);
        textComponent.text = langText.GetText(LanguageManager.instance.language);
    }
}
