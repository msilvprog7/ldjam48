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
        questDialogue.text = "Lorem Ipsum?";
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
        questButton.gameObject.SetActive(true);
        NoItemButton.gameObject.SetActive(false);
        ItemButton.gameObject.SetActive(false);
        //NoItemButton.enabled = false;
        //ItemButton.enabled=false;
    }

}
