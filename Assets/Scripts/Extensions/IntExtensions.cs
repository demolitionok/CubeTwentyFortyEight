using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IntExtensions
{
    public static Color IntToRGB(this int input) 
    {
        //from 2, 4, 8, 16 to 1024, 2048 are 11 integers
        //To make color change noticable even at small numbers (2 -> 4)
        //I made temp that decides what number does integer have (2 - 1st... 2048 - 11th)
        //Value made like this to make colors darker with each 2048 loop
        var temp = Mathf.Log(input, 2);
        var value = 0.9f - (input / 2048)/10f;
        var saturation = 1f;
        var hue = (temp / 11f) % 1;
        Debug.Log($"value : {value}, saturation : {saturation}, hue : {hue}");

        var result = Color.HSVToRGB(hue, saturation, value);
        return result;
    }
}
