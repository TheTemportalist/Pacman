using UnityEngine;
using System.Collections;

public class Inky : Ghost {

	override protected Vector3 getHomePoint() {
		return new Vector3(27.5f, -1.5f);
	}
	
	override protected Vector3 getChaseGoal() {
		Pacman pacman = Objects.getPacmanAttr ();
		Blinky blinky = (Blinky)Objects.getGhost (Objects.find ("blinky"));
		Vector3 pacVec = pacman.getPos () + this.scale (pacman.getNormalDir (), 2);
		Vector3 blinkVec = blinky.getPos ();
		return this.scale(blinkVec - pacVec, 2);
	}

}

