using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private GameController gameController;
    private Transform ItemSlotTemplate;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        ItemSlotTemplate = gameObject.transform.Find("ItemSlotTemplate");

        RefreshInventory();
    }

    public void RemoveElement(Item it)
    {
        //Find ItemSlotTemplate gameObject that has item field equal to it
        ItemSlotHandler[] allChildren = gameObject.GetComponentsInChildren<ItemSlotHandler>();
        foreach(ItemSlotHandler ish in allChildren)
        {
            ish.destroyObject();
        }
    }
    
    public void RefreshInventory()
    {
        foreach (Item item in gameController.inventory.GetItemList())
        {
            // Create new Item Object Panel in UI 
            var ist = Instantiate(ItemSlotTemplate, gameObject.transform);
            ist.GetComponent<ItemSlotHandler>().setItemReference(item);
            var textObject = ist.transform.GetChild(2).gameObject;
            textObject.GetComponent<TMPro.TextMeshProUGUI>().text = item.name;
            RectTransform itemSlotRectTransform = ist.GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
        }
    }

    public void enableSelection()
    {
        ItemSlotHandler[] allChildren = gameObject.GetComponentsInChildren<ItemSlotHandler>();
        // Loop through ItemSlotTemplate Children and enable
        foreach(ItemSlotHandler ish in allChildren)
        {
            ish.toggleButtonState(true);
        }
    }
    
    public void disableSelection()
    {
        ItemSlotHandler[] allChildren = gameObject.GetComponentsInChildren<ItemSlotHandler>();
        // Loop through ItemSlotTemplate Children and enable
        foreach(ItemSlotHandler ish in allChildren)
        {
            ish.toggleButtonState(false);
        }
    }
}
