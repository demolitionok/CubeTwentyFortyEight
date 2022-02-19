using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensions
{
    public static Color IntToRGB(this int input) 
    {
        var value = 0.9f - (input / 2048)/10f;
        var saturation = 1f;
        var hue = (input / 2048f) % 1;
        Debug.Log($"value : {value}, saturation : {saturation}, hue : {hue}");

        var result = Color.HSVToRGB(hue, saturation, value);
        return result;
    }
}
