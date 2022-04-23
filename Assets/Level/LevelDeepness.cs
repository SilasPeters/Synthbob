using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDeepness : MonoBehaviour
{
	[SerializeField] Transform RootOfTerrainGameObjects;
	[SerializeField] float Amplifier = 1;
	[SerializeField] bool LogTrackedObjects = false;

	private List<Transform> transforms = new();

	void Start() {
		Debug.Assert(RootOfTerrainGameObjects != null, "RootOfTerrainGameObjects heeft geen GameObject toegewezen in LevelDeepness.cs");
		FetchChildsRecursively(RootOfTerrainGameObjects, transforms);
	}

	// Update is called once per frame
	void Update() {
		foreach (Transform t in transforms)
			if (TransformIsVisible(t))
				ApplyDeepness(t);
	}

	/// <summary>Verzamelt alle te simuleren transform modules van gameobjects</summary>
	/// <param name="currentChild">Het transform vanuit waar recursief gezocht moet worden</param>
	/// <param name="sticks">De hoop aan stokken die gefetched zijn</param>
	private void FetchChildsRecursively(Transform currentChild, List<Transform> sticks) {
		if (currentChild.CompareTag("LevelDeepness")) {
			if (LogTrackedObjects) Debug.Log(currentChild.name);
			sticks.Add(currentChild);
		}
		for (int i = 0; i < currentChild.childCount; i++)
			FetchChildsRecursively(currentChild.GetChild(i), sticks);
	}

	bool TransformIsVisible(Transform t) {
		Vector3 visibleArea = Camera.main.WorldToViewportPoint(t.position);
		return new Rect(0, 0, 1, 1).Contains(visibleArea);
	}

	void ApplyDeepness(Transform t) {
		float diepteFactor = (int)(t.position.z / 10); //magic
		float movementFactor = diepteFactor / 10 * CameraExtention.CurrentMovementSpeed;
		LevelCommons.Move2D(t, new Vector2(movementFactor * Amplifier, 0));
	}
}
