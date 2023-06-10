using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class RhythmEventSystem : MonoBehaviour
{
	PlayerInput m_input;
	[SerializeField] KeyController m_lane1;
	[SerializeField] KeyController m_lane2;
	[SerializeField] KeyController m_lane3;
	[SerializeField] KeyController m_lane4;
	int playerScore = 0;
	int playerHealth = 50;

    // Start is called before the first frame update
    void Start()
    {
        m_input= GetComponent<PlayerInput>();
		m_input.currentActionMap.FindAction("Lane1").performed += Lane1;
		m_input.currentActionMap.FindAction("Lane2").performed += Lane2;
		m_input.currentActionMap.FindAction("Lane3").performed += Lane3;
		m_input.currentActionMap.FindAction("Lane4").performed += Lane4;

		KeyController.scoringEvent += ScoreEvent;
	}

	#region InputEvents
	// Update is called once per frame
	public void Lane1(InputAction.CallbackContext context)
	{
		m_lane1.KeyPressed();
	}

	public void Lane2(InputAction.CallbackContext context)
	{
		m_lane2.KeyPressed();
	}

	public void Lane3(InputAction.CallbackContext context)
	{
		m_lane3.KeyPressed();
	}

	public void Lane4(InputAction.CallbackContext context)
	{
		m_lane4.KeyPressed();
	}

	#endregion

	#region ScoreEvents

	private void ScoreEvent(string score)
	{
		//score and health incrementation
		switch (score)
		{
			case "worst":
				playerHealth -= 2;
				break;
			case "sad":
				playerHealth -= 1;
				break;
			case "safe":
				break;
			case "fine":
				playerHealth += 1;
				playerScore+= 1;
				break;
			case "cool":
				playerHealth += 2;
				playerScore += 2;
				break;
		}
	}

	#endregion
}
