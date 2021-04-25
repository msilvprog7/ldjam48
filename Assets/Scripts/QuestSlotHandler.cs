using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestSlotHandler : MonoBehaviour
{
    // Quest that UI element was generated from
    private Quest quest;
    private GameController gameController;

    // -----
    public Text questTitle;  // The title above the main dialogue quest
    public Text questDialogue;  // The main text in the center of the screen. Quest description and success/failure message.
    public Button questButton;
    public Text questButtonText;

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
            // Extract Quest object from UI object
            //launchQuest(go);
            Debug.Log(quest.Title + " Launched!");

            Debug.Log("Quest Selected.");
            Debug.Log(quest.Title);
            questTitle.text = quest.Title;
            questDialogue.text = quest.Description;
            questButtonText.text = "Start Quest";
            questButton.gameObject.SetActive(true);
            questButton.enabled = true;
            questButton.interactable = true;
        }
    }

    public void setQuestReference(Quest quest)
    {
        this.quest = quest;
    }

    public void QuestButtonClicked() {
        //gameController.questRunning = true;  This is read only(?)
        Debug.Log("Starting quest.");
        if (questComplete) {
            // Reset everything, prep for the next quest
            questTitle.text = "";
            questDialogue.text = "";
            questButtonText.text = "";
            questButton.gameObject.SetActive(false);
            questButton.enabled = false;
            questComplete = false;
            //gameController.questRunning = false;  This is read only(?)    
        } else {
            success = false;
            Debug.Log(quest.Title);
            Debug.Log(quest.SuccessItems);
            Debug.Log(quest);

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
    }
}
