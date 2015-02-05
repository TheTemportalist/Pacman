using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ObjCreator : MonoBehaviour {

	protected abstract bool isEnabled ();

	protected virtual void init() {}

	public abstract Dictionary<int[], int[]> getPoints ();

	protected abstract void createAtPoint(Vector3 point);

	// Use this for initialization
	public void Start () {
		if (!this.isEnabled ()) return;
		this.init ();
//		int count = 0;
		foreach (KeyValuePair<int[], int[]> points in this.getPoints()) {
			foreach (int x in points.Key) {
				foreach (int y in points.Value) {
					this.createAtPoint(new Vector3(x, y) + new Vector3(.5f, .5f));
//					count += 1;
				}
			}
		}
//		print (count);
	}

}

