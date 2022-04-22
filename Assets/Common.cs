using System.Collections;
using UnityEngine;

public static class Common
{
	public static Fresh<float> LevelProgression					= new(GetCalculatedLevelProgression);
	public static Player Player									= GameObject.Find("Player").GetComponent<Player>();
	public static CameraExtention CamExtention					= Camera.main.GetComponent<CameraExtention>();

	public static void Move2D(Transform obj, Vector2 delta)		=> obj.transform.localPosition += Time.deltaTime * new Vector3(delta.x, delta.y, 0);
	public static void Move2D(Transform obj, float x, float y)  => Move2D(obj, new Vector2(x, y));

	private static float GetCalculatedLevelProgression() {	
		float levelRightBound	   = 200; //TODO: laten bepalen
		float camRightBoundAtStart  = 25; //afgeleid uit standaard Unity scene
		return Mathf.InverseLerp(camRightBoundAtStart, levelRightBound, CameraExtention.CamBounds.xMax);
	}


	public delegate T Fetch<T>(); //wordt gebruikt in SomethingFresh<T> todo: kan dit weg of zo?
	public delegate bool IsFresh<T>(T current); //wordt gebruikt in SomethingFresh<T> todo: kan dit weg of zo?
	public struct Fresh<T>
    {
		private static T current;
		private static float lastFetch;
		private readonly Fetch<T> fetch;
		private readonly IsFresh<T> isFresh;

		public Fresh(Fetch<T> fetch) { this.fetch = fetch; isFresh = (current) => Time.time == lastFetch; }
		public Fresh(Fetch<T> fetch, IsFresh<T> isfresh) { this.fetch = fetch; this.isFresh = isfresh; }

		/// <summary>Returned de meest recenste versie van de waarde welke deze struct vertegenwoordigd. Als deze variabele deze frame nog niet is berekend, zal dat eerst plaatsvinden.</summary>
		public T GetFresh()
        {
			if (isFresh(current)) return current;
			else {
				lastFetch = Time.time;
				return current = fetch();
			}
        }
		public T GetDirty() => current;
    }
}