using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Player : MonoBehaviour {
	[SerializeField] [ReorderableList] List<BaseMinigame> minigames = null;

	BaseMinigame currMinigame = null;
	byte currMinigameId = 0;

	private void Start() {
		StartNewMinigame();
	}

	void OnWinMinigame() {
		++currMinigameId;
		StartNewMinigame();
	}

	void OnLoseMinigame() {
		++currMinigameId;
		StartNewMinigame();
	}

	void StartNewMinigame() {
		Debug.Log($"Start new minigame. Id: {currMinigameId}");
		currMinigame = Instantiate(minigames[currMinigameId].gameObject, transform).GetComponent<BaseMinigame>();

		currMinigame.onWinEvent += OnWinMinigame;
		currMinigame.onLoseEvent += OnLoseMinigame;

		currMinigame.Init();
	}
}
