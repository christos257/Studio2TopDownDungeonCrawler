using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestKilling : Quest
{

    public QuestKilling()
    {
        title = "Fragout";
        description = "Kill 10 enemies";
        enemiesToKill = GameObject.FindGameObjectsWithTag("Enemy").Length / 2;
        questCompletion = PlayerController.enemiesKilled.ToString() + " / " + enemiesToKill;
        prevDialogue = "Thank you, oh I also need to get through to other rooms, can you clear the way for me?";
        ifQuestNotCompleted = "We can't leave here with enemies still alive! Kill! Kill! Kill!";
        //QDO = GameObject.Find("QuestDescriptionObject");
        //questDescription = QDO.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    

    public override bool CheckIfQuestCompleted()
    {
        if (PlayerController.enemiesKilled >= enemiesToKill)
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
