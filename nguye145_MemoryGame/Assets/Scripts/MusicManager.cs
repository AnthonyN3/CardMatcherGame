using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Audio manager
public class MusicManager : MonoBehaviour
{

    //Audio clips are stored in a array
    public AudioClip[] clips;
    //The AudioSource is how the audio is able to play
    public AudioSource musicSource;

    //Sound used for when two cards match
    public void MatchSound()
    {
        musicSource.clip = clips[0];
        musicSource.Play();
    }

    //Sound used for when two cards do not match    
    public void NonMatchSound()
    {
        musicSource.clip = clips[1];
        musicSource.Play();
    }

    //sound used for when all cards are matched
    public void CourseCleared()
    {
        musicSource.clip = clips[2];
        musicSource.Play();
    }

    //sound used for when points reach 0 (you lost)
    public void GameOver()
    {
        musicSource.clip = clips[3];
        musicSource.Play();
    }

}
