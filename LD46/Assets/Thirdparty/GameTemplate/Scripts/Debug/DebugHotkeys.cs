using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class DebugHotkeys : MonoBehaviour {
	CinemachineVirtualCamera cam;

	void Update() {
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(0);
		}
		else if (Input.GetKeyDown(KeyCode.Escape)) {
			QuitGame.QuitApp();
		}
	}
}
