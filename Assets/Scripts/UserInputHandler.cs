using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Linq;

public class UserInputHandler : MonoBehaviour
{
    private SolitaireGame solitaire;
    public GameObject slot1;
    
    // Start is called before the first frame update
    void Start()
    {
        solitaire = FindObjectOfType<SolitaireGame>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();

    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                // what has been hit? Deck/Card/EmptySlot...
                if (hit.collider.CompareTag("Deck"))
                {
                    //clicked deck
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    // clicked card
                    Card(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Top"))
                {
                    // clicked top
                    Top(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Bottom"))
                {
                    // clicked bottom
                    Bottom(hit.collider.gameObject);
                }
            }
        }
    }
    void Deck()
    {
        // deck click actions
        print("Clicked on deck");
        solitaire.DealFromDeck(); 

        
    }
    void Card(GameObject selected)
    {
        // card click actions
        print("Clicked on Card");

        if (!selected.GetComponent<Select>().faceUp) {

            if (!Blocked(selected))
            {

                selected.GetComponent<Select>().faceUp = true;
                slot1 = this.gameObject;
            }
        }



        if (slot1 == this.gameObject) {

            slot1 = selected;
        }

        else if (slot1 != selected)
        {
            // if the new card is eligable to stack on the old card
            if (Stackable(selected))
            {
                Stack(selected);
            }
            else
            {
                // select the new card
                slot1 = selected;
            }
        }
        else if (selected.GetComponent<Select>().inDeckPile) // if the card clicked on is in the deck pile with the trips
        {
            // if it is not blocked
            if (!Blocked(selected))
            {
          //      if (slot1 == selected) // if the same card is clicked twice
          //      {
          //          if (DoubleClick())
           //         {
                        // attempt auto stack
           //             AutoStack(selected);
           //         }
           //     }
          //      else
          //      {
                    slot1 = selected;
           //     }
            }

        }


    }
    void Top(GameObject selected)
    {
        // top click actions
        print("Clicked on Top");

        if (slot1.CompareTag("Card"))
        {
            // if the card is an ace and the empty slot is top then stack
            if (slot1.GetComponent<Select>().value == 1)
            {
                Stack(selected);
            }

        }



    }
    void Bottom(GameObject selected)
    {
        // bottom click actions
        print("Clicked on Bottom");

        if (slot1.GetComponent<Select>().value == 13)
        {
            Stack(selected);
        }
   

    }

    bool Stackable(GameObject seletected)
    {
        
        Select s1 = slot1.GetComponent<Select>();
        Select s2 = seletected.GetComponent<Select>();
        if (!s2.inDeckPile)
        {
            if (s2.top)
            {
                if (s1.suit == s2.suit || (s1.value == 1 && s2.suit == null))
                {
                    if (s1.value == s2.value + 1)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (s1.value == s2.value - 1)
                {
                    bool card1Red = true;
                    bool card2Red = true;

                    if (s1.suit == "C" || s1.suit == "S")
                    {
                        card1Red = false;
                    }

                    if (s2.suit == "C" || s2.suit == "S")
                    {
                        card2Red = false;
                    }

                    if (card1Red == card2Red)
                    {
                        print("Not stackable");
                        return false;
                    }
                    else
                    {
                        print("Stackable");
                        return true;
                    }
                }

            }
        }
        return false; 

    }

    void Stack(GameObject selected)
    {
        // if on top of king or empty bottom stack the cards in place
        // else stack the cards with a negative y offset

        Select s1 = slot1.GetComponent<Select>();
        Select s2 = selected.GetComponent<Select>();
        float yOffset = 5;

        if (s2.top || (!s2.top && s1.value == 13))
        {
            yOffset = 0;
        }

        slot1.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - yOffset, selected.transform.position.z - 0.01f);
        slot1.transform.parent = selected.transform; // this makes the children move with the parents

        if (s1.inDeckPile) // removes the cards from the top pile to prevent duplicate cards
        {
            solitaire.tripsOnDisplay.Remove(slot1.name);
        }
        else if (s1.top && s2.top && s1.value == 1) // allows movement of cards between top spots
        {
            solitaire.topPos[s1.row].GetComponent<Select>().value = 0;
            solitaire.topPos[s1.row].GetComponent<Select>().suit = null;
        }
        else if (s1.top) // keeps track of the current value of the top decks as a card has been removed
        {
            solitaire.topPos[s1.row].GetComponent<Select>().value = s1.value - 1;
        }
        else // removes the card string from the appropriate bottom list
        {
            solitaire.bottoms[s1.row].Remove(slot1.name);
        }

        s1.inDeckPile = false; // you cannot add cards to the trips pile so this is always fine
        s1.row = s2.row;

        if (s2.top) // moves a card to the top and assigns the top's value and suit
        {
            solitaire.topPos[s1.row].GetComponent<Select>().value = s1.value;
            solitaire.topPos[s1.row].GetComponent<Select>().suit = s1.suit;
            s1.top = true;
        }
        else
        {
            s1.top = false;
        }

        // after completing move reset slot1 to be essentially null as being null will break the logic
        slot1 = this.gameObject;

    }

    bool Blocked(GameObject selected)
    {
        Select s2 = selected.GetComponent<Select>();
        if (s2.inDeckPile == true)
        {
            if (s2.name == solitaire.tripsOnDisplay.Last()) // if it is the last trip it is not blocked
            {
                return false;
            }
            else
            {
                print(s2.name + " is blocked by " + solitaire.tripsOnDisplay.Last());
                return true;
            }
        }
        else
        {
            if (s2.name == solitaire.bottoms[s2.row].Last()) // check if it is the bottom card
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }



}
