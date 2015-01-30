using UnityEngine;
using System.Collections;

public class Ghost : MovingEntity {
	
	public float speed = 0.3f;

	private Transform start;
	private Transform goal;
	private Vector2[] dirs = new Vector2[]{Vector2.up, Vector2.right, -Vector2.up, -Vector2.right};

	void Start() {
		this.start = this.transform;
		this.goal = this.transform;

		this.setGoal (new Vector2(0f, 3f));

	}

	void FixedUpdate() {
		if (!this.hasHitGoal ()) {
			print ("findnew");
			//this.findNewGoal ();
		} else {
			//print ("move to");
			this.moveTowardsGoal ();
		}

		// Animation
		Vector2 dir = this.goal.position - this.transform.position;
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);
	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pacman")
			Destroy(co.gameObject);
	}

	void setNextGoal() {

	}

	void setGoal(Vector2 pos) {
		this.goal.position = pos;
	}

	bool hasHitGoal() {
		//print (this.transform.position);
		//print (this.goal.position);
		return this.transform.position == this.goal.position;
	}

	void findNewGoal() {
		this.goal.position = this.getRandomDir (1);
	}

	Vector2 getRandomDir(float length) {
		Vector2 dir;
		do {
			dir = this.dirs [Random.Range (0, this.dirs.Length)];
			dir.x *= length;
			dir.y *= length;
		} while (!this.isValidDirection(dir));
		return dir;
	}

	void moveTowardsGoal() {
		print (this.moveTo(this.goal.position, this.speed));
	}


}
