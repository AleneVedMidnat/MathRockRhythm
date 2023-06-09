using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuck : MonoBehaviour
{
	[SerializeField] float movementSpeed;
	// Start is called before the first frame update
	private void FixedUpdate()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed, transform.position.z);
	}
}
