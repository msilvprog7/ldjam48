using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonBehavior : MonoBehaviour
{
   public GameObject firstQuestScreen;

   public void OnButtonPress(){
      firstQuestScreen.SetActive(false);
   }
}
