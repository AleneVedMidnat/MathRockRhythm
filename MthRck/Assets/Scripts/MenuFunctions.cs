using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour
{
	TextMeshProUGUI inputText;

	private void Start()
	{
		//Debug.Log(FindObjectOfType<Dropdown>().gameObject);
		inputText = GameObject.Find("Dropdown").GetComponentInChildren<TextMeshProUGUI>();
	}
	public void GoToCave()
	{
		PlayerPrefs.SetInt("InputSettings", SetToNumber(inputText.text));
		SceneManager.LoadScene("Cave");
	}

	public void GoToForest()
	{
		PlayerPrefs.SetInt("InputSettings", SetToNumber(inputText.text));
		SceneManager.LoadScene("Forest");
	}

	public void GoToSpace()
	{
		PlayerPrefs.SetInt("InputSettings", SetToNumber(inputText.text));
		SceneManager.LoadScene("Space");
	}

	public static void GoToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	private int SetToNumber(string input)
	{
		int returnvalue;
		switch (input) 
		{
			case "1234":
				returnvalue = 0;
				break;
			case "DFJK":
				returnvalue = 1;
				break;
			case "Remote":
				returnvalue = 2;
				break;
			default: 
				returnvalue = 0;
				break;
		}
		return returnvalue;

	}
}
