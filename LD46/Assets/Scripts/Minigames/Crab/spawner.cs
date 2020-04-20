using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public GameObject Obstacle;
    void Start()
    {
        Instantiate(Obstacle, transform.position, Quaternion.identity);
    }
}
