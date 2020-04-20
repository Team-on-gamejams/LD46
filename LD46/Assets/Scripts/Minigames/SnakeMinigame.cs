using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnakeMinigame : BaseMinigame {
	public SnakeMinigameDifficulty difficulty;

	[SerializeField] SpriteRenderer sr;
	[SerializeField] SpriteRendererAnimator srAnim;

	[SerializeField] Sprite winSprite;

	[SerializeField] GameObject minigame;
	[SerializeField] GameObject winAnimation;
	[SerializeField] GameObject loseAnimation;

	public override void Init(byte usedDifficulty) {
		base.Init(usedDifficulty);
		difficulty = difficultyBase as SnakeMinigameDifficulty;
	}

	public void Win() {
		isPlaying = false;
		srAnim.enabled = false;
		sr.sprite = winSprite;
		ShowWinAnimation();
	}

	protected override void ShowLoseAnimation() {
		loseAnimation.SetActive(true);
		minigame.SetActive(false);
		LeanTween.delayedCall(2.0f, () => {
			base.ShowLoseAnimation();
		});
	}

	protected override void ShowWinAnimation() {
		LeanTween.delayedCall(2.0f, () => {
			winAnimation.SetActive(true);
			minigame.SetActive(false);
			LeanTween.delayedCall(1.0f, () => {
				base.ShowWinAnimation();
			});
		});
	}
}
