using UnityEngine;
using System.Collections;

public class Blinky : Ghost {

	override protected Vector3 getHomePoint() {
		return new Vector3(25.5f, 33.5f);
	}

}

