using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	private float offset = 0.3f;

	void OnTriggerEnter2D(Collider2D co) {
		Vector3 pos = co.gameObject.transform.position;
		if (pos.x > 0) pos.x = -pos.x + offset;
		else pos.x = -pos.x - offset;
		((MovingEntity)co.gameObject.GetComponent ("MovingEntity")).setPos (pos);
	}

}
