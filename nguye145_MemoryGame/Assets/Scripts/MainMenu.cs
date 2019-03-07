using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {   
        //Main menu is set as the first index (aka 0) in the build, and the game is set to index 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        //Increment to the next index in the build scene
    }

    public void Exit()
    {
        Debug.Log("QUIT GAME");
        Application.Quit(); //NOTE: will not quit while in Unity Editor (only quits after buildt game)
    }

    public void UniqueCards(int input)
    {
        PlayerData.numOfCards = input;
        Debug.Log(PlayerData.numOfCards);
    }

    public void Menu()
    {
        //Loads the Main menu scene
        SceneManager.LoadScene("Menu");
    }

}
