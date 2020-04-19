using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Panda", menuName = "MinigameDifficulty/Panda")]
public class PandaMinigameDifficulty : BaseMinigameDifficulty {
	[Header("Balance")]
	public byte matchs = 4;
	public byte maxWrongMatchs = 2;
	public bool randomLeftRight = false;
}
