using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestChoiceButtonHandler : MonoBehaviour
{
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate {onClick(); });
    }

    void onClick()
    {
        if(gameObject.name == "NoItemButton")
        {
            // Player doesn't wish to use an item
            gameController.completeQuest(null);
        }
        else
        {
            // Player wishes to use an item
            Debug.Log("Please Select an Item");
            // Call UI_Inventory method to enable all buttons 
            gameController.uI_Inventory.enableSelection();
        }
    }
}
