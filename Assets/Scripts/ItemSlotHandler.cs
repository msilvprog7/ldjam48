using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlotHandler : MonoBehaviour
{
    // Item that UI element was generated from
    private Item item;
    private GameController gameController;
    private bool buttonState;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate {onClick(); });
        toggleButtonState(false);
    }

    void onClick()
    {
        // Item use stuff
        if(buttonState)
        {
            // pass item to gameController 
            gameController.completeQuest(item);
        }
    }

    public void setItemReference(Item item)
    {
        this.item = item;
    }
    public void toggleButtonState(bool state)
    {
        buttonState = state;
        //gameObject.GetComponent<Button>().interactable = state;
    }
}