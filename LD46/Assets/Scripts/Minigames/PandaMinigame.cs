using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PandaMinigame : BaseMinigame {
	byte matchs = 0;
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

	PandaMinigameDifficulty difficulty;

	public override void Init(byte usedDIfficulty) {
		base.Init(usedDIfficulty);
		difficulty = difficultyBase as PandaMinigameDifficulty;
		matchs = difficulty.matchs;

		centerPos = centerPanda.position;
		leftPos = leftPanda.position;
		rightPos = rightPanda.position;
		leftScale = leftPanda.localScale;
		rightScale = rightPanda.localScale;

		SpawnNewPanda();
	}

	protected new void Update() {
		base.Update();

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}
	}

	public void LeftClick() {
		if (!isPlaying)
			return;
		if (leftGender == centerGender)
			WrongMatch();
		else
			RightMatch();
	}

	public void RightClick() {
		if (!isPlaying)
			return;
		if (rightGender == centerGender)
			WrongMatch();
		else
			RightMatch();
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

		if (wrongMatchs >= difficulty.maxWrongMatchs) {
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
		if (difficulty.randomLeftRight) {
			leftGender = Random.Range(0, 2) == 0;
			rightGender = !leftGender;
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

		//centerPanda.transform.localScale = new Vector3(centerGender != rightGender ? 1 : -1, 1, 1);
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
