using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog
{
    private List<Quest> questList;

    public QuestLog(List<Quest> questMasterList)
    {
        questList = new List<Quest>();
        // Add first quest from Quests object
        Debug.Log(questMasterList.Count);
        AddQuest(questMasterList[0]);
        AddQuest(questMasterList[1]);

    }

    public void AddQuest(Quest quest)
    {
        questList.Add(quest);
    }

    public List<Quest> GetQuestList()
    {
        return questList;
    }
}
