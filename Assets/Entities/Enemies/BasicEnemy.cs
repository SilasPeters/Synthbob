using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
	public override void Update() {
		base.Update();
		MoveTowardsPlayer();
	}

	/// <summary>Verplaatst de vijand met een vaste hoeveelheid lineair naar de speler</summary>
	private void MoveTowardsPlayer() {
		Vector2 distanceVector = Common.Player.transform.position - transform.position;
		Common.Move2D(transform, distanceVector.normalized * BaseMovementSpeed);
	}
}
