using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crab", menuName = "MinigameDifficulty/Crab")]
public class CrabMinigameDifficulty : BaseMinigameDifficulty {
	[Header("Balance")]
	public float speed = 10.0f;
	public float startTime;
	public float obstacleSpeed = 5f;
}
