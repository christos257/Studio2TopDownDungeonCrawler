using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollection : Quest
{

    public QuestCollection()
    {
        title = "Collecting";
        description = "Collect 4 items";
        itemsToCollect = GameObject.FindGameObjectsWithTag("Items").Length / 2;
        questCompletion = PlayerController.pickUpAmount.ToString() + " / " + itemsToCollect;
        prevDialogue = "Hey, I need help! Can you collect some things for me?";
        ifQuestNotCompleted = "You come back empty handed? Get back out there and fetch me those items!";
        //QDO = GameObject.Find("QuestDescriptionObject");
        //questDescription = QDO.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public override bool CheckIfQuestCompleted()
    {
        if (PlayerController.pickUpAmount >= itemsToCollect)
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
