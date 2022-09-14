using System;
using UnityEngine;


[Serializable]
public class AudioClipWithSettings
{
    public AudioClip Clip;

    [Range(0f, 1f)] public float Volume = 1f;

    [Range(0f, 3f)] public float Pitch = 1f;

    public AudioClipWithSettings(AudioClip clip = null)
    {
        Clip = clip;
    }
}