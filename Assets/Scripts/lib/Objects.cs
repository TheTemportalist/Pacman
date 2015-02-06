using UnityEngine;
using System.Collections;

public class Objects {

	public static GameObject find(string name) {
		return GameObject.Find (name);
	}

	public static GameObject getPacman() {
		return Objects.find("pacman");
	}

	public static bool hasPacman() {
		return Objects.getPacman () != null;
	}
	
	public static Pacman getPacmanAttr() {
		return Objects.getPacmanAttr(Objects.getPacman());
	}
	
	public static Pacman getPacmanAttr(GameObject pac) {
		if (pac == null) return null;
		return (Pacman)pac.GetComponent("Pacman");
	}

	public static Ghost getGhost(string name) {
		return (Ghost)Objects.getGhost (Objects.find (name));
	}

	public static Ghost getGhost(GameObject obj) {
		return (Ghost)obj.GetComponent("Ghost");
	}

	public static Component getComp(GameObject obj, string name) {
		return obj.GetComponent(name);
	}

	public static void setMode(Ghost.AI ai) {
		foreach (string name in (new string[]{"blinky", "pinky", "inky", "clyde"})) {
			Objects.getGhost(name).setMode(ai);
		}
		if (ai == Ghost.AI.FRIGHTENED) {
			((Timer)Objects.getComp(Objects.find("maze"), "Timer")).startFright();
		}
	}

}

