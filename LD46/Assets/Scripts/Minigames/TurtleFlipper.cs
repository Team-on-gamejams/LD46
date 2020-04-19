using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class TurtleFlipper : BaseMinigame {
	[Header("Balance")]
	public GameObject Arrow;

	[SerializeField] [ReorderableList] TurtleActions[] taList = null;
	float arrowRotateSpeed;

	[Header("Debug")]
	public TextMeshProUGUI debugTextField = null;

	new void Update() {
		base.Update();

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}

		if(isPlaying)
			Arrow.transform.Rotate(new Vector3(0, 0, arrowRotateSpeed) * Time.deltaTime, Space.Self);
	}

	public override void Init(byte usedDifficulty) {
		base.Init(usedDifficulty);

		Arrow.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)), Space.Self);
		arrowRotateSpeed = Random.Range(0, 2) == 1 ? Random.Range(70f, 120f) : Random.Range(-120f, -70f);
	}

	public void GameCondition() {
		if (!isPlaying)
			return;

		bool result = true;

		foreach (TurtleActions ta in taList) {
			if (!ta.GetWinState()) {
				result = false;
				break;
			}
		}

		if (result) {
			isPlaying = false;
			ShowWinAnimation();
		}
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
