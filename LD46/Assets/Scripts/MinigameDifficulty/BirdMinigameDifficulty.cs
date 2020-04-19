using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bird", menuName = "MinigameDifficulty/Bird")]
public class BirdMinigameDifficulty : BaseMinigameDifficulty {
	[Header("Balance")]
	public byte maxSequenceLength = 6;
}
