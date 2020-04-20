using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject Crab;

    private void Start()
    {
        Crab = GameObject.FindWithTag("Crab");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "crab")
        {
            Crab.GetComponent<CrabMinigame>().Lose();
            Destroy(gameObject);
        }
        else if (other.gameObject.name == "die")
            Destroy(gameObject);
    }
}
