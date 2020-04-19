using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Egg", menuName = "MinigameDifficulty/Egg hatch")]

public class EggHatchMinigameDifficulty : BaseMinigameDifficulty {
	[Header("Balance")]
	public float neededDist = 8000;
}
