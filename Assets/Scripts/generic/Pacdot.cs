using UnityEngine;
using System.Collections;

public class Pacdot : MonoBehaviour {

	public Pacman.PowerUp powerup = Pacman.PowerUp.NONE;

	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pacman") {
			Objects.getPacmanAttr(co.gameObject).eat(this.gameObject, this.powerup);
		}
	}

}
