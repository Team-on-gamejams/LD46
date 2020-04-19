using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Player : MonoBehaviour {
	[Header("Minigames")]
	[SerializeField] [ReorderableList] List<BaseBaseMinigame> minigames = null;

	[Header("Audio")]
	[SerializeField] AudioClip mainTheme = null;

	BaseBaseMinigame currMinigame = null;
	byte currMinigameId = 0;

	private void Start() {
		if (mainTheme != null)
			AudioManager.Instance.PlayLoop(mainTheme, 0.7f, channel: AudioManager.AudioChannel.Music);
	}

	public void StartLoop() {
		StartNewMinigame();
	}
	
	public void PlayMinigame(MinigameType type) {
		StartNewMinigame();
	}

	void OnWinMinigame() {
		++currMinigameId;
		if (currMinigameId == minigames.Count) {
			minigames.Shuffle();
			currMinigameId = 0;
		}

		StartNewMinigame();
	}

	void OnLoseMinigame() {
		++currMinigameId;
		if (currMinigameId == minigames.Count) {
			minigames.Shuffle();
			currMinigameId = 0;
		}

		StartNewMinigame();
	}

	void StartNewMinigame() {
		Debug.Log($"Start new minigame. Id: {currMinigameId}");
		currMinigame = Instantiate(minigames[currMinigameId].gameObject, transform).GetComponent<BaseBaseMinigame>();

		currMinigame.onWinEvent += OnWinMinigame;
		currMinigame.onLoseEvent += OnLoseMinigame;

		currMinigame.Init();
	}
}
