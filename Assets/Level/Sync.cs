using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sync
{
	private const int bpm = 115;
	private const int OffsetInMiliseconds = 0; //TODO: muziek en dit script worden niet gelijk ingeladen
	private const float WindowOfOpportunityInSeconds = 0.15f;
	private static float SingleBeatLength => 1f / (bpm / 60f);

	public static bool OnSync() => (Time.unscaledTime - OffsetInMiliseconds/1000f) % SingleBeatLength <= WindowOfOpportunityInSeconds;
}