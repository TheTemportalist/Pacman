using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dots : ObjCreator {

	Sprite dotSprite;
	public bool generate = true;
	
	override protected bool isEnabled() {
		return this.generate;
	}

	override protected Dictionary<int[], int[]> getPoints () {
		return new Dictionary<int[], int[]> (){
			{new int[]{
				1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,
				14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26
			}, new int[]{1}},
			{new int[]{1, 12, 15, 26}, new int[]{2}},
			{new int[]{12, 15}, new int[]{3}},
			{new int[]{
				1, 2, 3, 4, 5, 6, 9, 10, 11, 12,
				15, 16, 17, 18, 21, 22, 23, 24, 25, 26
			}, new int[]{4}},
			{new int[]{3, 6, 9, 18, 21, 24}, new int[]{5}},
			{new int[]{3, 6, 9, 18, 21, 24}, new int[]{6}},
			{new int[]{
				1, 2, 3,
				6, 7, 8, 9, 10, 11, 12, 13,
				14, 15, 16, 17, 18, 19, 20, 21,
				24, 25, 26
			}, new int[]{7}},
			{new int[]{1, 6, 12, 15, 21, 26}, new int[]{8}},
			{new int[]{1, 6, 12, 15, 21, 26}, new int[]{9}},
			{new int[]{
				1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,
				15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26
			}, new int[]{10}},
			{new int[]{6, 9, 18, 21}, new int[]{11}},
			{new int[]{6, 9, 18, 21}, new int[]{12}},
			{new int[]{6, 9, 18, 21}, new int[]{13}},
			{new int[]{6, 9, 18, 21}, new int[]{14}},
			{new int[]{6, 9, 18, 21}, new int[]{15}},
			{new int[]{6, 7, 8, 9, 18, 19, 20, 21}, new int[]{16}},
			{new int[]{6, 9, 18, 21}, new int[]{17}},
			{new int[]{6, 9, 18, 21}, new int[]{18}},
			{new int[]{6, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 21}, new int[]{19}},
			{new int[]{6, 12, 15, 21}, new int[]{20}},
			{new int[]{6, 12, 15, 21}, new int[]{21}},
			{new int[]{
				1, 2, 3, 4, 5, 6,
				9, 10, 11, 12,
				15, 16, 17, 18,
				21, 22, 23, 24, 25, 26
			}, new int[]{22}},
			{new int[]{1, 6, 9, 18, 21, 26}, new int[]{23}},
			{new int[]{1, 6, 9, 18, 21, 26}, new int[]{24}},
			{new int[]{
				1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,
				14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26
			}, new int[]{25}},
			{new int[]{1, 6, 12, 15, 21, 26}, new int[]{26}},
			{new int[]{1, 6, 12, 15, 21, 26}, new int[]{27}},
			{new int[]{6, 12, 15, 21}, new int[]{28}},
			{new int[]{
				1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,
				15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26
			}, new int[]{29}}
		};
	}
	
	override protected void createAtPoint(Vector3 point) {
		this.createDot (point, Pacman.PowerUp.NONE);
	}

	protected void createDot(Vector3 point, Pacman.PowerUp powerup) {
		GameObject pacdot = new GameObject("pacdot");
		// create collider
		pacdot.AddComponent("BoxCollider2D");
		((BoxCollider2D)pacdot.collider2D).size = new Vector2(.25f, .25f);
		// add script
		pacdot.AddComponent("Pacdot");
		((Pacdot)pacdot.GetComponent("Pacdot")).powerup = powerup;
		// set transforms
		pacdot.transform.parent = this.transform;
		pacdot.transform.position = point;
		if (powerup != Pacman.PowerUp.NONE) {
			pacdot.transform.localScale += new Vector3(1.5f, 1.5f, 0f);
		}
		// set layer
		pacdot.layer = 8;
		// set sprite rendering
		SpriteRenderer rend = pacdot.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		rend.sprite = this.dotSprite;
	}

	override protected void init() {
		dotSprite = Resources.Load("pacdot", typeof(Sprite)) as Sprite;
	}

}
