using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestSlotHandler : MonoBehaviour
{
    // Quest that UI element was generated from
    private Quest quest;
    private GameController gameController;
    public GameObject MainQuestScreen;
    bool success;  // Succeed or fail the quest
    bool questComplete;

    public float successProbBadItem = 0.5f;
    public float successProbNoItem = 0.25f;

    private string item = "";
    // -----

    void Start()
    {
        questComplete = false;
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
            gameController.SetActiveQuest(quest);
            //gameController.questRunning = true;
        }
    }

    // Called by UI_Quests to set quest field 
    public void setQuestReference(Quest quest)
    {
        this.quest = quest;
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }
}
