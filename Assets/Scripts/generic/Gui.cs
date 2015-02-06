using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gui : MonoBehaviour {

	public bool isDebug = false;

	void OnGUI() {
		int score = -1;
		if (Objects.hasPacman ()) score = Objects.getPacmanAttr ().getScore ();
		GUI.Label (new Rect(0, 225, 100, 20), "Score: " + score);
		GUI.Label (new Rect(0, 240, 100, 20), "Lives: ");

		if (isDebug) {
			KeyValuePair<Ghost.AI, int> modeTimer =
				((Timer)Objects.getComp (Objects.find ("maze"), "Timer")).getCurrentState ();
			GUI.Label (new Rect (120, 225, 100, 20), modeTimer.Key + " - " + modeTimer.Value);
		}
	}

}
