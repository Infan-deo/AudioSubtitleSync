using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using TamilEncoder;

public class SubtitleConverter : MonoBehaviour
{
    public TextAsset srtFile;  
    public string outputFileName = "ConvertedSubtitles"; 

    void Start()
    {
        if (srtFile != null)
        {
            
            string convertedText = ConvertSRTToTSCII(srtFile.text);

            
            SaveConvertedText(convertedText, outputFileName);
        }
        else
        {
            Debug.LogError("SRT file not assigned.");
        }
    }

    private string ConvertSRTToTSCII(string srtContent)
    {
        
        string pattern = @"(\d+)\r?\n(\d{2}:\d{2}:\d{2},\d{3}) --> (\d{2}:\d{2}:\d{2},\d{3})\r?\n((?:.+\r?\n?)*)\r?\n";
        Regex regex = new Regex(pattern, RegexOptions.Compiled);
        MatchCollection matches = regex.Matches(srtContent);

        StringBuilder convertedContent = new StringBuilder();

        foreach (Match match in matches)
        {
            
            string index = match.Groups[1].Value;
            string startTime = match.Groups[2].Value;
            string endTime = match.Groups[3].Value;
            string subtitleText = match.Groups[4].Value.Trim();

            
            string convertedText = TamilEncoding.ConvertFromUnicode(subtitleText, TamilFontEncoding.TSCII);

            
            convertedContent.AppendLine(index);
            convertedContent.AppendLine($"{startTime} --> {endTime}");
            convertedContent.AppendLine(convertedText);
            convertedContent.AppendLine();
        }

        return convertedContent.ToString();
    }

    private void SaveConvertedText(string text, string fileName)
    {
        
        string path = Path.Combine(Application.dataPath, "SRT Format Text Asset/", fileName + ".txt");

        File.WriteAllText(path, text, Encoding.UTF8);
        Debug.Log("Converted SRT file saved at: " + path);

        
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
