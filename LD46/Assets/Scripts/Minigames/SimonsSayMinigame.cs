﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SimonsSayMinigame : BaseBaseMinigame {
	[Header("Balance")]
	[SerializeField] byte maxSequenceLength = 6;
	[SerializeField] AudioClip[] sounds;
	[SerializeField] SpriteRenderer[] sr;

	[Header("Visual")]
	[SerializeField] Sprite[] hightlightSprites;
	Sprite[] defaultSprites;

	[Header("Debug")]
	[SerializeField] TextMeshProUGUI debugTextField = null;

	List<byte> sequence;
	byte currSequenceId;
	sbyte lastClickId;
	AudioSource currAudio;
	LTDescr currDelayedUp;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			debugTextField.enabled = !debugTextField.enabled;
		}
	}

	public override void Init() {
		base.Init();

		defaultSprites = new Sprite[sr.Length];
		for (byte i = 0; i < defaultSprites.Length; ++i) {
			defaultSprites[i] = sr[i].sprite;
		}

		sequence = new List<byte>(maxSequenceLength);
		ContinueSequence();
	}

	public void OnNoteDown(int id) {
		if (isPlaying) {
			if (lastClickId != -1) {
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
				if (sequence.Count >= maxSequenceLength) {
					ShowWinAnimation();
				}
				else {
					ContinueSequence();
				}
			}
			else {
				debugTextField.text = $"Curr Id: {currSequenceId}/{sequence.Count} Seq len: {sequence.Count}/{maxSequenceLength} CurrNote: {sequence[currSequenceId]}";
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
					}
				});
			});

			allDelay += sounds[sequence[currId]].length;
		}

		debugTextField.text = $"Curr Id: {currSequenceId}/{sequence.Count} Seq len: {sequence.Count}/{maxSequenceLength} CurrNote: {sequence[currSequenceId]}";
	}

	void HightlightButton(int id, Sprite sprite, bool isToSprite) {
		if(isToSprite)
			LeanTweenEx.FadeToSprite(sr[id], sprite, 0.1f);
		else
			LeanTweenEx.FadeFromSprite(sr[id], sprite, 0.1f);
	}

	protected override void ShowLoseAnimation() {
		isPlaying = false;
		debugTextField.text = "Loser, ahahahah";
		LeanTween.delayedCall(1.0f, () => {
			base.ShowLoseAnimation();
		});
	}

	protected override void ShowWinAnimation() {
		isPlaying = false;
		debugTextField.text = "You win";
		LeanTween.delayedCall(1.0f, () => {
			base.ShowWinAnimation();
		});
	}
}
