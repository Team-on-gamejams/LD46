using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;

public class OwlMinigame : BaseMinigame {
	float fallSpeed;
	float currZ;
	bool isFallLeft;

	[Header("Refs")]
	[SerializeField] Transform owl;
	[SerializeField] Transform leftAhchor;
	[SerializeField] Transform rightAhchor;
	[SerializeField] SpriteRenderer leftLeg;
	[SerializeField] SpriteRenderer rightLeg;

	[Header("Debug")]
	[SerializeField] TextMeshProUGUI debugTextField = null;

	OwlMinigameDifficulty difficulty;

	public GameObject LoseAnimation;
	public GameObject WinAnimation;
	public GameObject LoseCanvas;
	public GameObject WinCanvas;

	public override void Init(byte usedDIfficulty) {
		base.Init(usedDIfficulty);
		difficulty = difficultyBase as OwlMinigameDifficulty;

		isFallLeft = Random.Range(0, 2) == 1;
		owl.SetParent(isFallLeft ? leftAhchor : rightAhchor);

		currZ = isFallLeft ? Random.Range(difficulty.maxAngle / 4, difficulty.maxAngle / 2) : Random.Range(-difficulty.maxAngle / 2, -difficulty.maxAngle / 4);

		CheckIsNeedOtherSide();
		SetRotation();
	}

	new void Update() {
		base.Update();

#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}
#endif

		if (isPlaying) {
			CheckIsNeedOtherSide();

			if (isFallLeft)
				fallSpeed += difficulty.fallSpeedAcceleration * Time.deltaTime;
			else
				fallSpeed -= difficulty.fallSpeedAcceleration * Time.deltaTime;
			fallSpeed = Mathf.Clamp(fallSpeed, -difficulty.fallSpeedMax, difficulty.fallSpeedMax);

			currZ += fallSpeed * Time.deltaTime;
			CheckIsNeedOtherSide();

			SetRotation();

			if (currZ < -difficulty.maxAngle || difficulty.maxAngle < currZ) {
				isPlaying = false;
				ShowLoseAnimation();
			}

			debugTextField.text = $"Angle: {(int)currZ}/{(int)difficulty.maxAngle}  Speed: {fallSpeed.ToString("0.0")}/{difficulty.fallSpeedMax.ToString("0.0")}";
		}
	}

	void SetRotation() {
		if (isFallLeft) {
			leftAhchor.rotation = Quaternion.Euler(0, 0, currZ);
			leftLeg.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else {
			rightAhchor.rotation = Quaternion.Euler(0, 0, currZ);
			rightLeg.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}

	void CheckIsNeedOtherSide() {
		if (isFallLeft && currZ <= 0) {
			isFallLeft = false;
			fallSpeed = currZ;

			leftLeg.transform.rotation = Quaternion.Euler(0, 0, 0);
			rightLeg.transform.rotation = Quaternion.Euler(0, 0, 0);

			owl.SetParent(rightAhchor.parent);
			owl.localPosition = Vector3.zero;
			owl.rotation = leftAhchor.rotation = Quaternion.Euler(Vector3.zero);
			owl.SetParent(rightAhchor);
		}
		else if (!isFallLeft && currZ >= 0) {
			isFallLeft = true;
			fallSpeed = currZ;

			leftLeg.transform.rotation = Quaternion.Euler(0, 0, 0);
			rightLeg.transform.rotation = Quaternion.Euler(0, 0, 0);

			owl.SetParent(leftAhchor.parent);
			owl.localPosition = Vector3.zero;
			owl.rotation = rightAhchor.rotation = Quaternion.Euler(Vector3.zero);
			owl.SetParent(leftAhchor);
		}
	}

	public void OnLeftClick() {
		currZ -= difficulty.chengeByClick;
		if (!isFallLeft)
			fallSpeed -= difficulty.chengeByClick * Time.deltaTime;
	}

	public void OnRightClick() {
		currZ += difficulty.chengeByClick;
		if (isFallLeft)
			fallSpeed += difficulty.chengeByClick * Time.deltaTime;
	}

	protected override void ShowLoseAnimation() {
		debugTextField.text = "Loser, ahahahah";
		owl.GetComponent<SpriteRenderer>().enabled = false;
		LoseAnimation.SetActive(true);
		LoseCanvas.SetActive(true);
		LeanTween.delayedCall(3.0f, () => {
			base.ShowLoseAnimation();
		});

	}

	protected override void ShowWinAnimation() {
		debugTextField.text = "You win";
		WinAnimation.SetActive(true);
		WinCanvas.SetActive(true);
		LeanTween.delayedCall(3.0f, () => {
			base.ShowWinAnimation();
		});
	}
}
