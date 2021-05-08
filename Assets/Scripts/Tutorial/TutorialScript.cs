using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    public GameObject[] popUps;
    public int popUpIndex = 0;
    public GameObject enemy;
    public GameObject potion;
    public GameObject boot;
    public GameObject door;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                popUpIndex++;
                enemy.SetActive(true);
            }
        }
        else if (popUpIndex == 3)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                popUpIndex++;
                boot.SetActive(true);
                potion.SetActive(true);
            }
        }
        else if (popUpIndex == 4)
        {
            if (GameObject.FindGameObjectsWithTag("Items").Length == 0) 
            {
                popUpIndex++;
                door.SetActive(true);
            }
        }
    }
    

}
