using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//TODO: rename to TimerMinigame
public class BaseMinigame : BaseBaseMinigame {

	[Header("Timer")]
	[SerializeField] TextMeshProUGUI timerTextField = null;
	float currTime;

	public override void Init(byte usedDifficulty) {
		base.Init(usedDifficulty);
		currTime = difficultyBase.timer;
		timerTextField.text = currTime.ToString("0.00");

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
			timerTextField.text = currTime.ToString("0.00");
		}
	}
}
