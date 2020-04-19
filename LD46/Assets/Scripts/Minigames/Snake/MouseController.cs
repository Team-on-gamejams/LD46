using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {
	[Header("Refs")]
	[SerializeField] SnakeMinigame minigame = null;
	[SerializeField] Rigidbody2D rb = null;

	Vector3 mouseCurrLocation, diff;
	Vector3 currVelocity;
	Vector3 force;
	bool isDrag = false;

#if UNITY_EDITOR
	private void OnValidate() {
		if (rb == null)
			rb = GetComponent<Rigidbody2D>();
	}
#endif

	void OnMouseDown() {
		isDrag = true;
	}

	void OnMouseUp() {
		isDrag = false;
	}

	public void FixedUpdate() {
		if (isDrag && minigame.isPlaying) {
			mouseCurrLocation = GameManager.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
			mouseCurrLocation.z = 0.0f;
			diff = mouseCurrLocation - transform.position;
			force = Vector3.SmoothDamp(force, diff.normalized * diff.magnitude * minigame.difficulty.speed, ref currVelocity, 0.05f);
			if (force.magnitude > minigame.difficulty.maxSpeed)
				force = force.normalized * minigame.difficulty.maxSpeed;

			rb.velocity = force;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		minigame.Win();
	}
}
