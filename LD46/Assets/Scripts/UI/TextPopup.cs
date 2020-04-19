using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour {
	[SerializeField] RectTransform rt = null;
	[SerializeField] TextMeshProUGUI textField = null;
	[SerializeField] CanvasGroup cg = null;
	[SerializeField] RectTransform arrow = null;

	[HideInInspector] public bool isUp = true;

	public void Show(string text, Vector3 pos) {
		textField.text = text;
		UpdatePos(pos);
		LeanTweenEx.ChangeCanvasGroupAlpha(cg, 1.0f, 0.2f);

		if (isUp) {
			rt.rotation = Quaternion.Euler(0, 0, 0);
			textField.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else {
			rt.rotation = Quaternion.Euler(0, 0, 180);
			textField.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}

	public void Hide() {
		LeanTweenEx.ChangeCanvasGroupAlpha(cg, 0.0f, 0.2f);
	}

	public void UpdatePos(Vector3 pos) {
		if (isUp)
			rt.anchoredPosition = pos;// + new Vector3(0, textField.rectTransform.sizeDelta.y / 2 + arrow.sizeDelta.y / 2);
		else
			rt.anchoredPosition = pos;// - new Vector3(0, textField.rectTransform.sizeDelta.y / 2 + arrow.sizeDelta.y / 2);
	}
}
