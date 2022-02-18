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
    private void OnEnable()
    {
        LanguageManager.instance.OnLanguageChange += ChangeText;
    }
    private void OnDisable()
    {
        LanguageManager.instance.OnLanguageChange -= ChangeText;
    }

    private void Start()
    {
        langText = LanguageManager.instance.GetTextById(id);
        Debug.Log(langText.ToString());
        textComponent.text = langText.GetText(LanguageManager.instance.language);
    }
}
