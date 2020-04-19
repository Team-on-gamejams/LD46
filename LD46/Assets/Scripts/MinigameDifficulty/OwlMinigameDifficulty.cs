using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Owl", menuName = "MinigameDifficulty/Owl")]
public class OwlMinigameDifficulty : BaseMinigameDifficulty {
	[Header("Balance")]
	public float maxAngle = 70;
	public float fallSpeedAcceleration = 4;
	public float fallSpeedMax = 20;
	public float chengeByClick = 10;
}
