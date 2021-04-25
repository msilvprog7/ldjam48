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
        }
        else if(activeQuest.SuccessItems.Contains(item.name))
        {
            // Item is found in list, score appropriately and remove from list
            Debug.Log("Used " + item.name + " and it was succesful!");
        }
        else
        {
            // Item is not found in list, score appropriately and remove from list
            Debug.Log("Used " + item.name + " and it was unsuccesful!");
        }

        // Then prep for next quest by calling initScreen() on QuestScreenHandler object
        qsh.initScreen();
        // Then remove quest from list
        //questLog.remove()

        // Reference Code from DDW
/*

            if (quest.SuccessItems.Contains("")) {
                success = true;
                questDialogue.text = quest.MessageSuccessWithItem;
            } else if (!item.Equals("")) {  // Check if they have an item
                if (Random.Range(0.0f, 1.0f) < successProbBadItem) {
                    success = true;
                    questDialogue.text = quest.MessageSuccessWrongItem;
                } else {
                    questDialogue.text = quest.MessageFailWithItem;
                }
            } else {  // The no item case
                if (Random.Range(0.0f, 1.0f) < successProbNoItem) {
                    success = true;
                    questDialogue.text = quest.MessageSuccessNoItem;
                } else {
                    questDialogue.text = quest.MessageFailNoItem;
                }
            }
            if (success) {
                questTitle.text = "Success!";
                questButtonText.text = "Onward!";
            } else {
                questTitle.text = "Quest Failed.";
                questButtonText.text = "...";
            }
            questComplete = true;
        }
    }*/
    }
}

