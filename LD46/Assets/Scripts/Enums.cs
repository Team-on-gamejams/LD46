using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinigameType {
	None,
	Pandas,
	Bird,
	EggHatch,
	Snake,
	TurtleTap,
	Owl,
	CrabRun,
	Aligator,
}

public enum PlayerScreenState : byte {
	MainMenu,		//Exit on escape
	SubMainMenu,	//To main menu on escape
	Cinematic,      //Skip on escape
	Minigame,       //To main menu on escape
}
