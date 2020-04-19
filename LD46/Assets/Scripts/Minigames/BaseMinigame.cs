using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//TODO: rename to timer minigame
public class BaseMinigame : BaseBaseMinigame {
	[Header("Timer")]
	[SerializeField] bool isWinIfTimerEnds = false;
	[SerializeField] TextMeshProUGUI timerTextField = null;
	[SerializeField] float maxTime = 5.0f;
	float currTime;

	protected void Awake() {
		currTime = maxTime;
	}

	protected void Update() {
		if (isPlaying) {
			currTime -= Time.deltaTime;
			if (currTime <= 0) {
				if (isWinIfTimerEnds)
					ShowWinAnimation();
				else
					ShowLoseAnimation();
				isPlaying = false;
				currTime = 0;
			}
			timerTextField.text = currTime.ToString("0.00");
		}
	}
}
