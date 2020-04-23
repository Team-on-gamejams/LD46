using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {
	[Header("Refs")]
	[SerializeField] Player player = null;
	[SerializeField] CanvasGroup creditscg = null;
	[Space]
	[SerializeField] Image SoundImage = null;
	[SerializeField] Sprite[] SoundImageState = null;

	[Space]
	[SerializeField] CanvasGroup logo = null;
	[SerializeField] ImageAnimator logoAnim = null;
	[SerializeField] CanvasGroup cg = null;
	[SerializeField] Animator anim = null;

#if UNITY_EDITOR
	private void OnValidate() {
		if (cg == null)
			cg = GetComponent<CanvasGroup>();
		if (anim == null)
			anim = GetComponent<Animator>();
	}
#endif

	private void Awake() {
		cg.interactable = cg.blocksRaycasts = false;
		cg.alpha = 0.0f;
		creditscg.interactable = creditscg.blocksRaycasts = false;
		creditscg.alpha = 0.0f;
	}

	private void Start() {
#if UNITY_EDITOR
		logo.interactable = logo.blocksRaycasts = false;
		LeanTweenEx.ChangeCanvasGroupAlpha(logo, 0.0f, 0.2f);
		SoundImage.sprite = SoundImageState[AudioManager.Instance.IsEnabled ? 1 : 0];
		ShowMainMenu();
#else
		player.ScreenState = PlayerScreenState.Cinematic;
		logo.interactable = logo.blocksRaycasts = true;
		logo.alpha = 1.0f;
		LeanTween.delayedCall(logoAnim.GetDuration(), () => {
			logo.interactable = logo.blocksRaycasts = false;
			LeanTweenEx.ChangeCanvasGroupAlpha(logo, 0.0f, 0.2f);
			SoundImage.sprite = SoundImageState[AudioManager.Instance.IsEnabled ? 1 : 0];
			ShowMainMenu();
			anim.Play("IntroAnimation");
		});
#endif
	}

	public void PlayEndAnimation() {
		player.ScreenState = PlayerScreenState.Cinematic;
		anim.Play("EndGameAnimation");
	}

	public void ShowMainMenu() {
		anim.Play("MainMenuIdle");
		player.ScreenState = PlayerScreenState.MainMenu;
		cg.interactable = cg.blocksRaycasts = true;
		LeanTweenEx.ChangeCanvasGroupAlpha(cg, 1.0f, 0.2f);
	}

	public void HideMainMenu() {
		cg.interactable = cg.blocksRaycasts = false;
		LeanTweenEx.ChangeCanvasGroupAlpha(cg, 0.0f, 0.2f);
	}

	public void OnPlayClick() {
		player.StartLoop();
	}

	public void ShowCreditsLong() {
		player.ScreenState = PlayerScreenState.SubMainMenu;
		creditscg.interactable = creditscg.blocksRaycasts = true;
		LeanTweenEx.ChangeCanvasGroupAlpha(creditscg, 1.0f, 0.5f);
	}

	public void OnCreditsClick() {
		player.ScreenState = PlayerScreenState.SubMainMenu;
		creditscg.interactable = creditscg.blocksRaycasts = true;
		LeanTweenEx.ChangeCanvasGroupAlpha(creditscg, 1.0f, 0.2f);
	}

	public void OnCreditsBackClick() {
		if (creditscg.alpha == 0)
			return;
		player.ScreenState = PlayerScreenState.MainMenu;
		creditscg.interactable = creditscg.blocksRaycasts = false;
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
		SoundImage.sprite = SoundImageState[AudioManager.Instance.IsEnabled ? 1 : 0];
	}
}
