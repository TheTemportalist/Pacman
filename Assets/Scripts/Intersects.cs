using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intersects : ObjCreator {

	public bool generate = true;

	override protected bool isEnabled() {
		return this.generate;
	}

	override protected Dictionary<int[], int[]> getPoints () {
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

	override protected void createAtPoint(Vector3 point) {
		GameObject empty = new GameObject ("intersect");
		IconManager.SetIcon (empty, IconManager.Icon.DiamondRed);
		empty.transform.parent = this.transform;
		empty.transform.position = point;
	}

}
