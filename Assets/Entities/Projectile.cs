using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[Header("Projectile.cs")]
	[SerializeField]				protected float Speed;
	[SerializeField]				protected int Damage;
	[SerializeField, Range(0, 1)]	protected float CollateralModifier; //todo: een wapen die juist meer damage na penetratie doet?
	[SerializeField]				protected float TimeToLive;

	protected SpriteRenderer SpriteRenderer;

	public virtual void Update() {
		SpriteRenderer = GetComponent<SpriteRenderer>();
		Debug.Assert(SpriteRenderer != null, $"{name} bevat geen Spriterenderer");

		if ((TimeToLive -= Time.deltaTime) < 0) Destroy(gameObject);
		Common.Move2D(transform, transform.right * Speed); //move towards red axis
	}

	public virtual void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.TryGetComponent(out Enemy enemy)) {
			enemy.Damage(Damage);
			FallBackToCollatoralDamage();
		}
	}

	/// <summary>Vermindert de kracht van dit projectiel zodat het alleen nog colaltoral damage doet</summary>
	public virtual void FallBackToCollatoralDamage() {
		Damage = (int)(Damage * CollateralModifier); //set damage to collateral damage
		Color colorNow = SpriteRenderer.color;
		SpriteRenderer.color = new Color(colorNow.r, colorNow.g, colorNow.b, colorNow.a / 2);
	}
}