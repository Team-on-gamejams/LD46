using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrocoMinigame : BaseMinigame {
	[SerializeField] Transform Toothbrush = null;
	[SerializeField] GameObject FrontCaries = null;
	[SerializeField] List<Transform> Caries = null;
	[SerializeField] CrocoBrush brush = null;
	[SerializeField] Transform center = null;

  public GameObject LoseAnimation;
  public GameObject WinAnimation;

  bool flip = false;
	bool wonState = false;

	SpriteRenderer ToothbrushSr;
	CapsuleCollider2D ToothbrushCapsuleCollider2D;
	CrocodileMinigameDifficulty difficulty;

	public override void Init(byte usedDifficulty) {
		base.Init(usedDifficulty);
		difficulty = difficultyBase as CrocodileMinigameDifficulty;

		brush.force = difficulty.toothBrushFlyForce;
		brush.alphaChangePerEnter = difficulty.alphaChangePerEnter;
		brush.alphaChangePerUnit = difficulty.alphaChangePerUnit;
	}

	void Awake() {
		foreach (Transform child in FrontCaries.transform)
			Caries.Add(child);

		ToothbrushSr = Toothbrush.GetComponent<SpriteRenderer>();
		ToothbrushCapsuleCollider2D = Toothbrush.GetComponent<CapsuleCollider2D>();
	}

	protected new void Update() {
		base.Update();

		if (isPlaying && 
			((center.position.y > Toothbrush.position.y && !flip) || (center.position.y <= Toothbrush.position.y && flip))
			) {
			flip = !flip;
			LeanTween.cancel(Toothbrush.gameObject, false);
			LeanTween.value(Toothbrush.localScale.y, flip ? -0.46574f : 0.46574f, 0.2f)
				.setOnUpdate(y => Toothbrush.localScale = new Vector3(0.46574f, y, 0.46574f));
		}
	}

	public void Win() {
		isPlaying = false;
		ShowWinAnimation();
	}

	public void CheckWonState() {
		wonState = true;
		foreach (Transform obj in Caries) {
			if (obj.gameObject.activeSelf) {
				wonState = false;
				break;
			}
		}

		if (wonState)
			Win();
	}

	protected override void ShowLoseAnimation() {
    LoseAnimation.SetActive(true);
		LeanTween.delayedCall(3.0f, () => {
      base.ShowLoseAnimation();
		});
	}

	protected override void ShowWinAnimation() {
    WinAnimation.SetActive(true);
    LeanTween.delayedCall(3.0f, () => {
			base.ShowWinAnimation();
		});
	}
}
