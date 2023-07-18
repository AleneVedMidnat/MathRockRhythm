using Melanchall.DryWetMidi.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuck : MonoBehaviour
{
	[SerializeField] float movementSpeed;
	[SerializeField] GameObject particleEffect;
	// Start is called before the first frame update

	private void Start()
	{
		FindObjectOfType<RhythmEventSystem>().endSong += EndSong;
	}
	private void FixedUpdate()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed, transform.position.z);
	}

	//private void OnDestroy()
	//{
	//	SpawnParticle();
	//}

	//private void SpawnParticle()
	//{
 //       Instantiate(particleEffect, transform.position, Quaternion.identity);
 //   }

	private void EndSong(bool end)
	{
		Destroy(this);
	}
}
