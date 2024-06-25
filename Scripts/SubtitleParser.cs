using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class SubtitleParser
{
    public struct Subtitle
    {
        public float startTime;
        public float endTime;
        public string text;
    }

    public static List<Subtitle> ParseSRT(TextAsset srtFile)
    {
        var subtitles = new List<Subtitle>();

        string[] lines = srtFile.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        for (int i = 0; i < lines.Length; i++)
        {
            if (int.TryParse(lines[i], out int index))
            {
                string timeLine = lines[++i];
                string text = string.Empty;

                while (++i < lines.Length && !string.IsNullOrEmpty(lines[i]))
                {
                    text += lines[i] + "\n";
                }

                Match match = Regex.Match(timeLine, @"(\d{2}):(\d{2}):(\d{2}),(\d{3}) --> (\d{2}):(\d{2}):(\d{2}),(\d{3})");
                if (match.Success)
                {
                    float startTime = (float)TimeSpan.Parse(match.Groups[1].Value + ":" + match.Groups[2].Value + ":" + match.Groups[3].Value + "." + match.Groups[4].Value).TotalSeconds;
                    float endTime = (float)TimeSpan.Parse(match.Groups[5].Value + ":" + match.Groups[6].Value + ":" + match.Groups[7].Value + "." + match.Groups[8].Value).TotalSeconds;

                    subtitles.Add(new Subtitle { startTime = startTime, endTime = endTime, text = text });
                }
            }
        }

        return subtitles;
    }
}
