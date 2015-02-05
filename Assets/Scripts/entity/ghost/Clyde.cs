using UnityEngine;
using System.Collections;

public class Clyde : Ghost {

	override protected Vector3 getHomePoint() {
		return new Vector3(0.5f, -1.5f);
	}
	
	override protected Vector3 getChaseGoal() {
		Pacman pacman = Objects.getPacmanAttr ();
		if (Vector3.Distance (this.getPos (), pacman.getPos ()) >= 8.0D)
			return pacman.getPos ();
		else
			return this.getHomePoint ();
	}

}

