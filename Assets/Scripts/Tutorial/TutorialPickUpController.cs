using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    public string name;

    public string description;

    public Sprite image;
}
public class TutorialPickUpController : MonoBehaviour
{
    public Item items;

    public float healthChange;
    public float moveSpeedChange;
    public float fireRateChange;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = items.image;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TutorialPlayer")
        {
            GameController.HealPlayer(healthChange);
            GameController.MoveSpeedChange(moveSpeedChange);
            GameController.FireRateChange(fireRateChange);
            Destroy(gameObject);
        }

    }
}
