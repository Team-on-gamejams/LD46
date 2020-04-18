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
	[SerializeField] SpriteRenderer centerPanda;
	[SerializeField] SpriteRenderer leftPanda;
	[SerializeField] SpriteRenderer rightPanda;

	[SerializeField] Sprite[] genders;

	[SerializeField] TextMeshProUGUI debugTextField;

	bool centerGender;
	bool leftGender;
	bool rightGender;

	public override void Init() {
		base.Init();

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

		if (randomLeftRight) {
			leftGender = Random.Range(0, 2) == 0;
			rightGender = Random.Range(0, 2) == 0;
		}
		else {
			leftGender = false;
			rightGender = true;
		}
		
		centerGender = Random.Range(0, 2) == 0;

		centerPanda.sprite = genders[centerGender ? 1 : 0];
		leftPanda.sprite = genders[leftGender ? 1 : 0];
		rightPanda.sprite = genders[rightGender ? 1 : 0];
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
