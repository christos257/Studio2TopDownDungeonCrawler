using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TutorialPlayer")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        }
    }
}
