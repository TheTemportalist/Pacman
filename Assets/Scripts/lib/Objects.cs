using UnityEngine;
using System.Collections;

public class Objects {

	public static GameObject find(string name) {
		return GameObject.Find (name);
	}

	public static GameObject getPacman() {
		return Objects.find("pacman");
	}
	
	public static Pacman getPacmanAttr(GameObject pac) {
		return (Pacman)pac.GetComponent("Pacman");
	}

	public static Pacman getPacmanAttr() {
		return Objects.getPacmanAttr(Objects.getPacman());
	}

}

