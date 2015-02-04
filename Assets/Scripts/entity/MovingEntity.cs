using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingEntity : MonoBehaviour {

	private Vector3 nextTile;
	private Vector3 dir;

	public Vector3 getPos() {
		return this.transform.position;
	}

	public virtual void setPos(Vector3 pos) {
		this.transform.position = pos;
	}

	void FixedUpdate() {
		this.update ();
		this.dir = this.nextTile - this.getPos ();
		GetComponent<Animator>().SetFloat("DirX", this.dir.x);
		GetComponent<Animator>().SetFloat("DirY", this.dir.y);
	}

	protected void setNext(Vector3 tile) {
		this.nextTile = tile;
	}

	protected Vector3 getNext() {
		return this.nextTile;
	}

	protected Vector3 getNormalDir() {
		return this.dir.normalized;
	}

	protected virtual void update() {}

	// Cast Line from 'next to Pac-Man' to 'Pac-Man'
	public bool isValidDirection(Vector2 dir) {
		// We check the SIDES of the pacman, because checking just the center
		// could result in us being too thick for the current point of entry.
		//		A linecast (in checkCollision) can hit us,
		//		even if the line is right on the edge of a collider.
		return this.checkDirCollision (this.getPos(), dir);
		/* I went back to the line above due to contraction of movable cells
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
		*/
		//return false;
	}
	
	bool checkDirCollision(Vector2 pos, Vector2 dir) {
		return this.checkCollision (pos + dir);
	}

	public bool moveToDir(Vector2 dir, float speed) {
		if (this.isAt (this.getPos() + (Vector3)dir)) return true;
		if (this.isValidDirection (dir)) {
			// This will smoothly move pacman to its destination, based on speed
			Vector2 p = Vector2.MoveTowards(
				this.transform.position, (Vector2)this.getPos() + dir, speed
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

	public bool moveToNext(float speed) {
		return this.moveTo (this.getNext (), speed);
	}

	public bool moveTo(Vector2 target, float speed) {
		if (this.checkCollision (target)) {
			return this.moveTo_do(target, speed);
		}
		return false;
	}

	public bool moveTo_do(Vector2 target, float speed) {
		if (!this.isAt (target)) {
			// This will smoothly move pacman to its destination, based on speed
			Vector2 p = Vector2.MoveTowards (this.transform.position, target, speed);
			// actually move to the calcualted vector
			this.rigidbody2D.MovePosition (p);
			return true;
		}
		return false;
	}

	bool isAt(Vector3 target) {
		return this.getPos() == target;
	}

	public Vector2 scale(Vector2 vec, float scale) {
		return new Vector2(vec.x * scale, vec.y * scale);
	}

	protected bool isComingFromDir(Direction dir) {
		return dir.getVec () == -this.getNormalDir ();
	}

	protected Vector3 getNextByDistance(Vector3 goal) {
		return this.getOrderedClosest (goal).toVec (this.getPos ());
	}

	protected Direction getOrderedClosest(Vector3 goal) {
		List<Direction> validDirections = this.getClosetsToGoalFromPos (goal);
		if (validDirections.Contains (Direction.UP))
			return Direction.UP;
		else if (validDirections.Contains (Direction.LEFT))
			return Direction.LEFT;
		else if (validDirections.Contains (Direction.DOWN))
			return Direction.DOWN;
		else
			return Direction.RIGHT;
	}

	protected List<Direction> getClosetsToGoalFromPos(Vector3 goal) {
		Dictionary<Direction, double> distanceDict = new Dictionary<Direction, double> ();
		foreach (Direction dir in Direction.getDirections ()) {
			distanceDict.Add (dir, Vector3.Distance (dir.toVec (this.getPos ()), goal));
		}
		double largest = 0.0D;
		List<Direction> largestList = new List<Direction> ();
		foreach (KeyValuePair<Direction, double> distance in distanceDict) {
			if (distance.Value >= largest) {
				if (distance.Value > largest)
					largestList.Clear();
				largestList.Add(distance.Key);
				largest = distance.Value;
			}
		}
		return largestList;
	}



}

