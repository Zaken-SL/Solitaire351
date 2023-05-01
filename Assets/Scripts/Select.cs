//Author: Jacob Slee
//Adapted from https://www.megalomobile.com/lets-make-solitaire-in-unity-part-1-set-up-and-shuffle/



using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Select : MonoBehaviour
{

    public bool faceUp = false;
    public bool top = false;
    public string suit;
    public int value;
    public int row;
    public bool inDeckPile = false;

    private string valueString;
    
    void Start()
    {


        if (CompareTag("Card")) { 
        
            suit = transform.name[0].ToString();

            for (int i = 1; i < transform.name.Length; i++) {

                char c = transform.name[i];
                valueString = valueString + c.ToString();
                
            }
            bool isNumber = int.TryParse(valueString, out int numericValue);

            if (isNumber)
            {

                value = numericValue;
            }


            if (valueString == "A")
            {
                value = 1;
            }
            if (valueString == "J")
            {
                value = 11;
            }
            if (valueString == "Q")
            {
                value = 12;
            }
            if (valueString == "K")
            {
                value = 13;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
