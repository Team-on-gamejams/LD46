using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {
	[Header("Refs")]
	[SerializeField] Player player;
	[SerializeField] CanvasGroup creditscg;

	[SerializeField] CanvasGroup logo;
	[SerializeField] ImageAnimator logoAnim;

	CanvasGroup cg;

#if UNITY_EDITOR
	private void OnValidate() {
		if (cg == null)
			cg = GetComponent<CanvasGroup>();
	}
#endif

	private void Awake() {
		cg.interactable = cg.blocksRaycasts = false;
		creditscg.interactable = creditscg.blocksRaycasts = false;
		cg.alpha = 0.0f;
		creditscg.alpha = 0.0f;
	}

	private void Start() {
		logo.interactable = logo.blocksRaycasts = false;
		logo.alpha = 1.0f;
		LeanTween.delayedCall(logoAnim.GetDuration(), () => {
			logo.interactable = logo.blocksRaycasts = false;
			LeanTweenEx.ChangeCanvasGroupAlpha(logo, 0.0f, 0.2f);
			cg.interactable = cg.blocksRaycasts = true;
			LeanTweenEx.ChangeCanvasGroupAlpha(cg, 1.0f, 0.2f);
		});
	}

	public void OnPlayClick() {
		player.StartLoop();
		LeanTweenEx.ChangeCanvasGroupAlpha(cg, 0.0f, 0.2f);
	}

	public void OnCreditsClick() {
		cg.interactable = cg.blocksRaycasts = false;
		creditscg.interactable = creditscg.blocksRaycasts = true;
		LeanTweenEx.ChangeCanvasGroupAlpha(cg, 0.0f, 0.2f);
		LeanTweenEx.ChangeCanvasGroupAlpha(creditscg, 1.0f, 0.2f);
	}

	public void OnCreditsBackClick() {
		cg.interactable = cg.blocksRaycasts = true;
		creditscg.interactable = creditscg.blocksRaycasts = false;
		LeanTweenEx.ChangeCanvasGroupAlpha(cg, 1.0f, 0.2f);
		LeanTweenEx.ChangeCanvasGroupAlpha(creditscg, 0.0f, 0.2f);
	}

	public void OnExitClick() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void OnSoundClick() {
		AudioManager.Instance.IsEnabled = !AudioManager.Instance.IsEnabled;
	}
}
