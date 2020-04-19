using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLauncher : MonoBehaviour {
	[SerializeField] MinigameType type;
	[SerializeField] Player player;
	
	public void OnClick() {
		player.PlayMinigame(type);
	}
}
