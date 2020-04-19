using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BaseMinigameDifficulty", menuName = "MinigameDifficulty/Base")]
public class BaseMinigameDifficulty : ScriptableObject {
	[Header("Delays")]
	public float delayBeforePlay = 1.0f;

	[Header("Timer")]
	public float timer = 10.0f;
	public bool isWinIfTimerEnds = false;
}
