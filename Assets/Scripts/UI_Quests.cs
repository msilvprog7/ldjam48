using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Quests : MonoBehaviour
{
    // Should generate initial elements on start and instantiate new gameObjects whenever a new Quest object is added
    private GameController gameController;
    private Transform QuestSlotTemplate;


    // Start is called before the first frame update
    void Start()
    {
        // Get Reference to GameController Object
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        QuestSlotTemplate = gameObject.transform.Find("QuestSlotTemplate");
        

        RefreshQuests();
    }

    // Updates QuestLog with Current QuestList
    private void RefreshQuests()
    {
        foreach (Quest quest in gameController.questLog.GetQuestList())
        {
            // Create new Quest Object Panel in UI 
            var qst = Instantiate(QuestSlotTemplate, gameObject.transform);
            qst.GetComponent<QuestSlotHandler>().setQuestReference(quest);
            var textObject = qst.transform.GetChild(2).gameObject;
            textObject.GetComponent<TMPro.TextMeshProUGUI>().text = quest.Title;
            RectTransform questSlotRectTransform = qst.GetComponent<RectTransform>();
            questSlotRectTransform.gameObject.SetActive(true);
        }
    }
}
