using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlotHandler : MonoBehaviour
{
    // Item that UI element was generated from
    private Item item;
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate {onClick(); });
    }

    void onClick()
    {
        // Item use stuff
    }

    public void setItemReference(Item item)
    {
        this.item = item;
    }
}