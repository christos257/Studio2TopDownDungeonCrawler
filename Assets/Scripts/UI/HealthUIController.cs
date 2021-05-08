using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public GameObject healthContainer;
    private float fillCounter;


    void Start()
    {
        
    }
    
    void Update()
    {
        fillCounter = (float)GameController.Health;
        fillCounter = fillCounter / GameController.MaxHealth;
        healthContainer.GetComponent<Image>().fillAmount = fillCounter;
    }
}
