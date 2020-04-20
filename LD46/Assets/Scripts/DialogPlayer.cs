using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogPlayer : MonoBehaviour {
	string[] dialogs = new string[] {
		"This zoo lost his zookeeper, and now animals suffer.",
		"No visitors for a long time.",
		"Damaged animal enclosures. Sad animals. If nothing changes, the zoo is gonna shut down.",
		"You need to make it beatiful again. You are the new zookepeer. Keep this zoo alive, and take a good care of our animal friends.",
	};
	byte i = 0;

	public TextMeshProUGUI text;

	public void ShowDialog() {
		StartCoroutine(Show(dialogs[i++]));
	}

	public IEnumerator Show(string txt) {
		for (int i = 0; i < txt.Length; ++i) {
			text.text = txt.Substring(0, i);
			yield return new WaitForSeconds(0.05f);
			//yield return null;
		}
	}
}
