using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public void ChangeText(int score) 
    {
        text.text = score.ToString();
    }
}
