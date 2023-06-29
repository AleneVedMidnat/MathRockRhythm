using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RhythmEventSystem : MonoBehaviour
{
	PlayerInput m_input;
	[SerializeField] KeyController m_lane1;
	[SerializeField] KeyController m_lane2;
	[SerializeField] KeyController m_lane3;
	[SerializeField] KeyController m_lane4;
	[SerializeField] TextMeshProUGUI m_achievementText;
	[SerializeField] GameObject gameplayUI;
	[SerializeField] GameObject scoreUI;
	[SerializeField] AudioSource backgroundAudio;
	[SerializeField] GameObject healthbar;
	int playerScore = 0;
	int playerHealth;
	int cool = 0;
	int fine = 0;
	int safe = 0;
	int sad = 0;
	int worst = 0;
	int miss = 0;

	public event System.Action<bool> endSong;

	// Start is called before the first frame update
	void OnEnable()
    {
        m_input= GetComponent<PlayerInput>();
		m_input.currentActionMap.FindAction("Lane1").performed += Lane1;
		m_input.currentActionMap.FindAction("Lane2").performed += Lane2;
		m_input.currentActionMap.FindAction("Lane3").performed += Lane3;
		m_input.currentActionMap.FindAction("Lane4").performed += Lane4;

		//KeyController.scoringEvent += ScoreEvent;
		m_lane1.scoringEvent+= ScoreEvent;
		m_lane2.scoringEvent+= ScoreEvent;
		m_lane3.scoringEvent+= ScoreEvent;
		m_lane4.scoringEvent+= ScoreEvent;
		StartCoroutine(SongEnd());
		playerHealth = 25;
	}

	IEnumerator SongEnd()
	{
		yield return new WaitForSeconds(backgroundAudio.clip.length);
		gameplayUI.SetActive(false);
		scoreUI.SetActive(true);
		scoreUI.GetComponent<ShowScoreUI>().DisplayScoreUI(playerScore, cool, fine, safe, sad, worst, miss, false);
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
				worst++;
				m_achievementText.text = "Worst";
				m_achievementText.color = new Color32(0xFF, 0x00, 0xFF, 0xFF);
				//FFFF00
				break;
			case "sad":
				playerHealth -= 1;
				sad++;
				m_achievementText.text = "Sad";
				m_achievementText.color = new Color32(0x00, 0xFF, 0x00, 0xFF);
				//0000FF
				break;
			case "safe":
				safe++;
				m_achievementText.text = "Safe";
				m_achievementText.color = new Color32(0x00, 0xFF, 0x00, 0xFF);
				//00FF00
				break;
			case "fine":
				playerHealth += 1;
				playerScore+= 1;
				fine++;
				m_achievementText.text = "Fine";
				m_achievementText.color = new Color32(0x00, 0x00, 0xFF, 0xFF);
				//00FFFF
				break;
			case "cool":
				playerHealth += 2;
				playerScore += 2;
				cool++;
				m_achievementText.text = "Cool";
				m_achievementText.color = new Color32(0xFF, 0xFF, 0x00, 0xFF);
				//FF00FF
				break;
			case "miss":
				playerHealth -= 2;
				miss++;
				m_achievementText.text = "Miss";
				m_achievementText.color = new Color32(0xFF, 0x00, 0x00, 0xFF);
				break;

		}
		if (playerHealth <= 0)
		{
			AudioSource[] audios = FindObjectsOfType<AudioSource>();
			for (int i = 0; i < audios.Length; i++)
			{
				audios[i].Stop();
			}
			//FindObjectOfType<AudioSource>().Stop();
			gameplayUI.SetActive(false);
			scoreUI.SetActive(true);
			scoreUI.GetComponent<ShowScoreUI>().DisplayScoreUI(playerScore, cool, fine, safe, sad, worst, miss, true);
			endSong?.Invoke(true);
			
		}
		else if (playerHealth > 50)
		{
			playerHealth = 50;
		}
		healthbar.transform.localScale = new Vector3(healthbar.transform.localScale.x, playerHealth / 50f, healthbar.transform.localScale.z);
		
	}

	

	#endregion
}
