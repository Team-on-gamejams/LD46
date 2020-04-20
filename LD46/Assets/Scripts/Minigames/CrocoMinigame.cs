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

		Debug.Log($"{center.position.y.ToString("0.00")} {Toothbrush.position.y.ToString("0.00")} {flip}");

		if (isPlaying && 
			((center.position.y > Toothbrush.position.y && !flip) || (center.position.y <= Toothbrush.position.y && flip))
			) {
			flip = !flip;
			LeanTween.cancel(Toothbrush.gameObject, false);
			LeanTween.rotateLocal(Toothbrush.gameObject, new Vector3(180.0f - (Toothbrush.localEulerAngles.x % 180), Toothbrush.localEulerAngles.y, Toothbrush.localEulerAngles.z), 0.2f);
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
