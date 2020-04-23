using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocoBrush : MonoBehaviour {
	[NonSerialized] public float force;
	[NonSerialized] public float alphaChangePerEnter;
	[NonSerialized] public float alphaChangePerUnit;

	[SerializeField] CrocoMinigame minigame = null;
	Rigidbody2D rb;

	private bool isDragging;
	private Vector2 mousePosition;
	private Vector3 prevPos, currPos;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void OnMouseDown() {
		isDragging = true;
	}

	public void OnMouseUp() {
		isDragging = false;
		rb.velocity = new Vector2();
	}

	public void OnTriggerEnter2D(Collider2D colide) {
		if (colide.gameObject.name.Contains("Caries")) {
			colide.GetComponent<SpriteRenderer>().color = colide.GetComponent<SpriteRenderer>().color - new Color(0, 0, 0, alphaChangePerEnter);

			if (colide.GetComponent<SpriteRenderer>().color.a <= 0.3f) {
				colide.gameObject.SetActive(false);
				minigame.CheckWonState();
			}
		}
	}

	private void OnTriggerStay2D(Collider2D colide) {
		if (colide.gameObject.name.Contains("Caries")) {
			colide.GetComponent<SpriteRenderer>().color = colide.GetComponent<SpriteRenderer>().color - new Color(0, 0, 0, alphaChangePerUnit * (currPos - prevPos).magnitude);

			if (colide.GetComponent<SpriteRenderer>().color.a <= 0.3f) {
				colide.gameObject.SetActive(false);
				minigame.CheckWonState();
			}
		}
	}

	public void FixedUpdate() {
		prevPos = currPos;
		currPos = transform.position;

		if (isDragging && minigame.isPlaying) {
			mousePosition = GameManager.Instance.Camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			rb.velocity = mousePosition * force;
		}
	}
}
