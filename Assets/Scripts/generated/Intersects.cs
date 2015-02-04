using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intersects : ObjCreator {

	public bool generate = true;

	override protected bool isEnabled() {
		return this.generate;
	}

	override public Dictionary<int[], int[]> getPoints () {
		return new Dictionary<int[], int[]> (){
			{new int[]{12, 15}, new int[]{1}},
			{new int[]{3, 24}, new int[]{4}},
			{new int[]{6, 9, 18, 21}, new int[]{7}},
			{new int[]{6, 9, 18, 21}, new int[]{10}},
			{new int[]{9, 18}, new int[]{13}},
			{new int[]{6, 9, 18, 21}, new int[]{16}},
			{new int[]{6, 21}, new int[]{22}},
			{new int[]{1, 6, 9, 12, 15, 18, 21, 26}, new int[]{25}},
			{new int[]{6, 21}, new int[]{29}}
		};
	}

	public Dictionary<int[], int[]> getPoints_NonUp () {
		return new Dictionary<int[], int[]> (){
			{new int[]{12, 15}, new int[]{7}},
			{new int[]{12, 15}, new int[]{19}}
		};
	}

	public static List<Vector3> normalDots = new List<Vector3> ();
	public static List<Vector3> nonUpDots = new List<Vector3> ();

	public static bool contains(Vector3 point) {
		return Intersects.normalDots.Contains (point) || Intersects.nonUpDots.Contains (point);
	}

	override protected void init() {
		foreach (KeyValuePair<int[], int[]> points in this.getPoints_NonUp()) {
			foreach (int x in points.Key) {
				foreach (int y in points.Value) {
					Vector3 point = new Vector3(x, y) + new Vector3(.5f, .5f);
					Intersects.nonUpDots.Add (point);
					this.createIntersect(point, IconManager.Icon.DiamondYellow);
				}
			}
		}
	}

	override protected void createAtPoint(Vector3 point) {
		Intersects.normalDots.Add (point);
		this.createIntersect (point, IconManager.Icon.DiamondRed);
	}

	private void createIntersect(Vector3 point, IconManager.Icon icon) {
		GameObject empty = new GameObject ("intersect");
		IconManager.SetIcon (empty, icon);
		empty.transform.parent = this.transform;
		empty.transform.position = point;
	}

}
