using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	Sprite dotSprite;

	Dictionary<int, int[]> dots = new Dictionary<int, int[]>(){
		{14, new int[]{
			-12, -11, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13
		}},
		{13, new int[]{-12, -7, -1, 2, 8, 13}},
		{12, new int[]{-12, -7, -1, 0, 1, 2, 8, 13}},
		{11, new int[]{-12, -7, -1, 2, 8, 13}},
		{10, new int[]{
			-12, -11, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13
		}},
		{9, new int[] {
			-12, -7, -4, 5, 8, 13
		}},
		{8, new int[] {
			-12, -7, -4, 5, 8, 13
		}},
		{7, new int[] {
			-12, -11, -10, -9, -8, -7, -4, -3, -2, -1, 2, 3, 4, 5, 8, 9, 10, 11, 12, 13
		}},
		{6, new int[] {
			-10, -7, -1, 2, 8, 11
		}},
		{5, new int[] {
			-10, -7, -1, 2, 8, 11
		}},
		{4, new int[] {
			-12, -11, -10, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 11, 12, 13
		}},
		{3, new int[] {
			-12, -7, -4, 5, 8, 13
		}},
		{2, new int[] {
			-12, -7, -4, 5, 8, 13
		}},
		{1, new int[] {
			-12, -11, -10, -9, -8, -7, -4, 5, 8, 9, 10, 11, 12, 13
		}},
		{0, new int[] {
			-12, -7, -4, 5, 8, 13
		}},
		{-1, new int[] {
			-12, -7, -4, 5, 8, 13
		}},
		{-2, new int[] {
			-12, -11, -10, -7, -4, 5, 8, 11, 12, 13
		}},
		{-3, new int[] {
			-10, -7, -4, 5, 8, 11
		}},
		{-4, new int[] {
			-10, -7, -4, 5, 8, 11
		}},
		{-5, new int[] {
			-12, -11, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13
		}},
		{-6, new int[] {
			-12, -7, -1, 2, 8, 13
		}},
		{-7, new int[] {
			-12, -7, -1, 2, 8, 13
		}},
		{-8, new int[] {
			-12, -11, -10, -7, -6, -5, -4, -3, -2, -1, 2, 3, 4, 5, 6, 7, 8, 11, 12, 13
		}},
		{-9, new int[] {
			-10, -7, -4, 5, 8, 11
		}},
		{-10, new int[] {
			-10, -7, -4, 5, 8, 11
		}},
		{-11, new int[] {
			-12, -11, -10, -9, -8, -7, -4, -3, -2, -1, 2, 3, 4, 5, 8, 9, 10, 11, 12, 13
		}},
		{-12, new int[] {
			-12, -7, -1, 2, 8, 13
		}},
		{-13, new int[] {
			-12, -7, -1, 2, 8, 13
		}},
		{-14, new int[] {
			-12, -11, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13
		}}
	};

	// Use this for initialization
	void Start () {
		dotSprite = Resources.Load("pacdot", typeof(Sprite)) as Sprite;

		//Physics.GetIgnoreLayerCollision (1, 8);
		//Physics2D.IgnoreCollision (co.collider2D, this.collider2D);

		foreach (KeyValuePair<int, int[]> posi in this.dots) {
			foreach (int yCol in posi.Value) {
				this.createDot(new Vector3(yCol, posi.Key));
			}
		}
	}

	void createDot(Vector3 pos) {
		GameObject pacdot = new GameObject("pacdot");
		pacdot.AddComponent("BoxCollider2D");
		pacdot.collider2D.isTrigger = true;
		pacdot.AddComponent("Pacdot");
		pacdot.transform.position = pos;
		pacdot.transform.parent = this.transform;
		pacdot.layer = 8;
		SpriteRenderer rend = pacdot.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		rend.sprite = this.dotSprite;
	}

}
