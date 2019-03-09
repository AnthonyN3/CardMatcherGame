using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {   
        //Main menu is set as the first index (aka 0) in the build, and the game is set to index 1
        SceneManager.LoadScene("Game");        //Increment to the next index in the build scene
        
        //Alternative (if Menu is first build and Game is second build in the index)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;    //Used to quit in Unity Editor
        
        
        Application.Quit(); //NOTE: will not quit while in Unity Editor (only quits after buildt game)
    }

    //Used for the OPTION to change number of unique cards in game
    //for button
    public void UniqueCards(int input)
    {
        PlayerData.numOfCards = input;
    }

    public void Menu()
    {
        //Loads the Main menu scene
        SceneManager.LoadScene("Menu");
    }

}
