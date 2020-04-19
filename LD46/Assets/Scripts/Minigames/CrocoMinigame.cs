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

	public void Win() {
		isPlaying = false;
		ShowWinAnimation();
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
    print("Done");
  }

  void Awake()
  {
    WithForeachLoop();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse1))
    {
      flip = !flip;
      Toothbrush.GetComponent<SpriteRenderer>().flipY = flip;
    }
  }
}
