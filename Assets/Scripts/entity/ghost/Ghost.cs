using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Ghost : MovingEntity {

	public enum AI {CHASE, SCATTER, FRIGHTENED};
	private static Vector3 starter = new Vector3(14.5f, 19.5f);
	private static Vector3 boxUpLeft = new Vector3(10.5f, 18.5f);
	private static Vector3 boxDownRight = new Vector3(17.5f, 14.5f);
	private static Vector3 boxCenter = new Vector3(14.5f, 16.5f);
	
	public float speed = 0.3f;
	public int dotCoundDelay = 0;

	private AI ai;
	private Vector3 goal;

	private bool eaten = false;

	void Start() {
		this.ai = AI.CHASE;
		this.setNext (this.getPos ());

	}

	override protected void update() {
		if (this.dotCoundDelay > 0 && Objects.getPacmanAttr ().getScore () < this.dotCoundDelay) {
			return;
		}
		if (this.getPos() == this.getNext()) {
			this.findNext();
		} else {
			this.moveToNext (this.speed);
		}
		if (this.isEaten () && this.isInBox()) {
			this.setEaten (false);
		}
	}

	void findNext() {
		this.findNewGoal ();
		// check if the current position and the target tile are the same (i have reached destination)
		if (this.getNext () == this.getPos ()) {
			// create a variable to store the shortest distance of all directions
			double shortestDistance = 10000D;
			List<Direction> shortestDirections = new List<Direction>();
			foreach (Direction dir in Direction.getDirections()) {
				// if the direction is up when we cant go up
				if (dir == Direction.UP && Intersects.nonUpDots.Contains(this.getPos())) continue;
				// if the direction is where we are coming from
				if (this.isComingFromDir(dir)) continue;
				// check to make sure we dont go back IN the box
				// valid in box point if in box or is eaten
				if (this.pointInBox(dir.toVec(this.getPos()))) {
					if (!this.isEaten() && !this.isInBox()) {
						//print ("skip " + dir.getName());
						continue;
					}
				}
				// if the direction is valid
				if (this.isValidDirection(dir.getVec())) {
					// find the distance to goal from target
					double distance = Vector3.Distance(dir.toVec(this.getPos()), this.goal);
					// if the distance from target to goal is smaller than last calculated
					if (distance <= shortestDistance) {
						// if the distance is shorter than
						if (distance < shortestDistance) {
							shortestDistance = distance;
							shortestDirections.Clear();
						}
						shortestDirections.Add(dir);
					}
				}
			}
			// we have a list of valid travel directions, now get the one we should use
			Direction directionToTravel = Direction.RIGHT;
			if (shortestDirections.Contains(Direction.UP)) directionToTravel = Direction.UP;
			else if (shortestDirections.Contains(Direction.LEFT)) directionToTravel = Direction.LEFT;
			else if (shortestDirections.Contains(Direction.DOWN)) directionToTravel = Direction.DOWN;

			this.setNext(directionToTravel.toVec(this.getPos()));
		}
	}

	void findNewGoal() {
		if (this.isInBox ()) {
			this.goal = Ghost.starter;
		} else if (this.isEaten ()) {
			this.goal = Ghost.boxCenter;
		}
		else if (this.ai == AI.CHASE) {
			this.goal = this.getChaseGoal();
		} else if (this.ai == AI.SCATTER) {
			this.goal = this.getHomePoint();
		} else if (this.ai == AI.FRIGHTENED) {
		}
	}

	public void setMode(AI ai) {

	}

	protected abstract Vector3 getHomePoint ();

	protected abstract Vector3 getChaseGoal ();

	override protected void onDirChanged() {
		Transform eyes = this.transform.FindChild ("eyes");
		if (eyes != null) {
			eyes.gameObject.GetComponent<Animator>().SetFloat("DirX", this.getDir ().x);
			eyes.gameObject.GetComponent<Animator>().SetFloat("DirY", this.getDir ().y);
		}
	}

	private bool pointInBox(Vector3 point) {
		return Ghost.boxUpLeft.x <= point.x && point.x <= Ghost.boxDownRight.x &&
			Ghost.boxDownRight.y <= point.y && point.y <= Ghost.boxUpLeft.y;
	}

	private bool isInBox() {
		return this.pointInBox (this.getPos ());
	}

	public bool isEaten() {
		return this.eaten;
	}

	public void setEaten(bool eaten) {
		this.eaten = eaten;
		if (this.eaten) this.renderer.enabled = false;
		else this.renderer.enabled = true;
	}

	override public void port(Vector3 newPos) {
		base.port (newPos);
		this.setNext (newPos);
	}

}
