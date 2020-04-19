using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrocoMinigame : BaseMinigame {
  public Transform Crocodile;
  public Transform Toothbrush;
  public GameObject FrontCaries;
  [SerializeField] List<Transform> Caries;
  bool flip = false;
  public bool wonState = false;


  public void Win() {
		isPlaying = false;
		ShowWinAnimation();
	}

  private void CheckWonState()
  {
    wonState = true;
    foreach (Transform obj in Caries)
    {
      if (obj.gameObject.active) wonState = false;
    }
  }

	protected override void ShowLoseAnimation() {
		LeanTween.delayedCall(1.0f, () => {
			base.ShowLoseAnimation();
		});
	}

	protected override void ShowWinAnimation() {
		LeanTween.delayedCall(1.0f, () => {
			base.ShowWinAnimation();
		});
	}


  void WithForeachLoop()
  {
    foreach (Transform child in FrontCaries.transform) Caries.Add(child);
  }

  void Awake()
  {
    WithForeachLoop();
  }

  void Update()
  {
    CheckWonState();
    //if (wonState) Win();

    if (Input.GetKeyDown(KeyCode.Mouse1))
    {
      flip = !flip;
      Toothbrush.GetComponent<SpriteRenderer>().flipY = flip;
      if (flip)
      {
        Toothbrush.GetComponent<CapsuleCollider2D>().offset = new Vector2(5, -0.7f);
      }
      else
      {
        Toothbrush.GetComponent<CapsuleCollider2D>().offset = new Vector2(5, 0.5f);
      }
    }
  }
}
