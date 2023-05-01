using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class SolitaireGame : MonoBehaviour
{




    public static string[] suits = new string[] { "H", "D", "S", "C" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public GameObject cardPrefab;

    public Sprite[] cardFaces;
    public GameObject[] bottomPos;
    public GameObject[] topPos;

    public List<string> deck;
    public List<string>[] bottoms;
    public List<string>[] tops;
    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();
    private int deckLocation;
    public GameObject deckButton;
    public List<string> discardPile = new List<string>();
    private int DrawOne;
    private int DrawRemainder;
    public List<string> Drawn = new List<string>();
    public List<string> notDrawn = new List<string>();

    public void SortDeck()
    {
        DrawOne = deck.Count;
        notDrawn.Clear();

        
        for (int i = 0; i < DrawOne; i++)
        {
         
            notDrawn.Add(deck[i]);
          
        }
 
        deckLocation = 0;

    }

    
    public void DealFromDeck()
    {

        

        foreach (Transform child in deckButton.transform)
        {
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);
            }
        }
        

        if (deckLocation < DrawOne)
        {

            Drawn.Clear();
            float xOffset = 100;
            float zOffset = -2;


            string card = notDrawn[deckLocation];
                GameObject newTopCard = Instantiate(cardPrefab, new Vector3(deckButton.transform.position.x + xOffset, deckButton.transform.position.y, deckButton.transform.position.z + zOffset), Quaternion.identity, deckButton.transform);

                newTopCard.name = card;
                newTopCard.GetComponent<Select>().faceUp = true;
                newTopCard.GetComponent<Select>().inDeckPile = true;

            
            deckLocation++;
        
        }
        else
        {
            //Restack the top deck
            RestackTopDeck();
        }

    }




   
    void Start()
    {
        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6 };

        PlayCards();
     
    }

    
    void Update()
    {

    }


    public void PlayCards()
    {

        deck = GenDeck();
        Shuffle(deck);
        SolitaireSortRows();
        SolitaireDeal();
        SortDeck();

    }


    public static List<string> GenDeck()

    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }
        return newDeck;
    }


    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void SolitaireSortRows()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = i; j < 7; j++)
            {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }

        }

    }


    void SolitaireDeal() {


        

        for (int i = 0; i < 7; i++)
        {

            float yOffset = 0;
            float zOffset = 2;
            foreach (string card in bottoms[i])
            {

                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity, bottomPos[i].transform);

                newCard.name = card;
              
                newCard.GetComponent<Select>().row = i;
                if (card == bottoms[i][bottoms[i].Count - 1])
                {
                    newCard.GetComponent<Select>().faceUp = true;
                }
                yOffset = yOffset + 7;
                zOffset = zOffset + 1;
                discardPile.Add(card);   
            }

        }

        foreach (string card in discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }
        }
        discardPile.Clear();


    }



  

    void RestackTopDeck()
    {
        deck.Clear();
        foreach (string card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();
 
        SortDeck();
    }



}
