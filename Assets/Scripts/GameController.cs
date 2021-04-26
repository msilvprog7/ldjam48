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
    public QuestLog questLog;
    public bool questRunning {get; set;} = false; // Let's you know if a quest is active
    public Quest activeQuest;
    public GameObject MainQuestScreen;
    private QuestScreenHandler qsh;

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
    public const int successesToWin = 10;

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
        uI_Inventory = SlotHolder.GetComponent<UI_Inventory>();
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
        }
        else
        {
            // Item is not found in list, score appropriately and remove from list
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
        } else {
            qsh.setQuestTitleText("Quest Failed.");
            qsh.setQuestButtonText("...");
        }

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
            qsh.winScreen();

        }

        // If the player has no items left, they lose
        if (inventory.GetItemList().Count == 0) {
            qsh.loseScreen();
        }
    }
}

