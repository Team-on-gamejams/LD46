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

	public override void Init() {
		base.Init();
		currTime = maxTime;
		timerTextField.text = currTime.ToString("0.00");

		LeanTween.delayedCall(delayBeforePlay, () => {
			isPlaying = true;
		});
	}

	protected void Update() {
		if (isPlaying) {
			currTime -= Time.deltaTime;
			if (currTime <= 0) {
				isPlaying = false;
				if (isWinIfTimerEnds)
					ShowWinAnimation();
				else
					ShowLoseAnimation();
				currTime = 0;
			}
			timerTextField.text = currTime.ToString("0.00");
		}
	}
}
