using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class TurtleActions : MonoBehaviour {
	public TurtleFlipper angle = null;
	public float winAngle = 25f;

	[Header("Refs")]
	[HideInInspector] [SerializeField] SpriteRenderer sr = null;
	[HideInInspector] [SerializeField] SpriteRendererAnimator2 sranim = null;

	private bool isJumping = false;
	private bool isFaling = false;
	private bool isStay = false;

	private float desiredAngle;

#if UNITY_EDITOR
	private void OnValidate() {
		if (sr == null)
			sr = GetComponent<SpriteRenderer>();
		if (sranim == null)
			sranim = GetComponent<SpriteRendererAnimator2>();
	}
#endif

	private void Awake() {
		float rndAng = sr.flipX ? Random.Range(-20f, 60f) : Random.Range(-60f, 20f);
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, rndAng));
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name == "MaxHeight") {
			isJumping = false;
			isFaling = true;

			if (-winAngle <= desiredAngle && desiredAngle <= winAngle) {
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, desiredAngle));
				isStay = true;
				sranim.currSequence = 1;
			}
			else {
				float rndAng = Random.Range(-60f, 20f);
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, rndAng));
				isStay = false;
				sranim.currSequence = 0;
			}
		}

		if (collision.gameObject.name == "MinHeight" && isFaling) {
			isFaling = false;
			isStay = -winAngle <= desiredAngle && desiredAngle <= winAngle;
			if(isStay)
				angle.GameCondition();
		}
	}

	void OnMouseDown() {
		if (gameObject.tag == "Turtle" && !isFaling && !isJumping) {
			isJumping = true;
			desiredAngle = (int)(angle.Arrow.transform.eulerAngles.z);
			if (desiredAngle >= 180)
				desiredAngle -= 360;
			angle.debugTextField.text = $"Desired angle : {desiredAngle} {-winAngle <= desiredAngle && desiredAngle <= winAngle}";
		}
	}


	void Update() {
		if (isJumping) {
			transform.position += new Vector3(0, 14, 0) * Time.deltaTime;
			return;
		}

		if (isFaling) {
			transform.position -= new Vector3(0, 12, 0) * Time.deltaTime;
			return;
		}
	}

	public bool GetWinState() {
		return isStay;
	}
}
