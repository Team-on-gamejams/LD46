using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.EventSystems;

public class TurtleTupperMinigame : BaseMinigame {
	[Header("Refs")]
	[SerializeField] GameObject turtlePrefab;
	[SerializeField] GameObject turtleShadowPrefab;
	[SerializeField] Transform[] turtleSpawnPosRandom;
	SpriteRendererAnimator2[] turtles = null;
	SpriteRendererAnimator2[] turtleShadows = null;
	bool[] turtlesState = null;

	[Header("Debug")]
	[SerializeField] TextMeshProUGUI debugTextField = null;

	[Header("Animations")]
	[SerializeField] GameObject minigame;
	[SerializeField] GameObject winAnimation;
	[SerializeField] GameObject loseAnimation;

	TurtleMinigameDifficulty difficulty;

	public override void Init(byte usedDifficulty) {
		base.Init(usedDifficulty);
		difficulty = difficultyBase as TurtleMinigameDifficulty;

		short i = difficulty.turtlesCount;
		turtles = new SpriteRendererAnimator2[i];
		turtleShadows = new SpriteRendererAnimator2[i];
		turtlesState = new bool[i];

		turtleSpawnPosRandom.Shuffle();

		while (i > 0) {
			--i;
			int id = i;

			Vector3 pos = turtleSpawnPosRandom[i].position;
			pos.z = 0.0f;

			turtles[i] = Instantiate(turtlePrefab,
				pos,
				Quaternion.identity, minigame.transform)
				.GetComponent<SpriteRendererAnimator2>();

			turtleShadows[i] = Instantiate(turtleShadowPrefab,
				pos,
				Quaternion.identity, minigame.transform)
				.GetComponent<SpriteRendererAnimator2>();

			EventTrigger et = turtles[i].GetComponent<EventTrigger>();
			EventTrigger.Entry en = new EventTrigger.Entry();
			en.callback.AddListener((ed) => OnTutrleClick(id));
			en.eventID = EventTriggerType.PointerDown;
			et.triggers.Add(en);

			turtlesState[i] = false;

			SpriteRenderer sr = turtles[i].GetComponent<SpriteRenderer>();
			SpriteRenderer srShadow = turtleShadows[i].GetComponent<SpriteRenderer>();
			srShadow.flipX = sr.flipX = Random.Range(0, 2) == 1;
		}
	}

	new void Update() {
		base.Update();

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}
	}

	public void OnTutrleClick(int id) {
		if (turtlesState[id])
			return;

		turtlesState[id] = true;

		GameObject t = turtles[id].gameObject;
		SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
		SpriteRenderer srShadow = turtleShadows[id].GetComponent<SpriteRenderer>();
		float moveY = Random.Range(1.0f, 3.0f);
		float moveX = (Random.Range(0, 2) == 1 ? Random.Range(1f, 2f) : Random.Range(-2f, -1f));

		sr.sortingOrder = id + 1;
		LeanTween.moveLocalY(t, t.transform.position.y + moveY, 0.3f)
			.setEase(LeanTweenType.easeOutCubic)
			.setOnComplete(()=> {
				turtleShadows[id].SetSequenceForce(1);
				turtles[id].SetSequenceForce(1);
				srShadow.flipX = sr.flipX = moveX < 0;
				LeanTween.moveLocalY(t, t.transform.position.y - moveY, 0.3f)
				.setEase(LeanTweenType.easeInCubic)
				.setOnComplete(()=> {
					sr.sortingOrder = 0;
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

	protected override void ShowLoseAnimation() {
		debugTextField.text = "Loser, ahahahah";
		minigame.SetActive(false);
		loseAnimation.SetActive(true);
		LeanTween.delayedCall(2.0f, () => {
			base.ShowLoseAnimation();
		});
	}

	protected override void ShowWinAnimation() {
		debugTextField.text = "You win";

		LeanTween.delayedCall(1.0f, () => {
			minigame.SetActive(false);
			winAnimation.SetActive(true);
			LeanTween.delayedCall(2.0f, () => {
				base.ShowWinAnimation();
			});
		});
	}
}
