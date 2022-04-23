using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Settings : MonoBehaviour
{
	public static int Difficulty { get; private set; }
	public static bool Cheats { get; private set; }
	public static bool EnableItalian { get; private set; }

	private static readonly string filePath = Application.dataPath + @"/settings.txt";

	private void Start()
	{
		try
		{
			///=>settings uitlezen
			using StreamReader reader = new(filePath);
			string[] settings = reader.ReadToEnd().TrimEnd('\r', '\n').Split("\r\n"); //todo: werkt dit ook op mac?

			Difficulty =	 int.Parse(ParseSetting(settings, 0));
			Cheats =		bool.Parse(ParseSetting(settings, 1));
			EnableItalian = bool.Parse(ParseSetting(settings, 2));
		}
		catch (System.Exception e)
		{
			throw new System.Exception($"Het settings bestand is niet te vinden, mist één of meer regels of heeft ongeldige waardes:\n\"{e.Message}\""); //todo: dit netter verwerken
		}

		///<summary>Leest een regel van een setting af. Gaat uit van maar één = teken in een regel, en dat rechts daarvan de waarde zit</summary>
		static string ParseSetting(string[] settings, int index) => settings[index].Split('=')[1].Trim();
	}

	/// <summary>Flushed alle instellingen naar settings.txt, waarna als het succesvol is gegaan de wijzigingen worden opgeslagen in de Settings klasse.</summary
	/// <returns>Als het opslaan succesvol ging, zal True gereturned worden, anders False.</returns>
	public static bool Save(int difficulty, bool cheats, bool enableItalian)
	{
		try
		{
			///=> zet parameters om in text wat naar settings.txt wordt geschreven
			string settingsInText = $"Difficulty={difficulty}\r\n" +
									$"Cheats={cheats}\r\n" +
									$"EnableItalian={enableItalian}";
			using StreamWriter writer = new(filePath);
			writer.Write(settingsInText);
		}
		catch (System.Exception)
		{
			return false;
		}

		/// deze code wordt alleen uitgevoerd als de wijzigingen al zijn gecommit naar settings.txt
		Difficulty = difficulty;
		Cheats = cheats;
		EnableItalian = enableItalian;

		return true;
	}
}
