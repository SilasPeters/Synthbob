using System.Collections;
using UnityEngine;

/// <summary>Any GameObject containing this component will slow time when an enemy is close</summary>
public class Slowmotion : MonoBehaviour
{
	/// ==> Fields die zichtbaar zijn in Unity
	[SerializeField]				GameObject RootObjectOfEnemies;
	/// <summary>Maximum time the player is given to react on a single danger</summary>
	[SerializeField, Range(0, 3)]	float   NormalDuration				= 0.75f;
	/// <summary>The factor of the maximum amount of slowness applied when dangers are most imminent</summary>
	[SerializeField, Range(0, 3)]	float   MostDifficultDuration		= 0.2f;
	/// <summary>The factor of the maximum amount of slowness applied when dangers are most imminent</summary>
	[SerializeField, Range(0, 1)]	float   Intensity					= 0.8f;
	/// <summary>x^SlowExponentie = de snelheid waarin de slowmotion wordt toegepast</summary>
	[SerializeField, Range(0, 3)]	float   SlowingExponent				= 0.8f;
	/// <summary>The range in which a danger is considered as too close and when slowmotion will be applied</summary>
	[SerializeField, Range(0, 30)]	float   TriggerRange				= 5f;

	private const float fixedTimeScale = 0.02f;

	private Enemy[] trackedEnemies;

	void Start() {
		trackedEnemies = RootObjectOfEnemies.GetComponentsInChildren<Enemy>();
		StartCoroutine(SlowmotionLoop());
	}

	/// <summary>Performs slowmotion logics and applies slowmotion if adequate</summary>
	private IEnumerator SlowmotionLoop() {
		///=> Start pas wanneer slowmotion toegepast moet worden
		float x;
		while ((x = DistanceToNearestEnemy()) == -1 || x > TriggerRange) //er is geen enemy of de enemy is te ver voor slowmotion
			yield return null; //sla frame over

		///=> Op dit punt moet slowmotion toegepast worden
		// pas slowmotion toe en wacht tot slowmotion eindigt
		yield return StartCoroutine(SlowTime());

		///=> Ga opnieuw door de loop
		StartCoroutine(SlowmotionLoop());
	}

	/// <summary>Decreases game speed following an exponential curve. Has no effect when slowmotion is applied to its fullest already</summary>
	private IEnumerator SlowTime() {
		float maxDuration = Mathf.Lerp(NormalDuration, MostDifficultDuration, Common.LevelProgression.GetFresh());
		float x = 0;
		while ((x += Time.unscaledDeltaTime) < maxDuration) { // gaat door tot maximale duratie (en slowmotion) is bereikt
			Time.timeScale = 1 - Intensity * Mathf.Pow(x / maxDuration, SlowingExponent);
			//y = 1 - Intensity * (x/maxDuration)^SlowingExponent; Bereik = [0, 1]
			Time.fixedDeltaTime = Time.timeScale * fixedTimeScale;
			yield return null;
		} //todo: doe dat slowmotion blijft als een andere enemy ook dichtbij is

		// Verwijder slowmotion
		Time.timeScale = 1;
		Time.fixedDeltaTime = fixedTimeScale;
	}

	/// <returns>Distance to nearest enemy. Returns -1 if there aren't any enemies</returns>
	private float DistanceToNearestEnemy() {
		float minDistance = float.PositiveInfinity;
		
		foreach (Enemy e in trackedEnemies) {
			if (e == null || !e.gameObject.activeInHierarchy) continue; // skip inactive gameobjects //todo: als een gameobject gekilled wordt moet deze uit de lijst worden gehaald
			float distanceSquared = (e.transform.position - transform.position).sqrMagnitude;
			if (distanceSquared < minDistance * minDistance) minDistance = Mathf.Sqrt(distanceSquared);
		}
		//todo: negeer vijand voor een tijdje met async methode
		return minDistance != float.PositiveInfinity ? minDistance : -1;
	}
}