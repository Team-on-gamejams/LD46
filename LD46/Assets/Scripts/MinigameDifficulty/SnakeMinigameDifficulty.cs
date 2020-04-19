using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Snake", menuName = "MinigameDifficulty/Snake")]
public class SnakeMinigameDifficulty : BaseMinigameDifficulty {
    [Header("Balance")]
    public float speed = 2;
    public float maxSpeed = 10;
}
