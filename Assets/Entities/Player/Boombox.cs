using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boombox : MonoBehaviour
{
	[SerializeField]				GameObject soundwave;
	[SerializeField]				float cooldownInSeconds;
	[SerializeField, Range(0, 1)]	float recoilIntensity;
	[SerializeField, Range(0, 1)]	float recoilDuration;
	
	private AudioSource soundShoot;
	private float timeCooldownOver;
	private Quaternion rotationToCursor;

	void Start() {
		soundShoot = GetComponent<AudioSource>();
		Debug.Assert(soundShoot != null, name + " heeft geen AudioSource component voor het schieten");
	}

	void Update() {
		Aim(out rotationToCursor);

		if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeCooldownOver)// && Sync.OnSync())
			Shoot();
	}

	private void Aim(out Quaternion rotationToCursor) {
		//first, calculate the angle towards the cursor
		Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 boomboxPos = transform.position;
		Vector2 aimVector = cursorPos - boomboxPos;
		float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;

		//apply rotation
		transform.rotation = rotationToCursor = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	private void Shoot()  {
		//Aestethics
		StartCoroutine(LevelCommons.CamExtention.ScreenShake(recoilIntensity, recoilDuration)); //todo: je kunt parameters meegeven wow
		soundShoot.Play();

		//Shooting
		timeCooldownOver = Time.time + cooldownInSeconds; //definiëer wanneer de coldown weer over is
		Instantiate(soundwave, transform.position, rotationToCursor); //maak van de meegegeven prefab "soundwave" een echt object
	}
}