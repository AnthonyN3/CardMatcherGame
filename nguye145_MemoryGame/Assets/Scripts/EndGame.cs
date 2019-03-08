using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameObject YouWin;
    public GameObject YouLose;
    public GameObject AudioManager;

    void Awake()
    {
        //Although we already moved these objects via inspector
        //It is a good habit to also find the objects and set them
        AudioManager = GameObject.Find("AudioObject");

        YouWin.SetActive(false);
        YouLose.SetActive(false);
    }


    void Start()
    {


        GameObject scoreGO = GameObject.Find("Score");
        scoreText = scoreGO.GetComponent<TextMeshProUGUI>();    //Assigns the scoteText to the reference of the component in Score GO
        scoreText.text = "SCORE: " + PlayerData.gameScore.ToString();

        //using GameObject scoreGO to find new Object named "Time" 
        scoreGO = GameObject.Find("Time");
        timeText = scoreGO.GetComponent<TextMeshProUGUI>();
        timeText.text = "TIME: " + PlayerData.gameTime.ToString() + " SEC";
        
        if(PlayerData.gameScore == 0)
            YouLose.SetActive(true);
        else
            YouWin.SetActive(true);

        PlayAudio();

    }

    private void PlayAudio()
    {
        if(PlayerData.gameScore == 0)
            AudioManager.GetComponent<MusicManager>().GameOver();
        else 
            AudioManager.GetComponent<MusicManager>().CourseCleared();
    }


}
