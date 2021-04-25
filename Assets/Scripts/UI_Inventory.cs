using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotTemplate;
    private Transform SlotHolder;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventory();
    }
    // Start is called before the first frame update
    void Start()
    {
        //originalPosition = transform.localPosition;
        SlotHolder = GameObject.Find("SlotHolder").transform;
        if (SlotHolder != null)
        {
            //Debug.Log("SlotHolder Found");
            itemSlotTemplate = SlotHolder.Find("ItemSlotTemplate");
        }
    }

    private void RefreshInventory()
    {
        foreach (Item item in inventory.GetItemList())
        {
            // Create new Item Object Panel in UI 
            var ist = Instantiate(itemSlotTemplate, SlotHolder);
            var textObject = ist.transform.GetChild(2).gameObject;
            textObject.GetComponent<TMPro.TextMeshProUGUI>().text = item.name;
            RectTransform itemSlotRectTransform = ist.GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
