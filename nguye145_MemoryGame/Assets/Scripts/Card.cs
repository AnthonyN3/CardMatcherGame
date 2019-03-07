using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int id;

    public void OnClick()
    {
        Debug.Log("WE");
        if(gameObject.transform.parent.GetComponent<PopulateGrid>().isPressed)
        {
            Debug.Log("OKAY");
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.parent.GetComponent<PopulateGrid>().TEST(id,gameObject);
            gameObject.GetComponent<Button>().enabled = false;
        }
    }

}
