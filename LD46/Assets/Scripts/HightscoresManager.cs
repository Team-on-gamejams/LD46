using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;
using System;

public class HightscoresManager : MonoBehaviour {
	public static string playfabId = string.Empty;

	private void Start() {
		Login();
	}

	public void Login() {
		Debug.Log("Playfab login start");
		PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest() { CustomId = DateTime.Now.Ticks.ToString() + UnityEngine.Random.Range(0, 100).ToString(), CreateAccount = true }, OnLoginSuccess, OnLoginFailure);
	}

	private void OnLoginSuccess(LoginResult result) {
		Debug.Log("Playfab login success");
		playfabId = result.PlayFabId;
	}

	private void OnLoginFailure(PlayFabError error) {
		Debug.Log($"Playfab login error: {error.GenerateErrorReport()}");
	}
}
