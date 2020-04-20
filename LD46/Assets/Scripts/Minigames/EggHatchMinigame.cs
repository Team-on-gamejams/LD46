using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class EggHatchMinigame : BaseMinigame {
	[Header("Balance")]
	float neededDistHalf;

	[Header("Refs")]
	[SerializeField] SpriteRendererAnimator3 sranim = null;
	[SerializeField] SpriteRenderer sr = null;
	[SerializeField] Color loseColor = Color.white;

	[Header("Debug")]
	[SerializeField] TextMeshProUGUI debugTextField = null;

	bool isPointerInside = false;
	float dist = 0;
	Vector3 lastPos, currPos, deltaPos;

  public GameObject minigame;
  public GameObject WinAnimation;

  EggHatchMinigameDifficulty difficulty;

	public override void Init(byte usedDIfficulty) {
		base.Init(usedDIfficulty);
		difficulty = difficultyBase as EggHatchMinigameDifficulty;

		neededDistHalf = difficulty.neededDist / 2;
		debugTextField.text = $"Progress: {dist.ToString("0")}/{difficulty.neededDist.ToString("0")}   Last: {deltaPos.magnitude.ToString("0")}";
	}

	private new void Update() {
		base.Update();

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}
	}

	public void OnEnterPointer() {
		isPointerInside = true;
	}

	public void OnExitPointer() {
		isPointerInside = false;
	}

	public void OnDragEgg() {
		currPos = Input.mousePosition;
		if (lastPos == Vector3.zero)
			lastPos = currPos;

		if (isPlaying && isPointerInside) {
			deltaPos = currPos - lastPos;
			dist += deltaPos.magnitude;

			if (dist >= neededDistHalf && sranim.currSequence <= 0) {
				sranim.currSequence = 1;
			}
			else if (dist >= difficulty.neededDist && sranim.currSequence <= 1) {
				sranim.currSequence = 2;
				isPlaying = false;
				ShowWinAnimation();
			}
		}

		lastPos = currPos;

		debugTextField.text = $"Progress: {dist.ToString("0")}/{difficulty.neededDist.ToString("0")}   Last: {deltaPos.magnitude.ToString("0")}";
	}

	protected override void ShowLoseAnimation() {
	debugTextField.text = "Loser, ahahahah";
	sranim.enabled = false;
		LeanTween.value(gameObject, sr.color, loseColor, 1.0f)
		.setDelay(0.5f)
		.setOnUpdateColor(c=> {
			sr.color = c;
		});
		LeanTween.delayedCall(2.0f, () => {
			base.ShowLoseAnimation();
		});
	}

	protected override void ShowWinAnimation() {
	debugTextField.text = "You win";
    minigame.SetActive(false);
    WinAnimation.SetActive(true);
    LeanTween.delayedCall(4.3f, () => {
			base.ShowWinAnimation();
		});
	}
}
