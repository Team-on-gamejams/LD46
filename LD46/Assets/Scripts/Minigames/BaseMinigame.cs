using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//TODO: rename to TimerMinigame
public class BaseMinigame : BaseBaseMinigame {

	[Header("Timer")]
	[SerializeField] Slider timerTextField = null;
	float currTime;

	public override void Init(byte usedDifficulty) {
		base.Init(usedDifficulty);
		currTime = difficultyBase.timer;
		timerTextField.value = 1.0f;

		LeanTween.delayedCall(difficultyBase.delayBeforePlay, () => {
			isPlaying = true;
		});
	}

	protected void Update() {
		if (isPlaying) {
			currTime -= Time.deltaTime;
			if (currTime <= 0) {
				isPlaying = false;
				if (difficultyBase.isWinIfTimerEnds)
					ShowWinAnimation();
				else
					ShowLoseAnimation();
				currTime = 0;
			}
			timerTextField.value = currTime / difficultyBase.timer;
		}
	}
}
