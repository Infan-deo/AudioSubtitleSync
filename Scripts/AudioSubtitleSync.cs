using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioSubtitleSync : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Text subtitleText;
    public List<SubtitleObj> subtitleObjects;
    public int subtitleIndex;
    public bool play = false;

    private List<SubtitleParser.Subtitle> subtitles;
    private float currentTime;

    void Start()
    {
        UpdateSub();
    }
    private void UpdateSub()
    {
        if (subtitleIndex >= 0 && subtitleIndex < subtitleObjects.Count)
        {
            SubtitleObj currentSubtitleObj = subtitleObjects[subtitleIndex];
            audioSource.clip = currentSubtitleObj.audioClip;
            subtitles = SubtitleParser.ParseSRT(currentSubtitleObj.srtFile);

        }
    }
    void Update()
    {
        if(play == true)
        {
            UpdateSub();
            audioSource.Play();
            play = false;
        }
        if (audioSource.isPlaying)
        {
            currentTime = audioSource.time;
            DisplaySubtitle();
        }
    }

    private void DisplaySubtitle()
    {
        foreach (var subtitle in subtitles)
        {
            if (currentTime >= subtitle.startTime && currentTime <= subtitle.endTime)
            {
                subtitleText.text = subtitle.text;
                return;
            }
        }
        subtitleText.text = string.Empty; 
    }
}
