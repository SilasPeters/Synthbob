using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MenuBehaviour
{
	[SerializeField] Dropdown difficultyDropdown;

	void Start()
	{
		gameObject.SetActive(false);
		
	}
	void OnEnable()
	{
		LoadCurrentSettings();
	}

	private void LoadCurrentSettings()
	{
		
	}

	private void Save()
	{
		
	}
}
