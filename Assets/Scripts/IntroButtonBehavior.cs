using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroButtonBehavior : MonoBehaviour {
   public GameObject introScreen;
   public GameObject firstQuestScreen;

   public void OnButtonPress(){
      introScreen.SetActive(false);
      firstQuestScreen.SetActive(true);
   }

}