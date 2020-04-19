using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;

public class OwlMinigame : BaseMinigame {
	[Header("Balance")]
	[SerializeField] float maxAngle = 85;
	[SerializeField] float fallSpeedAcceleration = 1;
	[SerializeField] float fallSpeedMax = 20;
	[SerializeField] float chengeByClick = 10;
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


	public override void Init() {
		base.Init();

		isFallLeft = Random.Range(0, 2) == 1;
		owl.SetParent(isFallLeft ? leftAhchor : rightAhchor);

		currZ = isFallLeft ? Random.Range(maxAngle / 4, maxAngle / 2) : Random.Range(-maxAngle / 2, -maxAngle / 4);
	}

	new void Update() {
		base.Update();

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}

		if (isPlaying) {
			CheckIsNeedOtherSide();

			if (isFallLeft)
				fallSpeed += fallSpeedAcceleration * Time.deltaTime;
			else
				fallSpeed -= fallSpeedAcceleration * Time.deltaTime;
			fallSpeed = Mathf.Clamp(fallSpeed, -fallSpeedMax, fallSpeedMax);

			currZ += fallSpeed * Time.deltaTime;
			CheckIsNeedOtherSide();


			if (isFallLeft) {
				leftAhchor.rotation = Quaternion.Euler(0, 0, currZ);
				leftLeg.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else {
				rightAhchor.rotation = Quaternion.Euler(0, 0, currZ);
				rightLeg.transform.rotation = Quaternion.Euler(0, 0, 0);
			}

			if (currZ < -maxAngle || maxAngle < currZ) {
				isPlaying = false;
				ShowLoseAnimation();
			}

			debugTextField.text = $"Angle: {(int)currZ}/{(int)maxAngle}  Speed: {fallSpeed.ToString("0.0")}/{fallSpeedMax.ToString("0.0")}";
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
	}

	public void OnLeftClick() {
		currZ += chengeByClick;
		if (isFallLeft)
			fallSpeed += chengeByClick * Time.deltaTime;
	}

	public void OnRightClick() {
		currZ -= chengeByClick;
		if (!isFallLeft)
			fallSpeed -= chengeByClick * Time.deltaTime;
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
