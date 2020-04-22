using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SimonsSayMinigame : BaseBaseMinigame {
	[Header("Refs")]
	[SerializeField] AudioClip[] sounds = null;
	[SerializeField] SpriteRenderer[] sr = null;
	[SerializeField] SpriteRendererAnimator2 sranim = null;
	[SerializeField] GameObject winAnimation = null;
	[SerializeField] GameObject loseAnimation = null;
	[SerializeField] TextMeshProUGUI stateTextField = null;

	[Header("Visual")]
	[SerializeField] Sprite[] hightlightSprites = null;
	Sprite[] defaultSprites;

	[Header("Debug")]
	[SerializeField] TextMeshProUGUI debugTextField = null;

	List<byte> sequence;
	byte currSequenceId;
	sbyte lastClickId;
	AudioSource currAudio;
	LTDescr currDelayedUp;

	BirdMinigameDifficulty difficulty;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}
	}

	public override void Init(byte usedDifficulty) {
		base.Init(usedDifficulty);

		defaultSprites = new Sprite[sr.Length];
		for (byte i = 0; i < defaultSprites.Length; ++i) {
			defaultSprites[i] = sr[i].sprite;
		}

		difficulty = (difficultyBase as BirdMinigameDifficulty);
		sequence = new List<byte>(difficulty.maxSequenceLength);

		LeanTween.delayedCall(difficultyBase.delayBeforePlay, () => { 
			ContinueSequence();
		});
	}

	public void OnNoteDown(int id) {
		if (isPlaying) {
			if (lastClickId != -1) {
				if(currAudio)
					currAudio.volume = 0.0f;
				LeanTween.cancel(currDelayedUp.uniqueId, false);
				NoteUpAction();
				currDelayedUp = null;
			}

			lastClickId = (sbyte)id;

			if (isPlaying) {
				HightlightButton(id, hightlightSprites[id], true);

				currAudio = AudioManager.Instance.Play(sounds[id], channel: AudioManager.AudioChannel.Sound);
				currDelayedUp = LeanTween.delayedCall(sounds[id].length, () => {
					NoteUpAction();
					currDelayedUp = null;
				});

				if (currSequenceId == sequence.Count - 1 || sequence[currSequenceId] != lastClickId)
					isPlaying = false;
			}
		}
	}

	void NoteUpAction() {
		if (sequence[currSequenceId] == lastClickId) {
			++currSequenceId;
			HightlightButton(lastClickId, defaultSprites[lastClickId], false);

			if (currSequenceId == sequence.Count) {
				if (sequence.Count >= difficulty.maxSequenceLength) {
					ShowWinAnimation();
				}
				else {
					ContinueSequence();
				}
			}
			else {
				debugTextField.text = $"Curr Id: {currSequenceId}/{sequence.Count} Seq len: {sequence.Count}/{difficulty.maxSequenceLength} CurrNote: {sequence[currSequenceId]}";
			}

		}
		else {
			ShowLoseAnimation();
		}

		lastClickId = -1;
	}

	void ContinueSequence() {
		const float additionalDelay = 0.1f;
		float allDelay = additionalDelay;

		stateTextField.text = "Listen and memorize bird sounds";

		lastClickId = -1;
		currSequenceId = 0;
		isPlaying = false;
		sequence.Add((byte)Random.Range(0, sounds.Length));

		for (byte i = 0; i < sequence.Count; ++i) {
			byte currId = i;

			allDelay += additionalDelay * currId;

			LeanTween.delayedCall(allDelay, () => {
				HightlightButton(sequence[currId], hightlightSprites[sequence[currId]], true);
				AudioManager.Instance.Play(sounds[sequence[currId]], channel: AudioManager.AudioChannel.Sound);

				LeanTween.delayedCall(sounds[sequence[currId]].length, () => {
					HightlightButton(sequence[currId], defaultSprites[sequence[currId]], false);
					if (currId == sequence.Count - 1) {
						isPlaying = true;
						stateTextField.text = "Repeat bird sounds";
					}
				});
			});

			allDelay += sounds[sequence[currId]].length;
		}

		debugTextField.text = $"Curr Id: {currSequenceId}/{sequence.Count} Seq len: {sequence.Count}/{difficulty.maxSequenceLength} CurrNote: {sequence[currSequenceId]}";
	}

	void HightlightButton(int id, Sprite sprite, bool isToSprite) {
		if(isToSprite)
			LeanTweenEx.FadeToSprite(sr[id], sprite, 0.1f);
		else
			LeanTweenEx.FadeFromSprite(sr[id], sprite, 0.1f);
	}

	protected override void ShowLoseAnimation() {
		isPlaying = false;
		stateTextField.text = "";
		debugTextField.text = "Loser, ahahahah";
		sranim.SetSequenceForce(1);

		loseAnimation.SetActive(true);

		LeanTween.delayedCall(2.50f, () => {
			base.ShowLoseAnimation();
		});
	}

	protected override void ShowWinAnimation() {
		isPlaying = false;
		stateTextField.text = "";
		debugTextField.text = "You win";
		sranim.SetSequenceForce(1);

		winAnimation.SetActive(true);

		LeanTween.delayedCall(2.5f, () => {
			base.ShowWinAnimation();
		});
	}
}
