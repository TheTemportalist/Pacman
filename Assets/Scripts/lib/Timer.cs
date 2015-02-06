using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour {

	private KeyValuePair<Ghost.AI, int>[] modes = new KeyValuePair<Ghost.AI, int>[]{
		new KeyValuePair<Ghost.AI, int>(Ghost.AI.SCATTER, 7),
		new KeyValuePair<Ghost.AI, int>(Ghost.AI.CHASE, 20),
		new KeyValuePair<Ghost.AI, int>(Ghost.AI.SCATTER, 7),
		new KeyValuePair<Ghost.AI, int>(Ghost.AI.CHASE, 20),
		new KeyValuePair<Ghost.AI, int>(Ghost.AI.SCATTER, 5),
		new KeyValuePair<Ghost.AI, int>(Ghost.AI.CHASE, 20),
		new KeyValuePair<Ghost.AI, int>(Ghost.AI.SCATTER, 5),
		new KeyValuePair<Ghost.AI, int>(Ghost.AI.CHASE, -1)
	};
	private int currentMode = 0;
	private bool runningFright = false;
	private Ghost.AI mode;
	private int timer = 0;

	void Awake() {
		StartCoroutine (updateMode());
	}

	IEnumerator updateMode() {
		while (this.runningFright) {
			yield return new WaitForFixedUpdate();	
		}
		this.refreshModes ();
		//print ("wait for " + this.modes [this.currentMode].Value);
		this.mode = this.modes [this.currentMode].Key;
		this.timer = this.modes [this.currentMode].Value;
		yield return new WaitForSeconds(this.modes[this.currentMode].Value);
		++this.currentMode;
		StartCoroutine (updateMode());
	}
	
	private void refreshModes() {
		Objects.setMode (this.modes [this.currentMode].Key);
	}

	public void startFright() {
		StartCoroutine (updateFright ());
	}

	IEnumerator updateFright() {
		this.runningFright = true;
		this.mode = Ghost.AI.FRIGHTENED;
		this.timer = 6;
		yield return new WaitForSeconds (6);
		this.runningFright = false;
		Objects.getPacmanAttr ().empower (Pacman.PowerUp.NONE);
	}

	public KeyValuePair<Ghost.AI, int> getCurrentState() {
		return new KeyValuePair<Ghost.AI, int>(this.mode, this.timer);
	}

}
