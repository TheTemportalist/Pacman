using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	private float offset = 0.5f;
	private int midX = 14;
	private float leftX = 0f;
	private float rightX = 28f;

	void OnTriggerEnter2D(Collider2D co) {
		MovingEntity obj = (MovingEntity)Objects.getComp (co.gameObject, "MovingEntity");
		Vector3 pos = obj.getPos ();
		if (pos.x > this.midX) {
			pos.x = this.leftX + this.offset;
		}
		else {
			pos.x = this.rightX - this.offset;
		}
		obj.port (pos);
	}

}
