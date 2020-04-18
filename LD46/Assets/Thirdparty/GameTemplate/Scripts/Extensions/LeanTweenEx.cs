using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class LeanTweenEx
{
	public static LTDescr ChangeSpriteAlpha(SpriteRenderer rend, float alpha, float animTime) {
		return LeanTween.value(rend.gameObject, rend.color.a, alpha, animTime)
			.setOnUpdate((float a) => {
				Color c = rend.color;
				c.a = a;
				rend.color = c;
			});
	}

	public static LTDescr ChangeSpriteColor(SpriteRenderer rend, Color color, float animTime) {
		return LeanTween.value(rend.gameObject, rend.color, color, animTime)
			.setOnUpdate((Color c) => {
				rend.color = c;
			});
	}


	public static LTDescr ChangeCanvasGroupAlpha(CanvasGroup canvasGroup, float alpha, float animTime)
	{
		return LeanTween.value(canvasGroup.gameObject, canvasGroup.alpha, alpha, animTime)
			.setOnUpdate((float a) => {
				canvasGroup.alpha = a;
			});
	}
	
	public static LTDescr ChangeTextAlpha(TextMeshProUGUI text, float alpha, float animTime)
	{
		return LeanTween.value(text.gameObject, text.alpha, alpha, animTime)
			.setOnUpdate((float a) => {
				text.alpha = a;
			});
	}

	public static void FadeImage(Image imageOrig, Sprite newSprite, float time)
	{
		GameObject fadedImage = new GameObject("fadedImage");

		Image image = fadedImage.AddComponent<Image>();
		image.sprite = newSprite;
		image.color = new Color(1, 1, 1, 0);

		RectTransform trans = fadedImage.GetComponent<RectTransform>();
		trans.transform.SetParent(imageOrig.transform);
		trans.transform.SetAsFirstSibling();
		trans.localScale = imageOrig.rectTransform.localScale;
		trans.localPosition = Vector3.zero;
		trans.sizeDelta = new Vector2(imageOrig.rectTransform.rect.width, imageOrig.rectTransform.rect.height);

		LeanTween.alpha(trans, 1.0f, time)
			.setOnComplete(() => {
				GameObject.Destroy(fadedImage);
				imageOrig.sprite = newSprite;
			});
	}

	public static void FadeToSprite(SpriteRenderer orig, Sprite newSprite, float time) {
		GameObject go = new GameObject("newSprite");

		SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
		sr.sprite = newSprite;
		sr.color = new Color(1, 1, 1, 0);
		sr.flipX = orig.flipX;
		sr.flipY = orig.flipY;

		Transform trans = go.GetComponent<Transform>();
		trans.transform.SetParent(orig.transform);
		trans.localPosition = Vector3.zero;
		trans.localScale = Vector3.one;

		LeanTween.value(go, 0.0f, 1.0f, time)
			.setOnUpdate((float a) => {
				Color c = sr.color;
				c.a = a;
				sr.color = c;
			})
			.setOnComplete(() => {
				GameObject.Destroy(go);
				orig.sprite = newSprite;
			});
	}

	public static void FadeFromSprite(SpriteRenderer orig, Sprite newSprite, float time) {
		GameObject go = new GameObject("newSprite");

		SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
		sr.sprite = orig.sprite;
		sr.color = new Color(1, 1, 1, 1);
		sr.flipX = orig.flipX;
		sr.flipY = orig.flipY;

		Transform trans = go.GetComponent<Transform>();
		trans.transform.SetParent(orig.transform);
		trans.localPosition = Vector3.zero;
		trans.localScale = Vector3.one;

		orig.sprite = newSprite;

		LeanTween.value(go, 1.0f, 0.0f, time)
			.setOnUpdate((float a) => {
				Color c = sr.color;
				c.a = a;
				sr.color = c;
			})
			.setOnComplete(() => {
				GameObject.Destroy(go);
			});
	}

	public static void InvokeNextFrame(GameObject go, Action action)
	{
		go.GetComponent<MonoBehaviour>().StartCoroutine(InvokeNextFrameInner(action));
	}

	static IEnumerator InvokeNextFrameInner(Action action)
	{
		yield return null;
		action?.Invoke();
	}
}