using System.Collections;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public class TextUtility
{
    public Dictionary<Language, string> StringRussianEnglish(string ru, string en)
    {
        var tempText = new Dictionary<Language, string>();
        tempText.Add(Language.Russian, ru);
        tempText.Add(Language.English, en);

        return tempText;
    }

    public string GenerateSampleJson()
    {
        var texts = new Dictionary<string, LangText>();

        var test = StringRussianEnglish("это тестовый текст", "this is a test text");
        var play = StringRussianEnglish("Играть", "Play");

        texts.Add("test", new LangText(test));
        texts.Add("play", new LangText(play));
        var content = JsonConvert.SerializeObject(texts);

        return content;
    }

    public Dictionary<string, LangText> ReadJsonByPath(string filePath)
    {
        string json = File.ReadAllText(filePath);
        using (StreamReader outputFile = new StreamReader(filePath))
        {
            json = outputFile.ReadToEnd();
        }
        var texts = JsonConvert.DeserializeObject<Dictionary<string, LangText>>(json);

        return texts;
    }
    public void WriteToJson(string filePath, Dictionary<string, LangText> texts)
    {
        var json = JsonConvert.SerializeObject(texts);

        using (StreamWriter outputFile = new StreamWriter(filePath))
        {
            outputFile.WriteLine();
        }
    }
}
