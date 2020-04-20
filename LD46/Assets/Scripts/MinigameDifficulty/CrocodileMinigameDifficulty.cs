using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crocodile", menuName = "MinigameDifficulty/Crocodile")]
public class CrocodileMinigameDifficulty : BaseMinigameDifficulty {
	[Header("Balance")]
	public byte toothBrushFlyForce = 5;
	public float alphaChangePerEnter = 0.05f;
	public float alphaChangePerUnit = 1f;
}
