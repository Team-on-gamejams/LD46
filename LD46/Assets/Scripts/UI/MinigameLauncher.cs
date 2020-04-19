using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameLauncher : MonoBehaviour {
	[SerializeField] MinigameType type;
	[SerializeField] Player player;

	[SerializeField] GameObject locked;

	[SerializeField] SpriteRendererAnimator srAnim;
	[SerializeField] ShowTextPopupOnMouseHover popupShower;
	[SerializeField] Button btn;
	string enabledText;

#if UNITY_EDITOR
	private void OnValidate() {
		if (srAnim == null)
			srAnim = GetComponent<SpriteRendererAnimator>();
		if (popupShower == null)
			popupShower = GetComponent<ShowTextPopupOnMouseHover>();
		if (btn == null)
			btn = GetComponent<Button>();
		if (locked == null)
			locked = transform.Find("CloseImg").gameObject;
	}
#endif

	private void Awake() {
		enabledText = popupShower.text;
		Disable();
	}

	public void Enable() {
		btn.interactable = true;
		srAnim.enabled = true;
		popupShower.text = enabledText;
		locked.SetActive(false);
	}

	public void Disable() {
		btn.interactable = false;
		srAnim.enabled = false;
		popupShower.text = "Progress through game\nto unlock";
		locked.SetActive(true);
	}

	public void OnClick() {
		player.PlayMinigame(type);
	}
}
