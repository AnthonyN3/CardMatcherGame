using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject cards;
    [SerializeField]
    private Sprite[] Images;
    private int[] idNumbers = {0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9};
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

        for(int i = 0; i < PlayerData.numOfCards*2 ; i++)
        {
            newObj = (GameObject)Instantiate(cards, transform);
            //newObj.GetComponent<SpriteRenderer>().sprite = Images[i];
            //newObj.transform.GetChild(0).image.sprite = Images[1];
            newObj.GetComponent<Card>().id = idNumbers[i];
            newObj.transform.GetChild(0).GetComponent<Image>().sprite = Images[newObj.GetComponent<Card>().id];
        }
            
    }

    public void TEST(int clickCardsId, GameObject thisCard)
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
            
            Invoke("DoTheyMatch", 1.5f);
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
