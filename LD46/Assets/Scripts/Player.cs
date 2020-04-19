using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class Player : MonoBehaviour {
	const string SAVE_PROGRESS_KEY = "Difficulty";

	[Header("Minigames")]
	[SerializeField] [ReorderableList] List<BaseBaseMinigame> minigames = null;

	[Header("Audio")]
	[SerializeField] AudioClip mainTheme = null;

	[Header("Refs")]
	[SerializeField] GameMenu gameMenu = null;
	[SerializeField] TextMeshProUGUI debugTextField = null;

	BaseBaseMinigame currMinigame = null;
	byte currMinigameId = 0;
	byte currDifficulty = 0;

	private void Awake() {
		currDifficulty = (byte)PlayerPrefs.GetInt(SAVE_PROGRESS_KEY, 0);
		debugTextField.text = $"Difficulty: {currDifficulty}";
	}

	private void Start() {
		if (mainTheme != null) {
			LeanTween.delayedCall(0.1f, () => {
				AudioManager.Instance.PlayLoop(mainTheme, 0.7f, channel: AudioManager.AudioChannel.Music);
			});
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.gameObject.SetActive(!debugTextField.gameObject.activeSelf);
		}
	}

	public void OnClearDifficultyClick() {
		currDifficulty = 0;
		PlayerPrefs.SetInt(SAVE_PROGRESS_KEY, currDifficulty);
		debugTextField.text = $"Difficulty: {currDifficulty}";
	}	
	
	public void OnAddDifficultyClick() {
		++currDifficulty;
		PlayerPrefs.SetInt(SAVE_PROGRESS_KEY, currDifficulty);
		debugTextField.text = $"Difficulty: {currDifficulty}";
	}

	public void StartLoop() {
		gameMenu.HideMainMenu();
		StartNewMinigameInSequence();
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

	void StartNewMinigameInSequence() {
		Debug.Log($"Start new minigame. Id: {currMinigameId}");
		currMinigame = Instantiate(minigames[currMinigameId].gameObject, transform).GetComponent<BaseBaseMinigame>();

		currMinigame.onWinEvent += OnWinMinigameInSequence;
		currMinigame.onLoseEvent += OnLoseMinigameInSequence;

		currMinigame.Init(currDifficulty);
	}

	void OnWinMinigameInSequence() {
		++currMinigameId;
		if (currMinigameId == minigames.Count) {
			OnEndSequence();
			return;
		}

		StartNewMinigameInSequence();
	}

	void OnLoseMinigameInSequence() {
		++currMinigameId;
		if (currMinigameId == minigames.Count) {
			OnEndSequence();
			return;
		}

		StartNewMinigameInSequence();
	}

	void OnEndSequence() {
		minigames.Shuffle();
		currMinigameId = 0;

		++currDifficulty;
		PlayerPrefs.SetInt(SAVE_PROGRESS_KEY, currDifficulty);
		debugTextField.text = $"Difficulty: {currDifficulty}";

		gameMenu.ShowMainMenu();
	}

	void StartSingleMinigame(BaseBaseMinigame minigame) {
		Debug.Log($"Start new minigame. Single: {minigame.transform.name}");
		currMinigame = Instantiate(minigame.gameObject, transform).GetComponent<BaseBaseMinigame>();

		currMinigame.onWinEvent += OnEndSingle;
		currMinigame.onLoseEvent += OnEndSingle;

		currMinigame.Init(currDifficulty);
	}

	void OnEndSingle() {
		gameMenu.ShowMainMenu();
	}
}
