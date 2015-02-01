using UnityEngine;
using System.Collections;

public class Ghost : MovingEntity {
	
	public float speed = 0.3f;

	private Vector2 goal;
	private Vector2[] dirs = new Vector2[]{Vector2.up, Vector2.right, -Vector2.up, -Vector2.right};

	void Start() {
		this.setGoal (this.scale (Vector2.up, 3));
	}

	void FixedUpdate() {
		if (this.hasHitGoal ()) {
			//print ("find new");
			//this.findNewGoal ();
		} else {
			//print ("move to");
			//this.moveTo_do(this.goal, this.speed);
		}

		// Animation
		Vector2 dir = this.goal - (Vector2)this.transform.position;
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);
	}

	bool hasHitGoal() {
		return (Vector2)this.transform.position == this.goal;
	}

	void findNewGoal() {
		Vector2 dir = this.getRandomDir();
		Collider2D col = null;
		float scale = 0;
		do {
			scale += 0.5f;
			col = this.getCollision((Vector2)this.transform.position + this.scale(dir, scale));
		//	print (col.gameObject.layer);
		}
		while(col.gameObject.layer != 0);
		this.setGoal(this.scale (dir, Random.Range(1, scale - 1)));


		//this.goal.position = (Vector2)this.transform.position + this.getRandomDir (1);
	}

	void setGoal(Vector2 offset) {
		this.goal = this.getPos() + offset;
	}

	Collider2D getCollision(Vector2 target) {
		// this is a layer mask. 8 is the pacdot layer, so 1 << 8 is the mask.
		// to ignore that layer, we invert the mask
		int layerMask = ~(1 << 8);
		// cast from the target to this object
		// if we hit something else, then the point is behind an object. otherwise, the line hits ourself
		RaycastHit2D hit = Physics2D.Linecast (target, this.transform.position, layerMask);
		return hit.collider;
	}

	Vector2 getRandomDir() {
		Vector2 dir;
		do {
			dir = this.dirs [Random.Range (0, this.dirs.Length)];
		} while (!this.isValidDirection(dir));
		return dir;
	}


}
