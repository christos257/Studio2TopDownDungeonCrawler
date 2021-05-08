using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class PlayerEntry
{
    public int roomComps;
    public int enemiesKilled;
    public string name;
}

public class LeaderboardTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemp;
    private Transform leaderboard;
    private Transform playerscore;
    private List<Transform> leaderboardEntryTransformList;
    [SerializeField] Button saveButton;
    private Transform entryTransform;

    [SerializeField] TMPro.TMP_InputField nameInputField;
    [SerializeField] TMPro.TextMeshProUGUI kills;
    [SerializeField] TMPro.TextMeshProUGUI roomsComp;

    private int maxLetters = 3;
    
    

    PlayerEntry playerStats = new PlayerEntry();
    PlayerEntry nonBinaryStats;


    private void Awake()
    {
        leaderboard = transform.Find("LeaderBoard");
        entryContainer = leaderboard.Find("LeaderboardEntryContainer");
        entryTemp = entryContainer.Find("LeaderboardEntryTemp");
        playerscore = transform.Find("PlayerEntry");

        entryTemp.gameObject.SetActive(false);

        //AddPlayerEntry(25, 12, "CAM");


        string jsonString = PlayerPrefs.GetString("leaderboardTable");
        Highscores highscore = JsonUtility.FromJson<Highscores>(jsonString);

        /*int maxScores = 10;

        if (highscore.highscoreEntryList.Count >= maxScores)
        {
            highscore.highscoreEntryList.RemoveAt(maxScores);
        }*/
        leaderboardEntryTransformList = new List<Transform>();
        foreach (PlayerEntry playerEntry in highscore.highscoreEntryList)
        {
            CreateNewPlayerEntry(playerEntry, entryContainer, leaderboardEntryTransformList);
        }
    }

    private void Update()
    {
        playerStats.roomComps = PlayerController.roomsCompleted;
        playerStats.enemiesKilled = PlayerController.enemiesKilled;
        nameInputField.characterLimit = 3;

        nameInputField.text.Replace(@"[^ a - zA - Z0 - 9]", "");
        playerStats.name = nameInputField.text.ToUpper();

    }
    public void CreateNewPlayerEntry(PlayerEntry entry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f;
        entryTransform = Instantiate(entryTemp, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }
        entryTransform.Find("PosText").GetComponent<TMPro.TextMeshProUGUI>().text = rankString;

        int roomsCompleted = entry.roomComps;
        entryTransform.Find("RoomCompletedText ").GetComponent<TMPro.TextMeshProUGUI>().text = roomsCompleted.ToString() + "/20";

        int enemiesKilled = entry.enemiesKilled;
        entryTransform.Find("EnemiesKilledText").GetComponent<TMPro.TextMeshProUGUI>().text = enemiesKilled.ToString();

        string name = entry.name;
        entryTransform.Find("NameText ").GetComponent<TMPro.TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
        PlayerScore();
    }

    private void PlayerScore()
    {
        Transform playerScore = Instantiate(playerscore);
        int roomsCompleted = PlayerController.roomsCompleted;
        playerscore.Find("RCTextFromComp").GetComponent<TMPro.TextMeshProUGUI>().text = roomsCompleted.ToString() + "/20";

        int enemiesKilled = PlayerController.enemiesKilled;
        playerscore.Find("KillsTextFromComp").GetComponent<TMPro.TextMeshProUGUI>().text = enemiesKilled.ToString();
    }

    private void AddPlayerEntry(int roomscomp, int kills, string name)
    {
        PlayerEntry playerEntry = new PlayerEntry { roomComps = roomscomp, name = name };

        string jsonString = PlayerPrefs.GetString("leaderboardTable");
        Highscores highscore = JsonUtility.FromJson<Highscores>(jsonString);

        highscore.highscoreEntryList.Add(playerEntry);
        int maxScore = 11;
        int remove = highscore.highscoreEntryList.Count - 1;
        //sorting entry list by rooms completed 
        for (int i = 0; i < highscore.highscoreEntryList.Count; i++)
        {
            
            if (nonBinaryStats.roomComps > highscore.highscoreEntryList[i].roomComps)
            {
                PlayerEntry temp = highscore.highscoreEntryList[i];
                highscore.highscoreEntryList[i] = nonBinaryStats;
                nonBinaryStats = temp;
            }
        }   

        if (highscore.highscoreEntryList.Count >= maxScore)
        {
            highscore.highscoreEntryList.RemoveAt(remove);
        }
        string json = JsonUtility.ToJson(highscore);
        PlayerPrefs.SetString("leaderboardTable", json);
        PlayerPrefs.Save();

        
    }

    public void Save()
    {
        using (FileStream fileStream = new FileStream(Application.dataPath + "/saveData.dat", FileMode.Create))
        {
            BinaryFormatter binary = new BinaryFormatter();
            binary.Serialize(fileStream, playerStats);
            fileStream.Close();
        }
        saveButton.interactable = false;
    }

    public void Load()
    {
        using (FileStream fileStream = new FileStream(Application.dataPath + "/saveData.dat", FileMode.Open))
        {
            BinaryFormatter binary = new BinaryFormatter();
            nonBinaryStats = (PlayerEntry)binary.Deserialize(fileStream);
            fileStream.Close();
            //nameInputField.text = nonBinaryStats.name.ToString();
            //kills.text = nonBinaryStats.enemiesKilled.ToString();
            //roomsComp.text = nonBinaryStats.roomComps.ToString() + "/20";
            entryTransform.Find("RoomCompletedText ").GetComponent<TMPro.TextMeshProUGUI>().text = nonBinaryStats.roomComps.ToString() + "/20";
            entryTransform.Find("EnemiesKilledText").GetComponent<TMPro.TextMeshProUGUI>().text = nonBinaryStats.enemiesKilled.ToString();
            entryTransform.Find("NameText ").GetComponent<TMPro.TextMeshProUGUI>().text = nonBinaryStats.name.ToString().ToUpper();
            

            AddPlayerEntry(nonBinaryStats.roomComps, nonBinaryStats.enemiesKilled, nonBinaryStats.name);
            
            Debug.Log(nameInputField.text);
        }
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(0);
        PlayerController.enemiesKilled = 0;
        PlayerController.pickUpAmount = 0;
        PlayerController.roomsCompleted = 0;
        PlayerController.speed = GameController.MoveSpeed;
        GameController.BossHealth = GameController.BossMaxHealth;
        GameController.Health = GameController.MaxHealth;
    }

    /*private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
        {
            e.Handled = true;
        }
    }*/


    private class Highscores
    {
        public List<PlayerEntry> highscoreEntryList;
    }
    
}


