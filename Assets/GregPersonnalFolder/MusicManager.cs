using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] playlist;
    AudioSource audioSource;
    private int musicIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextMusic();
        }
    }

    private void PlayNextMusic()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public void PlayThisSong(int index)
    {
        if (index <= playlist.Length && index >= 0)
        {
            musicIndex = index;
            audioSource.clip = playlist[musicIndex];
            audioSource.Stop();
            audioSource.Play();
        }
        else
        {
            Debug.Log("PlayThisSong parameter out of index");
        }
    }
}
