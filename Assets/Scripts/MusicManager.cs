using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip backgroundMusicClip;
    public AudioClip errorSound;
    public AudioClip winSound;
    public AudioClip failSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        PlayMusic();
    }

     void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void MuteMusic(bool mute)
    {
        audioSource.mute = mute;
    }

    public void PlayErrorSound()
    {
        audioSource.PlayOneShot(errorSound);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }

    public void PlayFailSound()
    {
        audioSource.PlayOneShot(failSound);
    }
}
