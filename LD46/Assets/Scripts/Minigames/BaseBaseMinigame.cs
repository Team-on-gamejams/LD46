using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

//TODO: rename to BaseMinigame
public class BaseBaseMinigame : MonoBehaviour {
	public Action onWinEvent = null;
	public Action onLoseEvent = null;

	public MinigameType type = MinigameType.None;

	[NonSerialized] public bool isPlaying = false;

	[SerializeField] /*[ReorderableList]*/ protected BaseMinigameDifficulty[] difficulties;

	protected BaseMinigameDifficulty difficultyBase => difficulties[usedDifficulty];
	byte usedDifficulty = 0;

	//Spaw objects for minigame, subscribe events, etc
	public virtual void Init(byte _usedDifficulty) {
		usedDifficulty = _usedDifficulty;
		if (usedDifficulty >= difficulties.Length)
			usedDifficulty = (byte)(difficulties.Length - 1);
		Debug.Log($"Init minigame {transform.name}");
	}

	//Destroy minigame after animations
	public virtual void Uninit() {
		Debug.Log($"Uninit minigame {transform.name}");
		Destroy(gameObject);
	}


	//TODO: win/lose animation. Or maybe use animator for this

	//Trigger manually.
	//Dont forger to set isPlaying = false;
	protected virtual void ShowWinAnimation() {
		Debug.Log($"Win Animation minigame {transform.name}");
		onWinEvent.Invoke();
		Uninit();
	}

	//Triggered when timer reach 0
	protected virtual void ShowLoseAnimation() {
		Debug.Log($"Lose Animation minigame {transform.name}");
		onLoseEvent.Invoke();
		Uninit();
	}
}
