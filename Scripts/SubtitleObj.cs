using UnityEngine;

[CreateAssetMenu(fileName = "SubtitleObj", menuName = "ScriptableObjects/SubtitleObj", order = 1)]
public class SubtitleObj : ScriptableObject
{
    public AudioClip audioClip;
    public TextAsset srtFile;
}
