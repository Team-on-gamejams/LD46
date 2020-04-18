using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MenuBase {
	public void Play() {
		SceneLoader.Instance.LoadScene("TeamScene2D", true, true);

	}

	public void Load() {
		SceneLoader.Instance.LoadScene("TeamScene2D", true, true);
	}
}
