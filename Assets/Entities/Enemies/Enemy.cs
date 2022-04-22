using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : LivingEntity
{
	// Fields zichtbaar in Unity
	[Header("Enemy.cs")]
	[SerializeField] protected float HitEffectDuration;
	//todo: rigidbody standaard toevoegen?

	public override void Damage(int amount) {
		base.Damage(amount);
		StartCoroutine(Stun());
		if (CurrentHealth <= BaseHealth / 2) SpriteRenderer.color = new Color(0, 255, 73);
	}
	public override void Kill() => Destroy(gameObject);

	/// <summary>Verandert de kleur van de SpriteRenderer</summary>
	public IEnumerator Stun() {
		float temp = BaseMovementSpeed;
		BaseMovementSpeed = 0;
		yield return new WaitForSeconds(HitEffectDuration);
		BaseMovementSpeed = temp;
	}
}