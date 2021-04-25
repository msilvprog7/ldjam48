using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButtonHandler : MonoBehaviour
{
    private GameController gameController;
    public GameObject MainQuestScreen;
    private QuestScreenHandler qsh;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        qsh = MainQuestScreen.GetComponent<QuestScreenHandler>();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate {onClick(); });
    }

    void onClick()
    {
        // Start Quest
        qsh.startQuest();
    }
}
