using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	private float offset = 0.7f;
	private int midX = 14;
	private float leftX = 0f;
	private float rightX = 28f;

	void OnTriggerEnter2D(Collider2D co) {
		Vector3 pos = co.gameObject.transform.position;
		if (pos.x > this.midX) pos.x = this.leftX + this.offset;
		else pos.x = this.rightX - this.offset;
		co.gameObject.transform.position = pos;
	}

}
