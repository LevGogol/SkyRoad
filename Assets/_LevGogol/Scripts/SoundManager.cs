using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(Clip clip, float pitch = 1f)
    {
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clips[(int) clip]);
    }
}

public enum Clip
{
    Damage = 0,
    Coin,
    Button,
    Heart,
    Jump,
    DestroyCloud,
    Cloud,
}