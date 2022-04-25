using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MenuBehaviour
{
	[SerializeField] Dropdown difficultyDropdown;
	[SerializeField] Toggle cheatsToggle;
	[SerializeField] Toggle enableItalian;

	void Start()
	{
		gameObject.SetActive(false);
	}

	public void Show()
	{
		LoadCurrentSettings();
		gameObject.SetActive(true);
	}

	private void LoadCurrentSettings()
	{
		difficultyDropdown.value = Settings.Difficulty;
		cheatsToggle.isOn = Settings.Cheats;
		enableItalian.isOn = Settings.EnableItalian;
	}

	private void Save()
	{
		bool succes = Settings.Save(difficultyDropdown.value, cheatsToggle.isOn, enableItalian.isOn);
		if (!succes) Debug.LogError("Saving failed"); //todo doe aan user feedback
	}
}
