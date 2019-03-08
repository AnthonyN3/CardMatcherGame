using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource musicSource;

    public void MatchSound()
    {
        musicSource.clip = clips[0];
        musicSource.Play();
    }

    
    public void NonMatchSound()
    {
        musicSource.clip = clips[1];
        musicSource.Play();
    }

    public void CourseCleared()
    {
        musicSource.clip = clips[2];
        musicSource.Play();
    }

    public void GameOver()
    {
        musicSource.clip = clips[3];
        musicSource.Play();
    }

}
