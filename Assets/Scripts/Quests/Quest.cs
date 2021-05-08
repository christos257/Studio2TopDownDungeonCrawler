using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Quest 
{
    
    public bool isActive;
    public bool isCompleted;
    public bool prevActive;

    public string title;
    public string description;
    public string questCompletion;
    public string prevDialogue;
    public string ifQuestNotCompleted;

    public int itemsToCollect;
    public int enemiesToKill;
        
    public GameObject QDO;
    public TMPro.TextMeshProUGUI questDescription;

    public abstract bool CheckIfQuestCompleted();

    public abstract bool PreviousTextWasActive(string dialogue);
}
