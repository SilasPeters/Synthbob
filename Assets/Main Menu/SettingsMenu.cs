using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MenuBehaviour
{
	void Start()
	{
		gameobject.SetActive(false);
	}
	private void OnEnable()
	{
		//load settings
	}

	public void Save()
	{
		print("Saved!");
	}
}
