using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	Sprite dotSprite;

	// row by column
	Dictionary<int[], int[]> dots = new Dictionary<int[], int[]>(){
		{new int[]{10, -9}, new int[]{
			-13, -12, -11, -10, -9, -8, -7, -6, -5, -4, -3, -2,
			1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
		}},
		{new int[]{9, 7, -10, -11}, new int[]{-13, -8, -2, 1, 7, 12}},
		{new int[]{8}, new int[]{-8, -2, 1, 7}},
		{new int[]{6}, new int[]{
			-13, -12, -11, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1,
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
		}},
		{new int[]{5, 4}, new int[]{-13, -8, -5, 4, 7, 12}},
		{new int[]{3}, new int[]{
			-13, -12, -11, -10, -9, -8, -5, -4, -3, -2,
			1, 2, 3, 4, 7, 8, 9, 10, 11, 12
		}},
		{new int[]{-15}, new int[]{
			-12, -11, -10, -9, -8, -5, -4, -3, -2,
			1, 2, 3, 4, 7, 8, 9, 10, 11
		}},
		{new int[]{2, 1}, new int[]{-8, -2, 1, 7}},
		{new int[]{0}, new int[]{
			-8, -5, -4, -3, -2, -1,
			0, 1, 2, 3, 4, 7
		}},
		{new int[]{-1, -2, -4, -5, -6, -7, -8}, new int[]{-8, -5, 4, 7}},
		{new int[]{-3}, new int[]{-8, -7, -6, -5, 4, 5, 6, 7}},
		{new int[]{-12}, new int[]{
			-13, -12, -11, -8, -7, -6, -5, -4, -3, -2, -1,
			0, 1, 2, 3, 4, 5, 6, 7, 10, 11, 12
		}},
		{new int[]{-13, -14}, new int[]{-11, -8, -5, 4, 7, 10}},
		{new int[]{-16, -17}, new int[]{-13, -2, 1, 12}},
		{new int[]{-18}, new int[]{
			-13, -12, -11, -10, -9, -8, -7, -6, -5, -4, -3, -2, -1,
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
		}}
	};
	Dictionary<int, int[]> ghostDots = new Dictionary<int, int[]>{
		{8, new int[]{-13, 12}}, {-15, new int[]{-13, 12}}
	};


	// Use this for initialization
	void Start () {
		dotSprite = Resources.Load("pacdot", typeof(Sprite)) as Sprite;

		//Physics.GetIgnoreLayerCollision (1, 8);
		//Physics2D.IgnoreCollision (co.collider2D, this.collider2D);

		foreach (KeyValuePair<int[], int[]> posi in this.dots) {
			foreach (int row in posi.Key) {
				foreach (int col in posi.Value) {
					this.createDot(new Vector3(col, row), Pacman.PowerUp.NONE);
				}
			}
		}
		foreach (KeyValuePair<int, int[]> posi in this.ghostDots) {
			foreach (int col in posi.Value)
				this.createDot (new Vector3 (col, posi.Key), Pacman.PowerUp.GHOST);
		}

	}

	void createDot(Vector3 pos, Pacman.PowerUp powerup) {
		// create object
		GameObject pacdot = new GameObject("pacdot");
		// create collider
		pacdot.AddComponent("BoxCollider2D");
		((BoxCollider2D)pacdot.collider2D).size = new Vector2(.25f, .25f);
		// add script
		pacdot.AddComponent("Pacdot");
		((Pacdot)pacdot.GetComponent("Pacdot")).powerup = powerup;
		// set transforms
		pacdot.transform.parent = this.transform;
		pacdot.transform.position = pos + new Vector3(.5f, 4.5f);
		if (powerup != Pacman.PowerUp.NONE) {
			pacdot.transform.localScale += new Vector3(1.5f, 1.5f, 0f);
		}
		// set layer
		pacdot.layer = 8;
		// set sprite rendering
		SpriteRenderer rend = pacdot.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		rend.sprite = this.dotSprite;

	}

}
