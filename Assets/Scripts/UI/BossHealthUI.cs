using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{   
    public GameObject bossHealthContainer;
    private float fillCounter;

    void Update()
    {
        fillCounter = (float)GameController.BossHealth;
        fillCounter = fillCounter / GameController.BossMaxHealth;
        bossHealthContainer.GetComponent<Image>().fillAmount = fillCounter;
    }
}
