using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PandaMinigame : BaseMinigame {
	[Header("Balance")]
	[SerializeField] byte matchs = 4;
	[SerializeField] byte maxWrongMatchs = 2;
	[SerializeField] bool randomLeftRight = false;
	byte wrongMatchs = 0;

	[Header("Refs")]
	[SerializeField] Transform centerPanda = null;
	[SerializeField] Transform leftPanda = null;
	[SerializeField] Transform rightPanda = null;

	[SerializeField] SpriteRendererAnimator[] genders = null;

	[Header("Debug")]
	[SerializeField] TextMeshProUGUI debugTextField = null;

	Vector3 centerPos;
	Vector3 leftPos;
	Vector3 rightPos;
	Vector3 leftScale;
	Vector3 rightScale;

	bool centerGender;
	bool leftGender;
	bool rightGender;

	public override void Init() {
		base.Init();

		centerPos = centerPanda.position;
		leftPos = leftPanda.position;
		rightPos = rightPanda.position;
		leftScale = leftPanda.localScale;
		rightScale = rightPanda.localScale;

		SpawnNewPanda();
	}

	protected new void Update() {
		base.Update();

		if (isPlaying) {
			if (Input.GetMouseButtonDown(0)) {
				if (leftGender == centerGender)
					WrongMatch();
				else
					RightMatch();
			}
			if (Input.GetMouseButtonDown(1)) {
				if (rightGender == centerGender)
					WrongMatch();
				else
					RightMatch();
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}
	}

	void RightMatch() {
		--matchs;

		debugTextField.text = $"Prev math right.";

		if (matchs <= 0) {
			isPlaying = false;
			ShowWinAnimation();
		}
		else {
			SpawnNewPanda();
		}
	}

	void WrongMatch() {
		--matchs;
		++wrongMatchs;

		debugTextField.text = $"Prev math wrong.";

		if (wrongMatchs >= maxWrongMatchs) {
			isPlaying = false;
			ShowLoseAnimation();
		}
		else {
			SpawnNewPanda();
		}
	}

	void SpawnNewPanda() {
		debugTextField.text += $" Total: {matchs} WrongLeft: {wrongMatchs}";

		centerGender = Random.Range(0, 2) == 0;
		if (randomLeftRight) {
			leftGender = Random.Range(0, 2) == 0;
			rightGender = Random.Range(0, 2) == 0;
		}
		else {
			leftGender = false;
			rightGender = true;
		}

		Destroy(centerPanda.gameObject);
		Destroy(leftPanda.gameObject);
		Destroy(rightPanda.gameObject);

		centerPanda = Instantiate(genders[centerGender ? 1 : 0], centerPos, Quaternion.identity, transform).transform;
		leftPanda = Instantiate(genders[leftGender ? 1 : 0], leftPos, Quaternion.identity, transform).transform;
		rightPanda = Instantiate(genders[rightGender ? 1 : 0], rightPos, Quaternion.identity, transform).transform;

		centerPanda.transform.localScale = new Vector3(centerGender != rightGender ? 1 : -1, 1, 1);
		leftPanda.localScale = leftScale;
		rightPanda.localScale = rightScale;
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
