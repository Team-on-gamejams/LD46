using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ShowTextPopupOnMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[Header("Data")]
	[Multiline] public string text = "";
	[SerializeField] bool isUp = true;

	[Header("Refs")]
	[SerializeField] TextPopup textPopupPrefab = null;
	[SerializeField] RectTransform rc = null;
	TextPopup textPopup = null;

	bool isMouseHover = false;

#if UNITY_EDITOR
	private void OnValidate() {
		if (rc == null)
			rc = GetComponent<RectTransform>();
	}
#endif

	private void Update() {
		if (isMouseHover) {
			UpdatePos();
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		isMouseHover = false;
		textPopup.Hide();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		isMouseHover = true;

		if (textPopup == null) {
			textPopup = Instantiate(textPopupPrefab, transform);
			textPopup.isUp = isUp;
			textPopup.transform.localScale = Vector3.one / transform.localScale.x;
		}

		UpdatePos();
	}

	void UpdatePos() {
		if (isUp) {
			Vector3 pos = new Vector3(Input.mousePosition.x - rc.position.x, rc.rect.height);
			textPopup.Show(text, pos);
		}
		else {
			Vector3 pos = new Vector3(Input.mousePosition.x - rc.position.x, 0);
			textPopup.Show(text, pos);
		}
	}
}
