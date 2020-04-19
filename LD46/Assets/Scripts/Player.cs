using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Player : MonoBehaviour {
	[Header("Minigames")]
	[SerializeField] [ReorderableList] List<BaseBaseMinigame> minigames = null;

	[Header("Audio")]
	[SerializeField] AudioClip mainTheme = null;

	[Header("Refs")]
	[SerializeField] GameMenu gameMenu = null;

	BaseBaseMinigame currMinigame = null;
	byte currMinigameId = 0;

	private void Start() {
		if (mainTheme != null) {
			LeanTween.delayedCall(0.1f, () => {
				AudioManager.Instance.PlayLoop(mainTheme, 0.7f, channel: AudioManager.AudioChannel.Music);
			});
		}
	}

	public void StartLoop() {
		gameMenu.HideMainMenu();
		StartNewMinigame();
	}
	
	public void PlayMinigame(MinigameType type) {
		BaseBaseMinigame minigame = null;
		for(byte i = 0; i < minigames.Count; ++i) {
			if(minigames[i].type == type) {
				minigame = minigames[i];
				break;
			}
		}

		if(minigame != null) {
			gameMenu.HideMainMenu();
			StartSingleMinigame(minigame);
		}
	}

	void OnWinMinigame() {
		++currMinigameId;
		if (currMinigameId == minigames.Count) {
			OnEndSequence();
			return;
		}

		StartNewMinigame();
	}

	void OnLoseMinigame() {
		++currMinigameId;
		if (currMinigameId == minigames.Count) {
			OnEndSequence();
			return;
		}

		StartNewMinigame();
	}

	void OnEndSequence() {
		minigames.Shuffle();
		currMinigameId = 0;

		gameMenu.ShowMainMenu();
	}

	void StartNewMinigame() {
		Debug.Log($"Start new minigame. Id: {currMinigameId}");
		currMinigame = Instantiate(minigames[currMinigameId].gameObject, transform).GetComponent<BaseBaseMinigame>();

		currMinigame.onWinEvent += OnWinMinigame;
		currMinigame.onLoseEvent += OnLoseMinigame;

		currMinigame.Init();
	}

	void StartSingleMinigame(BaseBaseMinigame minigame) {
		Debug.Log($"Start new minigame. Single: {minigame.transform.name}");
		currMinigame = Instantiate(minigame.gameObject, transform).GetComponent<BaseBaseMinigame>();

		currMinigame.onWinEvent += OnEndSequence;
		currMinigame.onLoseEvent += OnEndSequence;

		currMinigame.Init();
	}
}
