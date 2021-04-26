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
    public List<Quest> masterQuestList { get; set;} = new List<Quest>();
    public List<Item> masterItemList = new List<Item>();
    private static string Filename = "Assets\\Scripts\\Quests.txt";
    private static char Delimiter = '\t';
    private static char ListDelimiter = ',';
    private static int HeaderLines = 2;
    private List<string> tempItemList;

    //
    public float successProbWrongItem = 0.35f;
    public float successProbNoItem = 0.20f;
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
        tempItemList = new List<string>(){"Hoe", "Shovel", "Garden", "Fork", "Trowel", "Seeds", "Basket", "Shears", "Bucket", "Watering Can", "Pot", "Pan", "Steak", "Chicken", "Paintbrush", "Worms", "Fishing Rod", "Net", "Shirt", "Hat", "Pants", "Hammer", "Saw", "Shoes", "Corn"};
        foreach(string item in tempItemList)
        {
            Item tempItem = new Item(item);
            masterItemList.Add(tempItem);
        }
        
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
            if (Random.Range(0.0f, 1.0f) < successProbNoItem) {
                success = true;
                qsh.setQuestDialogue(activeQuest.MessageSuccessNoItem);
                success = true;
            } else {
                qsh.setQuestDialogue(activeQuest.MessageFailNoItem);
                success = false;
            }
        }
        else if(activeQuest.SuccessItems.Contains(item.name))
        {
            // Item is found in list, score appropriately and remove from list
            Debug.Log("Used " + item.name + " and it was succesful!");
            qsh.setQuestDialogue(activeQuest.MessageSuccessWithItem.Replace("[item]", item.name));
            success = true;
            
            // Remove Item
            var it = inventory.GetItemList().Find(it => it.name == item.name);
            inventory.GetItemList().Remove(it);
        }
        else
        {
            // Item is not found in list, score appropriately and remove from list
            Debug.Log("Used " + item.name + " and it was unsuccesful!");
            // Remove Item
            var it = inventory.GetItemList().Find(it => it.name == item.name);
            inventory.GetItemList().Remove(it);

            Debug.Log("Used " + item.name);
            if (Random.Range(0.0f, 1.0f) < successProbWrongItem) {
                qsh.setQuestDialogue(activeQuest.MessageSuccessWrongItem.Replace("[item]", item.name));
                success = true;
            } else {
                qsh.setQuestDialogue(activeQuest.MessageFailWithItem.Replace("[item]", item.name));
                success = false;
            }
        }

        if (success) {
            qsh.setQuestTitleText("Success!");
            qsh.setQuestButtonText("Onward!");
            successCount += 1;
            //Call UI_Quests.AddRandomItem
            Item randItem = masterItemList[Random.Range(0, masterItemList.Count)];
            Debug.Log(randItem.name);
            inventory.AddItem(randItem);
            uI_Inventory.RemoveElement(null);
            uI_Inventory.RefreshInventory();
        } else {
            qsh.setQuestTitleText("Quest Failed.");
            qsh.setQuestButtonText("...");
        }
        // Update UI
        questLog.GetQuestList().Remove(activeQuest);
        questLog.AddQuest(masterQuestList[Random.Range(0, masterQuestList.Count)]);
        questLog.AddQuest(masterQuestList[Random.Range(0, masterQuestList.Count)]);
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

