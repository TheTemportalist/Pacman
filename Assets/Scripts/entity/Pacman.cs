﻿using UnityEngine;
using System.Collections;

public class Pacman : MovingEntity {

	public enum PowerUp {NONE, GHOST};

	// speed variable to contorl how fast pacman moves
	public float speed = 0.4f;
	// destination varaible, where pacman is going
	Vector3 dest = Vector3.zero;
	private PowerUp powerup = Pacman.PowerUp.NONE;
	private int score = 0;

	// Use this for initialization Called on game start
	void Start () {
		// set the destination to the current position (starting position) of pacman
		this.dest = transform.position;

	}
	
	// Update is called once per frame, relies on FPS
	void Update () {}

	void FixedUpdate() {
		// This will smoothly move pacman to its destination, based on speed
		//Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
		// actually move to the calcualted vector
		//this.rigidbody2D.MovePosition(p);
		this.moveTo (this.dest, this.speed);
		
		// recheck the keys for a new movement
		if (this.getPos() == this.dest) {
			this.checkKeys ();
		}

		// Gets the offset from the destination to the current postion (change in direction)
		Vector2 dir = dest - this.getPos();
		// Sets animator parameters for animation changes
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);

	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co.gameObject.layer == 10) {
			if (this.powerup == Pacman.PowerUp.GHOST) Destroy (co.gameObject);
			else Destroy (this.gameObject);
		}
	}

	public void empower(PowerUp power) {
		this.powerup = power;

	}

	void checkKeys() {
		if (this.checkKey (KeyCode.UpArrow, Vector2.up)) return;
		if (this.checkKey (KeyCode.DownArrow, -Vector2.up)) return;
		if (this.checkKey (KeyCode.RightArrow, Vector2.right)) return;
		if (this.checkKey (KeyCode.LeftArrow, -Vector2.right)) return;
	}

	bool checkKey(KeyCode key, Vector2 dir) {
		if (Input.GetKey (key)) {
			if (isValidDirection (dir)) {
				this.dest = this.getPos() + (Vector3)dir;
				return true;
			}
		}
		return false;
	}

	public void eat(GameObject dot, PowerUp power) {
		this.score += 1;
		if (power != Pacman.PowerUp.NONE)
			this.empower(power);
		Destroy (dot);
	}

}