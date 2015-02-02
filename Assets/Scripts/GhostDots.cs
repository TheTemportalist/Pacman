using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostDots : Dots {

	override protected Dictionary<int[], int[]> getPoints () {
		return new Dictionary<int[], int[]>{
			{new int[]{1}, new int[]{28, 3}}, {new int[]{26}, new int[]{28, 3}}
		};
	}
	
	override protected void createAtPoint(Vector3 point) {
		this.createDot (point, Pacman.PowerUp.GHOST);
	}

}

