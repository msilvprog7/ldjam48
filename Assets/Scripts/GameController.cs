using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class GameController : MonoBehaviour
{
    private Inventory inventory;
    private QuestLog questLog;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private UI_Quests uiQuests;

    // Reading Quest List
    public List<Quest> masterQuestList { get; } = new List<Quest>();
    private static string Filename = "Assets\\Scripts\\Quests.txt";
    private static char Delimiter = '\t';
    private static char ListDelimiter = ',';
    private static int HeaderLines = 2;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        readQuests();
        //Debug.Log(questMaster.Value.Count);
        questLog = new QuestLog(masterQuestList);
        uiQuests.SetQuestLog(questLog);
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

}

