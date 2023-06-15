using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyController : MonoBehaviour
{
	List<GameObject> activePucks;
	public static event System.Action<string> scoringEvent; //Cool, Fine, Safe, Sad, Worst
	SpriteRenderer m_spriteRenderer;

	void Start()
    {
        activePucks= new List<GameObject>();
		m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		activePucks.Add(collision.gameObject);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		activePucks.Remove(collision.gameObject);
		Destroy(collision.gameObject);
		scoringEvent?.Invoke("miss");
		StartCoroutine(KeyPressedColour(new Color32(0xFF, 0x00, 0x00, 0xFF)));
	}

	public void KeyPressed()
	{
		Color inputColor = Color.gray;
		if (activePucks.Any())
		{
			//check how far from the tagret it was and return the 
			float distanceAway = (activePucks[0].transform.position- transform.position).magnitude;
			Destroy(activePucks[0]);
			if (4.5f < distanceAway && distanceAway <= 5f)
			{
				scoringEvent?.Invoke("worst");
				inputColor = new Color32(0xFF, 0x00, 0xFF, 0xFF);
			}
			else if (3.5f < distanceAway && distanceAway <= 4.5f)
			{
				scoringEvent?.Invoke("sad");
				inputColor = new Color32(0x00, 0xFF, 0x00, 0xFF);
			}
			else if (2.5f < distanceAway && distanceAway <= 3.5f)
			{
				scoringEvent?.Invoke("safe");
				inputColor = new Color32(0x00, 0xFF, 0x00, 0xFF);
			}
			else if (1.5f < distanceAway && distanceAway <= 2.5f)
			{
				scoringEvent?.Invoke("fine");
				inputColor = new Color32(0x00, 0x00, 0xFF, 0xFF);
			}
			else if (0f <= distanceAway && distanceAway <= 1.5f)
			{
				scoringEvent?.Invoke("cool");
				inputColor = new Color32(0xFF, 0xFF, 0x00, 0xFF);
			}
		}
		StartCoroutine(KeyPressedColour(inputColor));
	}

	IEnumerator KeyPressedColour(Color inputColor)
	{
		m_spriteRenderer.color = inputColor;
		yield return new WaitForSeconds(0.2f);
		m_spriteRenderer.color = Color.white;
	}
}
