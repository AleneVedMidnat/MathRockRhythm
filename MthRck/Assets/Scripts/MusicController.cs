using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	// Start is called before the first frame update
	AudioSource m_playAudio;
	[SerializeField] float lowerVolume;

	private void Start()
	{
		m_playAudio = GetComponent<AudioSource>();
		KeyController.scoringEvent += ScoreEvent;
	}

	// Update is called once per frame
	void ScoreEvent(string score)
	{
		
		switch (score)
		{
			case "worst":
				m_playAudio.volume = lowerVolume;
				break;
			case "sad":
				m_playAudio.volume = lowerVolume;
				break;
			case "safe":
				m_playAudio.volume = 1.0f;
				break;
			case "fine":
				m_playAudio.volume = 1.0f;
				break;
			case "cool":
				m_playAudio.volume = 1.0f;
				break;
			case "miss":
				m_playAudio.volume = lowerVolume;
				break;
		}
	}
}
