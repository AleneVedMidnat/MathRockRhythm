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
	[SerializeField] TextMeshProUGUI m_achievementText;
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
				m_achievementText.text = "Worst";
				m_achievementText.color = new Color32(0xFF, 0x00, 0xFF, 0xFF);
				//FFFF00
				break;
			case "sad":
				playerHealth -= 1;
				m_achievementText.text = "Sad";
				m_achievementText.color = new Color32(0x00, 0xFF, 0x00, 0xFF);
				//0000FF
				break;
			case "safe":
				m_achievementText.text = "Safe";
				m_achievementText.color = new Color32(0x00, 0xFF, 0x00, 0xFF);
				//00FF00
				break;
			case "fine":
				playerHealth += 1;
				playerScore+= 1;
				m_achievementText.text = "Fine";
				m_achievementText.color = new Color32(0x00, 0x00, 0xFF, 0xFF);
				//00FFFF
				break;
			case "cool":
				playerHealth += 2;
				playerScore += 2;
				m_achievementText.text = "Cool";
				m_achievementText.color = new Color32(0xFF, 0xFF, 0x00, 0xFF);
				//FF00FF
				break;
			case "miss":
				playerHealth -= 2;
				m_achievementText.text = "Miss";
				m_achievementText.color = new Color32(0xFF, 0x00, 0x00, 0xFF);
				break;

		}
	}

	#endregion
}
