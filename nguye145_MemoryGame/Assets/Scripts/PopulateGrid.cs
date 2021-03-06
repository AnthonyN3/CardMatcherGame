﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //For Unity UI (Canvas, buttons, texts, etc)    
using TMPro;            //For TextMeshPro 
using System.Diagnostics;       //Used for timeing the game (stopwatch)
using UnityEngine.SceneManagement;  //used to navigate to other scenes (End game scene)

public class PopulateGrid : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject AudioManager;

    [SerializeField]
    private Sprite[] Images = new Sprite[10];
    //private int[] idNumbers = {0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9};
    private int[] idNumbers = new int[PlayerData.numOfCards*2];     
    private int pressed = 0;
    private int cardOne = -1, cardTwo = -2;
    private GameObject CardOne, CardTwo;
    public bool isPressed = true;
    private int cardsMatched;

    //Used for the grid layout
    private int rows;

    //SCORE
    public TextMeshProUGUI scoreText;
    private int score;

    //public Stopwatch timer;
    Stopwatch timer;

    // Start is called before the first frame update
    void Start()
    {
        cardsMatched = 0;
        isPressed = true;

        //Start timer
        timer = new Stopwatch();
        timer.Start();
    

        //Finds the gameobbject named "Score"
        GameObject scoreGO = GameObject.Find("Score");
        scoreText = scoreGO.GetComponent<TextMeshProUGUI>();    //Assigns the scoteText to the reference of the component in Score GO
        score = 1000;
        scoreText.text = score.ToString();  //Sets the text score to 1000 (Default)

        populate(); //Populates the grid/screen with cards

    }

    public void populate()
    {
        
        if(PlayerData.numOfCards == 6)
            rows = 4;
        else if(PlayerData.numOfCards == 7 || PlayerData.numOfCards == 8)
            rows = 5;
        else if (PlayerData.numOfCards == 9 || PlayerData.numOfCards == 10)
            rows = 6;
        
        //Since grid layout group does not scale the cells of instantiated object..
        //We created a formula/equation that will scale it accordingly to the grids length and width and the number of cards
        RectTransform parentRect = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(parentRect.rect.width/5, parentRect.rect.height/rows);

        GameObject newObj; 

        int y = 0;  //We pair every id.. Therefore we need another incrementor
        //Fills the correct size array with 2 id numbers that are used for pairing
        for(int i = 0 ; i < PlayerData.numOfCards; i++)
        {
            //Pairing 2 id values
            idNumbers[y] = i;
            idNumbers[y+1] = i;

            y = y+2;    //Increment by 2 to give each id a pair
        }

        //Shufle the idNumbers array
        idNumbers = ShuffleArray(idNumbers);

        //Instantiating cards(buttons) onto the grid
        for(int i = 0; i < PlayerData.numOfCards*2 ; i++)
        {
            newObj = (GameObject)Instantiate(cardPrefab, transform);    //create instance of a card
            newObj.GetComponent<Card>().id = idNumbers[i];              //Assign that card a id
            newObj.transform.GetChild(0).GetComponent<Image>().sprite = Images[newObj.GetComponent<Card>().id];     //Assign the card's image based off of its id
        }
            
    }

    private int[] ShuffleArray(int[] num)
    {
        int[] newArray = num.Clone() as int[];  //num.clone is a object..Unable to convert object to a int [] therefore we must "as int[]"; it
        
        //Loop used for shuffling the array
        for(int i = 0 ; i < newArray.Length; i++)
        {
            int temp = newArray[i]; //Hold the value inside newArray[i] (Becuse we will change it)
            int r = Random.Range(i, newArray.Length);   //NOTE this is max exclusive since these are integers and not floats
            
            //Swaping the two values in the two indexes
            newArray[i] = newArray[r]; 
            newArray[r] = temp;
        }
        
        return newArray;
    }

    public void ClickCard(int clickCardsId, GameObject thisCard)
    {
        
        pressed++;  //increment the pressed (used to calculate if there were 1 or 2 cards pressed)

        if(pressed == 1)
        {   
            //If prsesed == 1, that means only 1 card has be selected (and its waiting for its pair to be selected) 
            cardOne = clickCardsId;
            CardOne = thisCard;

            return; //exit out
        }
        else if (pressed == 2)
        {   
            cardTwo = clickCardsId;
            CardTwo = thisCard;
        }

        //Checks whether a card has been pressed (this is also used to deal with spamming cards)
        if(isPressed)
        {     
            pressed = 0; 
            isPressed = false;  //So user cannot spam cards  
            
            //Checks which audio to play depending on if the cards id/image match
            if(cardOne == cardTwo)
                AudioManager.GetComponent<MusicManager>().MatchSound();
            else
                AudioManager.GetComponent<MusicManager>().NonMatchSound();

            //Calls a match checker function in 1 second
            Invoke("DoTheyMatch", 1f);
        }

    }


    //Checks if two cards match
    private void DoTheyMatch()
    {
        if(cardOne == cardTwo)
        {
            //Reseting the two cards id
            cardOne = -1;
            cardTwo = -2;
            isPressed = false;
            
            //When two cards are matched
            //we remove them from the grid
            //NOTE: we do not disable the entire obect because since these
            //objects are put onto a grid, it shifts everything over when its SetActive(false)
            CardOne.GetComponent<Image>().enabled = false;
            CardTwo.GetComponent<Image>().enabled = false;
            CardOne.transform.GetChild(0).gameObject.SetActive(false);
            CardTwo.transform.GetChild(0).gameObject.SetActive(false);

            //Set the two GO reference of the 2 cards back to null
            CardOne = null;
            CardTwo = null;

            cardsMatched++; //keeps track of # of total cards matched

            //Checks if you have won(matched all cards)
            if(cardsMatched == PlayerData.numOfCards)
            {
                //Stop timer and store into a static variable from a static script/class
                timer.Stop();
                PlayerData.gameTime = (timer.ElapsedMilliseconds/1000f);
                PlayerData.gameScore = score;
                
                //Load the end game scene
                SceneManager.LoadScene("WinLose");
            }
        }
        else        //If mis matched cards...
        {   
            //re-enables the cards to be clickable (since a fail matched occur)
            CardOne.GetComponent<Button>().enabled = true;
            CardTwo.GetComponent<Button>().enabled = true;
            
            //Sets the front side of the card to unactive(hide the image)
            CardOne.transform.GetChild(0).gameObject.SetActive(false);
            CardTwo.transform.GetChild(0).gameObject.SetActive(false);

            //Reset checking values
            cardOne = -1;
            cardTwo = -2;
            CardOne = null;
            CardTwo = null;
            isPressed = false;

            //Update the score on the screen
            score = score-40;       //Deduct 40 points for mismatch
            scoreText.text = score.ToString();  //Show on current points onto screen

        }

        //This is used to check if the player has lost all points and therefore lost the game
        if(score == 0)
        {   
            //Stop timer and store into a static variable from a static script/class
            timer.Stop();
            PlayerData.gameTime = (timer.ElapsedMilliseconds/1000f);
            PlayerData.gameScore = score;

            SceneManager.LoadScene("WinLose");
        }


        isPressed = true;
    }


}
