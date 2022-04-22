using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraExtention : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField]				float   baseMovementSpeed;
	[SerializeField]				float   maximumMovementSpeed;
	[SerializeField]				float   startupTime;
	[Header("Out Of Bounds Indicator")]
	[SerializeField]				Image   OOBIndicatorImage;
	[SerializeField]				float   _OOBIndicatorRange = 8f;
	[Header("Feel the beat")]
	[SerializeField]				bool	enableHeartBeat;
	[SerializeField]				float   heartBeatDuration = 0.6f;
	[SerializeField, Range(0, 1)]	float   cameraViewSizeWhenOnBeat = 0.99f;
	public static float CurrentMovementSpeed { get; private set; }
	public static Rect CamBounds { get; private set; }
	
	private float cameraViewSizeNormal;
	private bool movementLock = false;
	private bool heartbeatLock = false;

	void Start() {
		cameraViewSizeNormal = Camera.main.orthographicSize;
		StartCoroutine(StartCamera());
	}

	void Update() {
		UpdateAreaVisible();
		UpdateOOBIndicator();

		//Voer één heartbeat uit indien nu nodig
		if (enableHeartBeat && !heartbeatLock && Sync.OnSync()) StartCoroutine(Heartbeat());

		// Beweeg camera
		if (Input.GetKeyDown(KeyCode.Z))
			movementLock = !movementLock;
		if (!movementLock)
			Common.Move2D(transform.parent, CurrentMovementSpeed, 0);
		//TODO: hierboven staat .parent. Hmhm. MIsschien moet dit script verplaatst worden naar screen, met alle gevolgen
	}

	private IEnumerator StartCamera() {
		float timeElapsed = 0;

		while (timeElapsed < startupTime) {
			CurrentMovementSpeed = Mathf.Lerp(0, baseMovementSpeed, timeElapsed / startupTime);
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		StartCoroutine(VersnelScrollSnelheid());
	}
	private IEnumerator VersnelScrollSnelheid() {
		while (true) { //Gedurende het hele level
			CurrentMovementSpeed = Mathf.Lerp(baseMovementSpeed, maximumMovementSpeed, Common.LevelProgression.GetFresh());
			yield return null;
		}
	}

	private void UpdateOOBIndicator() {
		OOBIndicatorImage.color = new Color(255, 255, 255, Mathf.Lerp(0, 1,
														   Mathf.InverseLerp(CamBounds.xMin + _OOBIndicatorRange, CamBounds.xMin, Common.Player.transform.position.x))
										   );
	}

	private void UpdateAreaVisible() {
		Camera c = Camera.main;
		Vector2 viewSize = new(c.orthographicSize * c.aspect * 2, c.orthographicSize * 2);
		CamBounds = new Rect((Vector2)c.transform.position - viewSize / 2, viewSize);
	}
	private IEnumerator Heartbeat() {
		heartbeatLock = true; //lock instellen

		float progression, timeStart = Time.unscaledTime;
		while ((progression = (Time.unscaledTime - timeStart) / heartBeatDuration) < 1)
		{
			//Camera.main.orthographicSize = Mathf.Lerp(CameraViewSizeNormal, CameraViewSizeNormal * CameraViewSizeWhenOnBeat, Mathf.Sin(progression * Mathf.PI));
			Camera.main.orthographicSize = Mathf.Lerp(cameraViewSizeNormal * cameraViewSizeWhenOnBeat, cameraViewSizeNormal, progression);
			yield return null;
		}
		Camera.main.orthographicSize = cameraViewSizeNormal;

		heartbeatLock = false; //lock vrijlaten
	}

	public IEnumerator ScreenShake(float intensity, float duration) {
		Vector3 originalPos = transform.localPosition;

		float shakeStart = Time.time;
		while (Time.time - shakeStart < duration)
		{
			float xOffset = Random.Range(-0.5f, 0.5f) * intensity;
			float yOffset = Random.Range(-0.5f, 0.5f) * intensity;
			transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);

			yield return null;
		}
		transform.localPosition = originalPos;
		yield return null;
	}
}
