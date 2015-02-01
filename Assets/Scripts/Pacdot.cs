using UnityEngine;
using System.Collections;

public class Pacdot : MonoBehaviour {

	public Pacman.PowerUp powerup = Pacman.PowerUp.NONE;

	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pacman") {
			if (this.powerup != Pacman.PowerUp.NONE)
				((Pacman)co.gameObject.GetComponent("Pacman")).empower(this.powerup);
			Destroy (this.gameObject);
		}
	}

}
