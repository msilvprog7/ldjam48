using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScreenHandler : MonoBehaviour
{
    public GameController gameController;
    private Quest activeQuest;
    public Text questTitle;  // The title above the main dialogue quest
    public Text questDialogue;  // The main text in the center of the screen. Quest description and success/failure message.
    public Button questButton;
    public Text questButtonText;
    public Button NoItemButton;
    public Button ItemButton;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    public void updateQuest()
    {
        activeQuest = gameController.activeQuest;
        //Debug.Log(activeQuest.Title + " Launched!");
        questTitle.text = activeQuest.Title;
        questDialogue.text = activeQuest.Description;
        questButtonText.GetComponent<Text>().text = "Start Quest";
        questButton.gameObject.SetActive(true);
        questButton.enabled = true;
        questButton.interactable = true;
    }
    public void startQuest()
    {
        gameController.questRunning = true;
        questDialogue.text = "";
        questButton.gameObject.SetActive(false);
        questButton.enabled = false;
        questButton.interactable = false;
        NoItemButton.gameObject.SetActive(true);
        ItemButton.gameObject.SetActive(true);
    }
    public void initScreen()
    {
        gameObject.SetActive(true);
        //Debug.Log("Made It");
        questButton.gameObject.SetActive(false);
        NoItemButton.gameObject.SetActive(false);
        ItemButton.gameObject.SetActive(false);
        //NoItemButton.enabled = false;
        //ItemButton.enabled=false;
    }
    
    public void winScreen()
    {
        questButton.gameObject.SetActive(false);
        NoItemButton.gameObject.SetActive(false);
        ItemButton.gameObject.SetActive(false);
        questTitle.text = "Congratulations, hero.";
        questDialogue.text = "Your seemingly endless task of helping the people of this village has become your new way of life. They welcomed you with open arms, and meet your every action with gratitude. \n\nBut...wasn't there something else you had to do? Maybe it will come back to you...";
        questButtonText.GetComponent<Text>().text = "";
    }
    
    public void loseScreen()
    {
        questButton.gameObject.SetActive(false);
        NoItemButton.gameObject.SetActive(false);
        ItemButton.gameObject.SetActive(false);
        questTitle.text = "Well...";
        questDialogue.text = "Your seemingly endless list of tasks has become too much for you. Without a shirt on your back or a coin to your name, you are now endebted to the people of this village, and fully reliant on their giving kindness.\n\nOh, and you didn't save the Royal Family either.";
        questButtonText.GetComponent<Text>().text = "";
    }

    public void noItemInstructions() {
        questDialogue.text = "";
    }

    public void itemInstructions() {
        questDialogue.text = "Please select an item";
    }

    public void setQuestDialogue(string dlg) {
        questDialogue.text = dlg;
    }

    public void setQuestButtonText(string txt) {
        questButtonText.GetComponent<Text>().text = txt;
        questButton.gameObject.SetActive(true);
    }

    public void setQuestTitleText(string txt) {
        questTitle.text = txt;
    }

}
