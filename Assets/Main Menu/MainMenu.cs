using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[SerializeField] GameObject ButtonStart;
	[SerializeField] GameObject ButtonSettings;
	[SerializeField] GameObject ButtonControls;
	public static void StartGame() => UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
	public void OpenSettings()
	{
		ShowMenu(false);
		FindObjectOfType<SettingsMenu>().gameObject.SetActive(true);
	}
	public static void OpenControls() { }

	public void ShowMenu(bool value)
	{
		ButtonStart.SetActive(value);
		ButtonSettings.SetActive(value);
		ButtonControls.SetActive(value);
	}
}
