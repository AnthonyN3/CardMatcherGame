using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{   
    //Every Card will be assigned a matching paired id..
    public int id;

    public void OnClick()
    {
        if(gameObject.transform.parent.GetComponent<PopulateGrid>().isPressed)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.parent.GetComponent<PopulateGrid>().ClickCard(id,gameObject);
            gameObject.GetComponent<Button>().enabled = false;
        }
    }

}
