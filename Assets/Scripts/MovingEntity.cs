using UnityEngine;
using System.Collections;

public class MovingEntity : MonoBehaviour {

	// Cast Line from 'next to Pac-Man' to 'Pac-Man'
	public bool isValidDirection(Vector2 dir) {
		// We check the SIDES of the pacman, because checking just the center
		// could result in us being too thick for the current point of entry.
		//		A linecast (in checkCollision) can hit us,
		//		even if the line is right on the edge of a collider.
		if (dir.y == 0) { // along x axis
			Vector2 top = (Vector2)this.transform.position + new Vector2(0f, 0.5f);
			Vector2 bottom = (Vector2)this.transform.position + new Vector2(0f, -0.5f);
			return this.checkDirCollision(top, dir) && this.checkDirCollision(bottom, dir);
		}
		else { // along y axis
			Vector2 left = (Vector2)this.transform.position + new Vector2(-0.5f, 0f);
			Vector2 right = (Vector2)this.transform.position + new Vector2(0.5f, 0f);
			return this.checkDirCollision(left, dir) && this.checkDirCollision(right, dir);
		}
		//return false;
	}
	
	bool checkDirCollision(Vector2 pos, Vector2 dir) {
		return this.checkCollision (pos + dir);
	}

	public bool moveToDir(Vector2 dir, float speed) {
		if (this.isAt ((Vector2) this.transform.position + dir)) return true;
		if (this.isValidDirection (dir)) {
			// This will smoothly move pacman to its destination, based on speed
			Vector2 p = Vector2.MoveTowards(
				this.transform.position, (Vector2)this.transform.position + dir, speed
			);
			// actually move to the calcualted vector
			this.rigidbody2D.MovePosition(p);
			return true;
		}
		return false;
	}

	public bool checkCollision(Vector2 target) {
		// this is a layer mask. 8 is the pacdot layer, so 1 << 8 is the mask.
		// to ignore that layer, we invert the mask
		int layerMask = ~(1 << 8);
		// cast from the target to this object
		// if we hit something else, then the point is behind an object. otherwise, the line hits ourself
		RaycastHit2D hit = Physics2D.Linecast (target, this.transform.position, layerMask);
		// if it hits this collider, than there is nothing in the way
		return (hit.collider == this.collider2D);
	}

	public bool moveTo(Vector2 target, float speed) {
		if (this.isAt (target)) return true;
		if (this.checkCollision (target)) {
			// This will smoothly move pacman to its destination, based on speed
			Vector2 p = Vector2.MoveTowards(this.transform.position, target, speed);
			// actually move to the calcualted vector
			this.rigidbody2D.MovePosition(p);
			return true;
		}
		return false;
	}

	bool isAt(Vector2 target) {
		return (Vector2)this.transform.position == target;
	}


}

