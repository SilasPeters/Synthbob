using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public static void StartGame() => UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
	public static void OpenSettings() { }
	public static void OpenControls() { }
}
