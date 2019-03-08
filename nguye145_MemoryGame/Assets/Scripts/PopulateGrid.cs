using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject cards;
    public GameObject AudioManager;

    [SerializeField]
    private Sprite[] Images;
    //private int[] idNumbers = {0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9};
    private int[] idNumbers = new int[PlayerData.numOfCards*2];
    private bool[] idTaken = new bool[20];  //Used to check if id has been set (NOTE: default value is false..)
    private int pressed = 0;
    private int cardOne = -1, cardTwo = -2;
    private GameObject CardOne, CardTwo;
    public bool isPressed = true;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = true;
        populate();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void populate()
    {
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

        for(int i = 0; i < PlayerData.numOfCards*2 ; i++)
        {
            newObj = (GameObject)Instantiate(cards, transform);
            newObj.GetComponent<Card>().id = idNumbers[i];  

            newObj.transform.GetChild(0).GetComponent<Image>().sprite = Images[newObj.GetComponent<Card>().id];
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
        
        pressed++;

        if(pressed == 1)
        {
            cardOne = clickCardsId;
            CardOne = thisCard;


            return;
        }
        else if (pressed == 2)
        {
            cardTwo = clickCardsId;
            CardTwo = thisCard;
        }

        if(isPressed)
        {     
            pressed = 0; 
            isPressed = false;    
            
            if(cardOne == cardTwo)
                AudioManager.GetComponent<MusicManager>().MatchSound();
            else
                AudioManager.GetComponent<MusicManager>().NonMatchSound();

            Invoke("DoTheyMatch", 1f);
        }

    }

    private void DoTheyMatch()
    {
        if(cardOne == cardTwo)
        {
            cardOne = -1;
            cardTwo = -2;
            isPressed = false;
            CardOne = null;
            CardTwo = null;
        }
        else
        {
            CardOne.GetComponent<Button>().enabled = true;
            CardTwo.GetComponent<Button>().enabled = true;
            

            CardOne.transform.GetChild(0).gameObject.SetActive(false);
            CardTwo.transform.GetChild(0).gameObject.SetActive(false);

            cardOne = -1;
            cardTwo = -2;
            CardOne = null;
            CardTwo = null;
            isPressed = false;
        }

        isPressed = true;
    }


}
