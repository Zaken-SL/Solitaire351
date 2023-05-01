using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private Select select;
    private SolitaireGame game;
    private UserInputHandler userInput;


    void Start() 
    {
        List<string> deck = SolitaireGame.GenDeck();
        game = FindObjectOfType<SolitaireGame>();
       userInput = FindObjectOfType<UserInputHandler>();


        int i = 0;

            foreach (string card in deck) { 
        if (this.name == card)
            {

                cardFace = game.cardFaces[i];
                break;
            }
        i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        select = GetComponent<Select>();

    }

    void Update() 
    {
        if (select.faceUp == true)
       {
            spriteRenderer.sprite = cardFace;
        }
        else {
            spriteRenderer.sprite = cardBack;
        }

        if (userInput.slot1)
        {
                
            if (name == userInput.slot1.name)
            {
                spriteRenderer.color = Color.green;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
    
}
