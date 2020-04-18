using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class EggHatchMinigame : BaseMinigame {
	[Header("Balance")]
	[SerializeField] float neededDist = 500;
	float neededDistHalf;

	[Header("Refs")]
	[SerializeField] SpriteRendererAnimator3 sranim = null;

	[Header("Debug")]
	[SerializeField] TextMeshProUGUI debugTextField = null;

	float dist = 0;
	Vector3 lastPos, currPos, deltaPos;

	public override void Init() {
		base.Init();

		neededDistHalf = neededDist / 2;
	}

	//protected new void Update() {
	//	currPos = Input.mousePosition;

	//	base.Update();

	//	if (isPlaying) {
	//		if (Input.GetMouseButtonDown(0)) {
	//			deltaPos = currPos - lastPos;
	//			dist += deltaPos.magnitude;

	//			if(dist >= neededDistHalf && sranim.currSequence <= 0) {
	//				sranim.currSequence = 1;
	//			}
	//			else if(dist >= neededDist && sranim.currSequence <= 1) {
	//				sranim.currSequence = 2;
	//				isPlaying = false;
	//				ShowWinAnimation();
	//			}
	//		}
	//	}

	//	lastPos = currPos;
	//}

	public void OnDragEgg() {
		currPos = Input.mousePosition;
		if (lastPos == Vector3.zero)
			lastPos = currPos;

		if (isPlaying) {
			deltaPos = currPos - lastPos;
			dist += deltaPos.magnitude;

			if (dist >= neededDistHalf && sranim.currSequence <= 0) {
				sranim.currSequence = 1;
			}
			else if (dist >= neededDist && sranim.currSequence <= 1) {
				sranim.currSequence = 2;
				isPlaying = false;
				ShowWinAnimation();
			}
		}

		lastPos = currPos;

		debugTextField.text = $"Progress: {dist.ToString("0")}/{neededDist.ToString("0")}   Last: {deltaPos.magnitude.ToString("0")}";
		Debug.Log(deltaPos.magnitude);
	}

	protected override void ShowLoseAnimation() {
		debugTextField.text = "Loser, ahahahah";
		LeanTween.delayedCall(1.0f, () => {
			base.ShowLoseAnimation();
		});
	}

	protected override void ShowWinAnimation() {
		debugTextField.text = "You win";
		LeanTween.delayedCall(1.0f, () => {
			base.ShowWinAnimation();
		});
	}
}
