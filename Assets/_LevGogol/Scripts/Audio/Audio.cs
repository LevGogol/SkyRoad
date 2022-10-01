using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Audio : MonoBehaviour
{
    [HideInInspector] public List<AudioTrack> Tracks;

    private AudioSource _soundSource;
    private AudioSource _musicSource;
    private Volume _gameVolume;

    private static Audio _instance;

    public static Audio Instance => _instance;

    public float MusicVolume
    {
        get => _gameVolume.Music;
        set => _gameVolume.Music = value;
    }

    public float SoundVolume
    {
        get => _gameVolume.Sound;
        set => _gameVolume.Sound = value;
    }

    private void Awake()
    {
        _instance = this;

        _soundSource = new GameObject("SoundSource").AddComponent<AudioSource>();
        _soundSource.playOnAwake = false;
        _soundSource.transform.parent = transform;

        _musicSource = new GameObject("MusicSource").AddComponent<AudioSource>();
        _musicSource.playOnAwake = false;
        _musicSource.loop = true;
        _musicSource.transform.parent = transform;

        _gameVolume.Music = 1f;
        _gameVolume.Sound = 1f;
    }

    public void PlayClip(TrackName clipKey)
    {
        var track = Tracks[(int) clipKey];
        if (track != null)
        {
            _soundSource.Stop();

            var clipWithSettings = GetRandomAudioClipWithSettings(track.ClipsWithSettings);
            _soundSource.pitch = clipWithSettings.Pitch;
            _soundSource.volume = clipWithSettings.Volume * _gameVolume.Sound;
            _soundSource.clip = clipWithSettings.Clip;
            _soundSource.Play();
        }
    }

    public void PlayClipOneShot(TrackName clipKey)
    {
        var track = Tracks[(int) clipKey];
        if (track != null)
        {
            // _soundSource.Stop();

            var clipWithSettings = GetRandomAudioClipWithSettings(track.ClipsWithSettings);
            _soundSource.pitch = clipWithSettings.Pitch;
            _soundSource.PlayOneShot(clipWithSettings.Clip,
                clipWithSettings.Volume * _gameVolume.Sound);
        }
    }

    public void PlayMusic(TrackName musicKey)
    {
        var track = Tracks[(int) musicKey];

        if (track.ClipsWithSettings.Length != 0)
        {
            var clipWithSettings = GetRandomAudioClipWithSettings(track.ClipsWithSettings);
            _musicSource.volume = clipWithSettings.Volume * _gameVolume.Music;
            _musicSource.pitch = clipWithSettings.Pitch;
            _musicSource.clip = clipWithSettings.Clip;
            _musicSource.Play();
        }
        else
        {
            Debug.LogError("Zero tracks in Audio Editor. Fill tracks, please");
        }
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void ContinueMusic()
    {
        _musicSource.UnPause();
    }

    public void Mute()
    {
        _soundSource.mute = true;
        _musicSource.mute = true;
    }

    public void UnMute()
    {
        _soundSource.mute = false;
        _musicSource.mute = false;
    }

    private AudioClipWithSettings GetRandomAudioClipWithSettings(AudioClipWithSettings[] clip)
    {
        return clip[Random.Range(0, clip.Length)];
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}