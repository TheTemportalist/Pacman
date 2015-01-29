using UnityEngine;
using System.Collections;

public class Pacman : MonoBehaviour {

	// speed variable to contorl how fast pacman moves
	public float speed = 0.4f;
	// destination varaible, where pacman is going
	Vector2 dest = Vector2.zero;

	// Use this for initialization Called on game start
	void Start () {
		// set the destination to the current position (starting position) of pacman
		this.dest = transform.position;

	}
	
	// Update is called once per frame, relies on FPS
	void Update () {}

	void FixedUpdate() {
		// This will smoothly move pacman to its destination, based on speed
		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		// actually move to the calcualted vector
		this.rigidbody2D.MovePosition(p);
		
		// recheck the keys for a new movement
		if ((Vector2)this.transform.position == this.dest) {
			this.checkKeys ();
		}

		// Gets the offset from the destination to the current postion (change in direction)
		Vector2 dir = dest - (Vector2)transform.position;
		// Sets animator parameters for animation changes
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);

	}
	
	// Cast Line from 'next to Pac-Man' to 'Pac-Man'
	bool isValidDirection(Vector2 dir) {
		// We check the SIDES of the pacman, because checking just the center
		// could result in us being too thick for the current point of entry.
		//		A linecast (in checkCollision) can hit us,
		//		even if the line is right on the edge of a collider.
		if (dir.y == 0) { // along x axis
			Vector2 top = (Vector2)this.transform.position + new Vector2(0f, 0.5f);
			Vector2 bottom = (Vector2)this.transform.position + new Vector2(0f, -0.5f);
			return this.checkCollision(top, dir) && this.checkCollision(bottom, dir);
		}
		else { // along y axis
			Vector2 left = (Vector2)this.transform.position + new Vector2(-0.5f, 0f);
			Vector2 right = (Vector2)this.transform.position + new Vector2(0.5f, 0f);
			return this.checkCollision(left, dir) && this.checkCollision(right, dir);
		}
		return false;
	}

	bool checkCollision(Vector2 pos, Vector2 dir) {
		// this is a layer mask. 8 is the pacdot layer, so 1 << 8 is the mask.
		// to ignore that layer, we invert the mask
		int layerMask = ~(1 << 8);
		// cast from the target to this object
		// if we hit something else, then the point is behind an object. otherwise, the line hits ourself
		RaycastHit2D hit = Physics2D.Linecast (pos + dir, pos, layerMask);
		// if it hits this collider, than there is nothing in the way
		return (hit.collider == this.collider2D);
	}

	void checkKeys() {
		if (this.checkKey (KeyCode.UpArrow, Vector2.up)) return;
		if (this.checkKey (KeyCode.DownArrow, -Vector2.up)) return;
		if (this.checkKey (KeyCode.RightArrow, Vector2.right)) return;
		if (this.checkKey (KeyCode.LeftArrow, -Vector2.right)) return;
	}

	bool checkKey(KeyCode key, Vector2 dir) {
		if (Input.GetKey (key)) {
			//print (isValidDirection (dir));
			if (isValidDirection (dir)) {
				this.dest = (Vector2)transform.position + dir;
				//print (this.dest);
				return true;
			}
		}
		return false;
	}

}
