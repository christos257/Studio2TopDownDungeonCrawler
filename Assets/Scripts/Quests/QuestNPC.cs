using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNPC : MonoBehaviour
{
    public List<Quest> quests;


    private TMPro.TextMeshProUGUI questDescriptionText;
    private TMPro.TextMeshProUGUI questTitleText;
    private TMPro.TextMeshProUGUI[] currTask;

    private GameObject wholeChat;
    private GameObject activeQuestUI;
    private GameObject questDescriptionObject;
    private GameObject questTitleObject;
    private GameObject canvas;

    private Text pressButton;
    
    public int currentQuest = 0;
    private string npcName = "Ron";


    private void Start()
    {
        canvas = GameObject.Find("QuestCanvas");
        wholeChat = GameObject.Find("QuestDiaFrame");
        activeQuestUI = GameObject.Find("ActiveQuest");
        questTitleObject = GameObject.Find("QuestTitleObject");
        questDescriptionObject = GameObject.Find("QuestDescriptionObject");
       
        questDescriptionText = questDescriptionObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        questTitleText = questTitleObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        currTask = activeQuestUI.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        pressButton = canvas.GetComponentInChildren<Text>();

        wholeChat.SetActive(false);
        activeQuestUI.SetActive(false);
        pressButton.enabled = false;

        quests = new List<Quest>();
        quests.Add(new QuestCollection());
        quests.Add(new QuestKilling());
        quests.Add(new QuestBoss());
        
    }
    private void Update()
    {
        if (quests[currentQuest].isActive)
        {
            currTask[1].text = quests[currentQuest].description + " " + quests[currentQuest].questCompletion;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pressButton.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (quests[currentQuest].CheckIfQuestCompleted() && activeQuestUI.activeSelf == true)
                {
                    activeQuestUI.SetActive(false);
                    if (currentQuest < quests.Count)
                    {
                        currentQuest++;
                    }
                    else
                    {
                        currentQuest = quests.Count;
                    }
                }
                wholeChat.SetActive(true);
                
                questTitleText.text = npcName;
                questDescriptionText.text = quests[currentQuest].prevDialogue;

                if (quests[currentQuest].PreviousTextWasActive(questDescriptionText.text) && !quests[currentQuest].CheckIfQuestCompleted() && activeQuestUI.activeSelf == true)
                {
                    questDescriptionText.text = quests[currentQuest].ifQuestNotCompleted;
                }
                //questTitleText.text = quests[currentQuest].title;
                //questDescriptionText.text = quests[currentQuest].description;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                quests[currentQuest].isActive = true;
                activeQuestUI.SetActive(true);
                questTitleText.text = quests[currentQuest].title;
                questDescriptionText.text = quests[currentQuest].description;
                currTask[1].text = quests[currentQuest].description + " " + quests[currentQuest].questCompletion;
                wholeChat.SetActive(false);
            }

            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pressButton.enabled = false;
        }
    }

   
    
}
