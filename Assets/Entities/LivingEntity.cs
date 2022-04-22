using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{
	// Fields zichtbaar in Unity
	[Header("LivingEntity.cs")]
	[SerializeField] protected int BaseHealth;
	[SerializeField] protected float BaseMovementSpeed;

	protected Collider2D Hitbox;
	protected SpriteRenderer SpriteRenderer;
	public int CurrentHealth { get; protected set; }

	public virtual void Start() {
		Hitbox = GetComponent<Collider2D>();
		Debug.Assert(Hitbox != null, this.name + " heeft geen Collider2D component (hitbox)");
		SpriteRenderer = GetComponent<SpriteRenderer>();
		Debug.Assert(SpriteRenderer != null, this.name + " heeft geen SpriteRenderer component");

		CurrentHealth = BaseHealth;
	}

	public virtual void Update() {
	}

	public abstract void Kill();
	public virtual void Damage(int value) {
		if ((CurrentHealth -= value) <= 0) Kill();
	}
	/// <summary>Deals [percentage * BaseHealth] amount of damage</summary>
	/// <param name="percentage">The percentage of damage: 0.0 - 1.0f</param>
	/// <remarks>Resulting health will be rounded down to the nearest integer</remarks>
	public void DamageByPercentage(float percentage) => Damage((int)(percentage * BaseHealth));
	public void SetHealth(int value) => CurrentHealth = value;
	/// <summary>Sets CurrentHealth to [percentage * BaseHealth]</summary>
	/// <param name="percentage">The percentage of health: 0.0 - 1.0f</param>
	/// <remarks>Resulting health will be rounded down to the nearest integer</remarks>
	public void SetHealthByPercentage(float percentage) => SetHealth((int)(percentage * BaseHealth));
}