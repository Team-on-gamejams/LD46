using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//TODO: rename to TimerMinigame
public class BaseMinigame : BaseBaseMinigame {

	[Header("Timer")]
	[SerializeField] Slider timerTextField = null;
	[SerializeField] CanvasGroup helpGroup = null;
	float currTime;

	bool isFirstClick;

#if UNITY_EDITOR
	private void OnValidate() {
		if (helpGroup == null)
			helpGroup = transform.Find("HelpGroup").GetComponent<CanvasGroup>();
	}
#endif

	public override void Init(byte usedDifficulty) {
		base.Init(usedDifficulty);
		currTime = difficultyBase.timer;
		timerTextField.value = 1.0f;
		isFirstClick = true;

		LeanTween.delayedCall(difficultyBase.delayBeforePlay, () => {
			HideHelpMenu();
		});
	}

	protected void Update() {
		if(isFirstClick && Input.GetMouseButtonDown(0)) {
			HideHelpMenu();
		}

		if (isPlaying) {
			currTime -= Time.deltaTime;
			if (currTime <= 0) {
				isPlaying = false;
				if (difficultyBase.isWinIfTimerEnds)
					ShowWinAnimation();
				else
					ShowLoseAnimation();
				currTime = 0;
			}
			timerTextField.value = currTime / difficultyBase.timer;
		}
	}

	void HideHelpMenu() {
		if (isFirstClick) {
			isPlaying = true;
			isFirstClick = false;
			LeanTween.moveLocalY(helpGroup.gameObject, helpGroup.transform.position.y + 0.1f, 0.2f);
			LeanTweenEx.ChangeCanvasGroupAlpha(helpGroup, 0.0f, 0.2f);
		}
	}
}
