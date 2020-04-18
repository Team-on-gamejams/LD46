using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//TODO: rename to BaseMinigame
public class BaseBaseMinigame : MonoBehaviour {
	public Action onWinEvent = null;
	public Action onLoseEvent = null;

	[NonSerialized] public bool isPlaying = false;

	//Spaw objects for minigame, subscribe events, etc
	public virtual void Init() {
		Debug.Log($"Init minigame {transform.name}");
		isPlaying = true;
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
