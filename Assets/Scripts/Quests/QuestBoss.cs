using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoss : Quest
{

    public QuestBoss()
    {
        title = "Final";
        description = "Kill the Boss";
        questCompletion = "";
        prevDialogue = "Thank you, I'd be greatful if you could take me out of the dungeon with you, can you kill the warden?";
        ifQuestNotCompleted = "It's your funeral, not mine. Go kill the boss so we can escape";
        //QDO = GameObject.Find("QuestDescriptionObject");
        //questDescription = QDO.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    GameObject boss;

    public override bool CheckIfQuestCompleted()
    {
        boss = GameObject.Find("Boss");
        if (boss == null)
        {
            isCompleted = true;
        }
        return isCompleted;
    }

    public override bool PreviousTextWasActive(string dialogue)
    {
        if (dialogue == prevDialogue)
        {
            prevActive = true;
        }
        return prevActive;
    }
}
