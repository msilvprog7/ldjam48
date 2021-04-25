using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Quests : MonoBehaviour
{
    private QuestLog questLog;
    private Transform QuestSlotTemplate;
    private Transform QuestHolder;

    public void SetQuestLog(QuestLog questLog)
    {
        this.questLog = questLog;
        RefreshQuests();
    }
    // Start is called before the first frame update
    void Start()
    {
        //originalPosition = transform.localPosition;
        QuestHolder = GameObject.Find("QuestHolder").transform;
        if (QuestHolder != null)
        {
            QuestSlotTemplate = QuestHolder.Find("QuestSlotTemplate");
        }
    }

    private void RefreshQuests()
    {
        foreach (Quest quest in questLog.GetQuestList())
        {
            // Create new Quest Object Panel in UI 
            var qst = Instantiate(QuestSlotTemplate, QuestHolder);
            var textObject = qst.transform.GetChild(2).gameObject;
            textObject.GetComponent<TMPro.TextMeshProUGUI>().text = quest.Title;
            RectTransform questSlotRectTransform = qst.GetComponent<RectTransform>();
            questSlotRectTransform.gameObject.SetActive(true);
        }
    }
}
