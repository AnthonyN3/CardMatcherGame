﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoText : MonoBehaviour
{
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
    }

    public IEnumerator ShowText()
    {
        currentText = "";
        for(int i = 0 ; i<= fullText.Length; i++)
        {
            currentText = fullText.Substring(0,i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
