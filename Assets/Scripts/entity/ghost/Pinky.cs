using UnityEngine;
using System.Collections;

public class Pinky : Ghost {

	override protected Vector3 getHomePoint() {
		return new Vector3(2.5f, 33.5f);
	}
	
	override protected Vector3 getChaseGoal() {
		Pacman pacman = Objects.getPacmanAttr ();
		return pacman.getPos() + this.scale(pacman.getNormalDir(), 4);
	}

}

