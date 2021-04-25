using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestSlotHandler : MonoBehaviour
{
    // Quest that UI element was generated from
    private Quest quest;
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate {onClick(); });
    }

    void onClick()
    {
        //Check if there is already a quest in progress
        if (gameController.questRunning)
        {
            return;
        }
        else
        {
            // Extract Quest object from UI object
            //launchQuest(go);
            Debug.Log(quest.Title + " Launched!");
        }
    }

    public void setQuestReference(Quest quest)
    {
        this.quest = quest;
    }
}
