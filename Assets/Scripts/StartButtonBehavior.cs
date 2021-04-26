using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonBehavior : MonoBehaviour
{
   public GameObject firstQuestScreen;
   public GameObject mainQuestScreen;
   public GameObject InventoryPanel;
   public GameObject QuestLogPanel;

   void OnButtonPress()
   {
      firstQuestScreen.SetActive(false);
      //mainQuestScreen.SetActive(true);
      InventoryPanel.SetActive(true);
      QuestLogPanel.SetActive(true);
      mainQuestScreen.GetComponent<QuestScreenHandler>().initScreen();
   }
}
