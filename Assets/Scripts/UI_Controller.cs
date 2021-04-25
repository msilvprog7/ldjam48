using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Controller : MonoBehaviour
{
    // References to UI gameObjects
    public UI_Inventory uI_Inventory;
    public UI_Quests uI_Quests;
    private GameObject gameController;
	
    // Start is called before the first frame update
    void Start()
    {
        // Get References
        gameController = GameObject.FindWithTag("GameController");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
