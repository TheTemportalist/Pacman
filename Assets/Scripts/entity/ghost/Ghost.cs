using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Ghost : MovingEntity {

	private enum AI {CHASE, SCATTER, FRIGHTENED};
	
	public float speed = 0.3f;
	public int dotCoundDelay = 0;

	private AI ai;
	private Vector3 goal;

	void Start() {
		this.ai = AI.SCATTER;

	}

	override protected void update() {
		if (this.dotCoundDelay > 0) {

		}
		else if (this.getNext() == Vector3.zero || this.getPos() == this.getNext()) {
			this.findNext();
		} else {
			this.moveToNext (this.speed);
		}
	}

	void findNext() {
		this.findNewGoal ();
		if (this.getNext () == Vector3.zero || Intersects.contains (this.getPos ())) {
			//print ("next tile please");
			double smallestDist = 1000D;
			List<Direction> smallest = new List<Direction>();
			foreach (Direction dir in Direction.getDirections ()) {
				if (dir == Direction.UP && Intersects.nonUpDots.Contains(this.getPos ()))
					continue;
				if (!this.isComingFromDir(dir) && this.isValidDirection(dir.getVec())) {
					double dist = Vector3.Distance(dir.toVec(this.getPos ()), this.goal);
					//print (dir.getName() + " | " + dist);
					if (dist <= smallestDist) {
						if (dist < smallestDist)
							smallest.Clear();
						smallest.Add(dir);
						smallestDist = dist;
					}
				}
			}
			Direction smallestDistanceDir = Direction.RIGHT;
			if (smallest.Contains(Direction.UP)) smallestDistanceDir = Direction.UP;
			else if (smallest.Contains(Direction.LEFT)) smallestDistanceDir = Direction.LEFT;
			else if (smallest.Contains(Direction.DOWN)) smallestDistanceDir = Direction.DOWN;

			//print ("goal = " + this.goal);
			//print ("shortest = " + smallestDistanceDir.getName ());
			//print ("dist = " + smallestDist);

			this.setNext(smallestDistanceDir.toVec(this.getPos()));

		} else {
			if (this.isValidDirection (this.getNormalDir())) {
				this.setNext (this.getPos () + this.getNormalDir ());
			}
			else {
				foreach (Direction dir in Direction.getDirections ()) {
					if (!this.isComingFromDir(dir) && this.isValidDirection(dir.getVec())) {
						this.setNext (dir.toVec(this.getPos ()));
					}
				}
			}
		}
	}

	void findNewGoal() {
		if (this.ai == AI.CHASE) {
		} else if (this.ai == AI.SCATTER) {
			this.goal = this.getHomePoint();
		} else if (this.ai == AI.FRIGHTENED) {
		}
	}

	protected abstract Vector3 getHomePoint ();
	
}
