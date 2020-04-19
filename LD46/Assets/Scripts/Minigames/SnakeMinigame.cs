using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnakeMinigame : BaseMinigame {
	public SnakeMinigameDifficulty difficulty;

	public override void Init(byte usedDifficulty) {
		base.Init(usedDifficulty);
		difficulty = difficultyBase as SnakeMinigameDifficulty;
	}

	public void Win() {
		isPlaying = false;
		ShowWinAnimation();
	}

	protected override void ShowLoseAnimation() {
		LeanTween.delayedCall(1.0f, () => {
			base.ShowLoseAnimation();
		});
	}

	protected override void ShowWinAnimation() {
		LeanTween.delayedCall(1.0f, () => {
			base.ShowWinAnimation();
		});
	}
}
