using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MenuBehaviour
{
	public static void StartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
	}
	public void OpenSettings()
	{
		//ShowMenu(false);
		//FindObjectOfType<SettingsMenu>(true).ShowMenu(true);
	}
	public static void OpenControls()
	{

	}
}
