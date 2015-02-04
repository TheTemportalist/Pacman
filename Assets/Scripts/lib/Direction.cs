using UnityEngine;
using System.Collections;

public class Direction {

	public static readonly Direction UP = new Direction ("up", Vector3.up);
	public static readonly Direction LEFT = new Direction ("left", -Vector3.right);
	public static readonly Direction DOWN = new Direction ("down", -Vector3.up);
	public static readonly Direction RIGHT = new Direction ("right", Vector3.right);

	public static Direction[] getDirections() {
		return new Direction[]{UP, LEFT, DOWN, RIGHT};
	}

	private string name;
	private Vector3 dirVec;

	private Direction (string name, Vector3 vec) {
		this.name = name;
		this.dirVec = vec;
	}

	public string getName() {
		return this.name;
	}

	public Vector3 getVec() {
		return this.dirVec;
	}

	public Vector3 toVec(Vector3 inital) {
		return inital + this.dirVec;
	}

}

