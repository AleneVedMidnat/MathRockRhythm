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
		scoreUI.SetActive(true);
		scoreUI.GetComponent<ShowScoreUI>().DisplayScoreUI(playerScore, cool, fine, safe, sad, worst, miss, false);
	}

	#region InputEvents
	// Update is called once per frame
	public void Lane1(InputAction.CallbackContext context)
	{
		m_lane1.KeyPressed();
		m_lane1.GetComponentInChildren<AudioSource>().Play();
	}

	public void Lane2(InputAction.CallbackContext context)
	{
		m_lane2.KeyPressed();
        m_lane2.GetComponentInChildren<AudioSource>().Play();
    }

	public void Lane3(InputAction.CallbackContext context)
	{
		m_lane3.KeyPressed();
        m_lane3.GetComponentInChildren<AudioSource>().Play();
    }

	public void Lane4(InputAction.CallbackContext context)
	{
		m_lane4.KeyPressed();
        m_lane4.GetComponentInChildren<AudioSource>().Play();

    }

	#endregion

	#region ScoreEvents

	private void ScoreEvent(string score)
	{
		//score and health incrementation
		switch (score)
		{
			case "worst":
				playerHealth -= 5;
				worst++;
				//FFFF00
				break;
			case "sad":
				playerHealth -= 3;
				sad++;
				//0000FF
				break;
			case "safe":
				safe++;
				//00FF00
				break;
			case "fine":
				playerHealth += 0;
				playerScore+= 1;
				fine++;
				//00FFFF
				break;
			case "cool":
				playerHealth += 1;
				playerScore += 2;
				cool++;
				//FF00FF
				break;
			case "miss":
				playerHealth -= 2;
				miss++;
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
