using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BaseMinigame : MonoBehaviour {
	public Action onWinEvent = null;
	public Action onLoseEvent = null;

	[NonSerialized] public bool isPlaying = false;

	[Header("Timer")]
	[SerializeField] TextMeshProUGUI timerTextField = null;
	[SerializeField] float maxTime = 5.0f;
	float currTime;

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

	protected void Awake() {
		currTime = maxTime;
	}

	protected void Update() {
		if (isPlaying) {
			currTime -= Time.deltaTime;
			if (currTime <= 0) {
				ShowLoseAnimation();
				isPlaying = false;
				currTime = 0;
			}
			timerTextField.text = currTime.ToString("0.00");
		}
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
