using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public static void GoToCave()
	{
		SceneManager.LoadScene("Cave");
	}

	public static void GoToForest()
	{
		SceneManager.LoadScene("Forest");
	}

	public static void GoToSpace()
	{
		SceneManager.LoadScene("Space");
	}

	public static void GoToMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
