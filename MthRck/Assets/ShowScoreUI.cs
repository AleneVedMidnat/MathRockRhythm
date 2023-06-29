using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowScoreUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI rankScoreUI;
	[SerializeField] TextMeshProUGUI scoreUI;
	[SerializeField] TextMeshProUGUI coolScoreUI;
	[SerializeField] TextMeshProUGUI fineScoreUI;
	[SerializeField] TextMeshProUGUI safeScoreUI;
	[SerializeField] TextMeshProUGUI sadScoreUI;
	[SerializeField] TextMeshProUGUI worstScoreUI;
	[SerializeField] TextMeshProUGUI missScoreUI;

	public void DisplayScoreUI(int playerScore, int coolScore, int fineScore, int safeScore, int sadScore, int worstScore, int missScore, bool levelFail)
	{
		float maxscore = ((coolScore + fineScore + safeScore + sadScore + worstScore + missScore) * 2f);
		//decide rank based on score 
		if (playerScore < maxscore * 0.6 || levelFail == true)
		{
			rankScoreUI.text = "Fail";
		}
		else if (playerScore < maxscore * 0.7)
		{
			rankScoreUI.text = "Third-Class";
		}
		else if (playerScore < maxscore * 0.8)
		{
			rankScoreUI.text = "Lower Second-Class";
		}
		else if (playerScore < maxscore * 0.9)
		{
			rankScoreUI.text = "Upper Second-Class";
		}
		else
		{
			rankScoreUI.text = "First Class";
		}

		//change all the UI texts to their equivalents 
		scoreUI.text = playerScore.ToString();
		coolScoreUI.text = coolScore.ToString();
		fineScoreUI.text= fineScore.ToString();
		safeScoreUI.text = safeScore.ToString();
		sadScoreUI.text = sadScore.ToString();
		worstScoreUI.text = worstScore.ToString();
		missScoreUI.text = missScore.ToString();
	}
}
