using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
	// Fields zichtbaar in Unity
	[Header("Player.cs")]
	[SerializeField]				protected float MaxMovementSpeed;
	[SerializeField, Range(0, 60)]  protected float JumpVelocity;
	[SerializeField, Range(0, 120)] protected float JumpPadVelocity;

	public static float CurrentMovementSpeed { get; protected set; }
	public static Rigidbody2D Rigidbody { get; protected set; }

	public static int _incomingThreats = 0; //todo, verantwoordelijkheid van slowmotion

	public override void Start() {
		base.Start();
		CurrentMovementSpeed = BaseMovementSpeed;
		Rigidbody = GetComponent<Rigidbody2D>();
		Debug.Assert(Rigidbody != null, name + " heeft geen RigidBody2D component");
	}

	public override void Update() {
		base.Update();
		CurrentMovementSpeed = Mathf.Lerp(BaseMovementSpeed, MaxMovementSpeed, LevelCommons.LevelProgression.GetFresh());
		ProcessUserInput();
		HandleOutOfBounds();
	}

	public override void Kill() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(2);
	}

	public override void Damage(int amount) {
		base.Damage(amount);
		//Hit Animation
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name.Contains("Jumppad"))
			Rigidbody.velocity += new Vector2(0, JumpPadVelocity); //overschrijft een eerdere jump of zo todo: feature? Zo niet, maak er += van

		else if (other.gameObject.GetComponent<Enemy>() != null) //als enemy gameobject een Enemy is
			this.Kill(); //todo: meteen killen lijkt slordig
	}

	private void ProcessUserInput()
	{
		// Lopen
		Vector2 sidewaysMovement = Vector2.zero;
		if (Input.GetKey(KeyCode.A)) sidewaysMovement += new Vector2(-1, 0);
		if (Input.GetKey(KeyCode.D)) sidewaysMovement += new Vector2( 1, 0);
		LevelCommons.Move2D(transform, CurrentMovementSpeed * sidewaysMovement);
		SpriteRenderer.flipX = sidewaysMovement.x < 0;

		// Springen
		if (Input.GetKeyDown(KeyCode.Space) && Rigidbody.velocity.y == 0) //todo: gebruik maken van player controller?
			Rigidbody.velocity += new Vector2(0, JumpVelocity);

		// Fast-fall
		//if (Input.GetKey(KeyCode.S)) rigidbody.velocity += new Vector2(0, FastFallVelocity);
	}

	private void HandleOutOfBounds() {
		Rect camBounds = CameraExtention.CamBounds;
		Bounds playerBounds = Hitbox.bounds;

		if (playerBounds.max.x < camBounds.xMin || playerBounds.max.y < camBounds.yMin) // links of onder out of bounds
			Kill();
		else if (playerBounds.max.x > camBounds.xMax) // rechts out of bounds
			// zet de speler terug op de grens
			transform.position = new Vector3(transform.position.x - (playerBounds.max.x - camBounds.xMax),
											 transform.position.y,
											 transform.position.z);
	}
}
