﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name=="crab" || other.gameObject.name == "die")
        {
            Destroy(gameObject);
        }
    }
}
