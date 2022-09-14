using System;
using UnityEngine;


[Serializable]
public class AudioTrack
{
    [HideInInspector] public string ID;
    [HideInInspector] public TrackName Name;
    public AudioClipWithSettings[] ClipsWithSettings;
}