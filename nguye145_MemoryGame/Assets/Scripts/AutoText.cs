using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This is used for creating an auto text animation
public class AutoText : MonoBehaviour
{
    public float delay = 0.1f;  //delay time on the animation/typing
    public string fullText;     //the full text to write out
    private string currentText = "";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
    }

    public IEnumerator ShowText()
    {   
        currentText = "";

        //Loops character by character
        for(int i = 0 ; i<= fullText.Length; i++)
        {   
            //Add to the string character by character (creating an illusion as if yu are typing out a string)
            currentText = fullText.Substring(0,i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
