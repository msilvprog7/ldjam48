using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Inventory inventory;
    public GameObject SlotHolder; //Has UI_Inventory Script
    public UI_Inventory uI_Inventory;
     public GameObject QuestHolder; //Has UI_Inventory Script
    public UI_Quests uI_Quests;
    public QuestLog questLog;
    public bool questRunning {get; set;} = false; // Let's you know if a quest is active
    public Quest activeQuest;
    public GameObject MainQuestScreen;
    private QuestScreenHandler qsh;
    
    public GameObject SoundManager;
    public Sounds snd;

    public GameObject InventoryPanel;
    public GameObject QuestLogPanel;

    // Reading Quest List
    public List<Quest> masterQuestList { get; } = new List<Quest>();
    private static string Filename = "Assets\\Scripts\\Quests.txt";
    private static char Delimiter = '\t';
    private static char ListDelimiter = ',';
    private static int HeaderLines = 2;

    //
    public float successProbWrongItem = 0.5f;
    public float successProbNoItem = 0.25f;
    private bool success;
    private int successCount = 0;
    public int successesToWin = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Generate Inventory Object
        inventory = new Inventory();

        // Read in All Quests from CSV
        readQuests();

        // Generate QuestLog Object
        questLog = new QuestLog(masterQuestList);
        qsh = MainQuestScreen.GetComponent<QuestScreenHandler>();
        snd = SoundManager.GetComponent<Sounds>();
        uI_Inventory = SlotHolder.GetComponent<UI_Inventory>();
        uI_Quests = QuestHolder.GetComponent<UI_Quests>();
    }

    void readQuests()
    {
        masterQuestList.Clear();
        foreach (var line in File.ReadAllLines(Filename).Skip(HeaderLines))
        {
            var row = line.Split(Delimiter);
            var quest = new Quest(row, ListDelimiter);
            masterQuestList.Add(quest);
        }
    }

    public void SetActiveQuest(Quest quest)
    {
        activeQuest = quest;
        qsh.updateQuest();
    }

    public void completeQuest(Item? item)
    {
        questRunning = false;
        // Check item conditions
        if(item == null)
        {
            // No Item Used
            Debug.Log("No Item Used");
        }
        else if(activeQuest.SuccessItems.Contains(item.name))
        {
            // Item is found in list, score appropriately and remove from list
            Debug.Log("Used " + item.name + " and it was succesful!");
            // Remove Item
            //Debug.Log(inventory.GetItemList().Count);
            var it = inventory.GetItemList().Find(it => it.name == item.name);
            inventory.GetItemList().Remove(it);
            uI_Inventory.RemoveElement(it);
            uI_Inventory.RefreshInventory();
            //Debug.Log(inventory.GetItemList().Count);
        }
        else
        {
            // Item is not found in list, score appropriately and remove from list
            Debug.Log("Used " + item.name + " and it was unsuccesful!");
            // Remove Item
            //Debug.Log(inventory.GetItemList().Count);
            var it = inventory.GetItemList().Find(it => it.name == item.name);
            inventory.GetItemList().Remove(it);
            uI_Inventory.RemoveElement(it);
            //Debug.Log(inventory.GetItemList().Count);
            uI_Inventory.RefreshInventory();
        }
        // Update UI
        questLog.GetQuestList().Remove(activeQuest);
        uI_Quests.RemoveElement(activeQuest);
        uI_Quests.RefreshQuests();
        // Then prep for next quest by calling initScreen() on QuestScreenHandler object
        qsh.initScreen();
        // Then remove quest from list
        //questLog.remove()
        
        uI_Inventory.disableSelection();
        Debug.Log("Score: " + successCount);
        handleEndGame();
    }

    private void handleEndGame() {
        // If the player has succeeded in a certain number of quests, they win
        if (successCount >= successesToWin) {
            snd.StartLoop("Success");
            qsh.winScreen();
            InventoryPanel.SetActive(false);
            QuestLogPanel.SetActive(false);
        }

        // If the player has no items left, they lose
        if (inventory.GetItemList().Count == 0) {
            snd.StartLoop("Fail");
            qsh.loseScreen();
            InventoryPanel.SetActive(false);
            QuestLogPanel.SetActive(false);
        }
    }
}

