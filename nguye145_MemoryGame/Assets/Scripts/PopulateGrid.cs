using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopulateGrid : MonoBehaviour
{
    public GameObject cards;
    public int numOfCards;
    // Start is called before the first frame update
    void Start()
    {
        populate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populate()
    {
        GameObject newObj;

        for(int i = 0; i < numOfCards ; i++)
        {
            newObj = (GameObject)Instantiate(cards, transform);
        }

    }
}
