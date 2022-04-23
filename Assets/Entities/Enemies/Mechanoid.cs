﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanoid : Enemy
{
	public override void Update() {
		base.Update();
		MoveTowardsPlayer();
	}

	/// <summary>Verplaatst de vijand met een vaste hoeveelheid lineair naar de speler</summary>
	private void MoveTowardsPlayer() {
		Vector2 distanceVector = LevelCommons.Player.transform.position - transform.position;
		LevelCommons.Move2D(transform, distanceVector.normalized * BaseMovementSpeed);
	}
}
