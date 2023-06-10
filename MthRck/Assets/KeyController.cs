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
	}

	public void KeyPressed()
	{
		StartCoroutine(KeyPressedColour());
		if (activePucks.Any())
		{
			//check how far from the tagret it was and return the 
			float distanceAway = (activePucks[0].transform.position- transform.position).magnitude;
			Destroy(activePucks[0]);
			if (4f < distanceAway && distanceAway <= 5f)
			{
				scoringEvent?.Invoke("worst");
				Debug.Log("worst");
			}
			else if (3f < distanceAway && distanceAway <= 4f)
			{
				scoringEvent?.Invoke("sad");
				Debug.Log("s");
			}
			else if (2f < distanceAway && distanceAway <= 3f)
			{
				scoringEvent?.Invoke("safe");
				Debug.Log("sa");
			}
			else if (1f < distanceAway && distanceAway <= 2f)
			{
				scoringEvent?.Invoke("fine");
				Debug.Log("f");
			}
			else if (0f <= distanceAway && distanceAway <= 1f)
			{
				scoringEvent?.Invoke("cool");
				Debug.Log("c");
			}
		}
	}

	IEnumerator KeyPressedColour()
	{
		m_spriteRenderer.color = Color.gray;
		yield return new WaitForSeconds(0.2f);
		m_spriteRenderer.color = Color.white;
	}
}
