using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class TurtleTupperMinigame : BaseMinigame {
	[Header("Rets")]
	[SerializeField] SpriteRendererAnimator2[] turtles = null;
	[SerializeField] SpriteRendererAnimator2[] turtleShadows = null;
	bool[] turtlesState = null;

	[Header("Debug")]
	[SerializeField] TextMeshProUGUI debugTextField = null;

	new void Update() {
		base.Update();

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}
	}

	public void OnTutrleClick(int id) {
		if (!isPlaying || turtlesState[id])
			return;

		turtlesState[id] = true;

		GameObject t = turtles[id].gameObject;
		float moveY = Random.Range(1.0f, 3.0f);
		float moveX = (Random.Range(0, 2) == 1 ? Random.Range(1f, 2f) : Random.Range(-2f, -1f));

		t.GetComponent<SpriteRenderer>().sortingOrder = id + 1;
		LeanTween.moveLocalY(t, t.transform.position.y + moveY, 0.3f)
			.setEase(LeanTweenType.easeOutCubic)
			.setOnComplete(()=> {
				turtleShadows[id].currSequence = turtles[id].currSequence = 1;
				LeanTween.moveLocalY(t, t.transform.position.y - moveY, 0.3f)
				.setEase(LeanTweenType.easeInCubic)
				.setOnComplete(()=> { 
					t.GetComponent<SpriteRenderer>().sortingOrder = 0;
				});
			});
		LeanTween.moveLocalX(t, t.transform.position.x + moveX, 0.6f);
		LeanTween.moveLocalX(turtleShadows[id].gameObject, turtleShadows[id].transform.position.x + moveX, 0.6f);


		bool isAllOnLegs = true;
		for (byte i = 0; i < turtlesState.Length; ++i) {
			if (!turtlesState[i]) {
				isAllOnLegs = false;
				break;
			}
		}
		
		if (isAllOnLegs) {
			isPlaying = false;
			ShowWinAnimation();
		}
	}

	public override void Init() {
		base.Init();

		turtlesState = new bool[turtles.Length];
		for(byte i = 0; i < turtlesState.Length; ++i) {
			turtlesState[i] = false;

			//turtles[i].GetComponent<SpriteRenderer>().sortingOrder = i;
			//turtleShadows[i].GetComponent<SpriteRenderer>().sortingOrder = i;
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
