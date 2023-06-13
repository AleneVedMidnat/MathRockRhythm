using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateNotes : MonoBehaviour
{
	MidiFile midiFile;
	[SerializeField] string midiLocation;
	TempoMap tempoMap;
	List<TimedEvent> noteOnEvents;
	[SerializeField] GameObject notePrefab;
	[SerializeField] Sprite lane1sprite;
	[SerializeField] Sprite lane2sprite;
	[SerializeField] Sprite lane3sprite;
	[SerializeField] Sprite lane4sprite;
	[SerializeField] AudioSource audio;
	[SerializeField] AudioSource playAudio;
	public float delayTime;

	NoteName[] lane1Notes = { NoteName.A, NoteName.ASharp, NoteName.B };
	NoteName[] lane2Notes = { NoteName.C, NoteName.CSharp, NoteName.D };
	NoteName[] lane3Notes = { NoteName.DSharp, NoteName.E, NoteName.F };
	NoteName[] lane4Notes = { NoteName.FSharp, NoteName.G, NoteName.GSharp };

	// Start is called before the first frame update
	void Start()
    {
		midiFile = MidiFile.Read(midiLocation);
		tempoMap = midiFile.GetTempoMap();
		noteOnEvents = new List<TimedEvent>();

		foreach (var timedEvent in midiFile.GetTimedEvents())
		{
			if (timedEvent.Event is NoteOnEvent)
			{
				noteOnEvents.Add(timedEvent);
			}
		}
		Debug.Log(noteOnEvents.Count);

		delayTime = 20f / (0.25f * 50f); 
		foreach (TimedEvent noteStart in noteOnEvents)
		{
			StartCoroutine(SpawnNotes(noteStart));
		}
		audio.Play();
		playAudio.Play();
	}

	IEnumerator SpawnNotes(TimedEvent noteStart)
	{
		//for each note set wait for seconds to the equaivalent timestamp of the note
		
			//convert time of notestart to realtime
			MetricTimeSpan timeToWait = TimeConverter.ConvertTo<MetricTimeSpan>(noteStart.Time, tempoMap);
			float timeInSeconds = timeToWait.Minutes * 60f + timeToWait.Seconds + (float)timeToWait.Milliseconds / 1000f;
		timeInSeconds -= delayTime + (5f/ (0.25f *50f));
			yield return new WaitForSeconds(timeInSeconds);

			Vector2 spawnPoint = new Vector2(0,0);
			Sprite currentSprite = lane1sprite;
			if (noteStart.Event is NoteOnEvent nameCheck)
			{
				if (lane1Notes.Contains<NoteName>(nameCheck.GetNoteName()))
				{
					spawnPoint = new Vector2(-3, 11);
					currentSprite = lane1sprite;
				}
				else if (lane2Notes.Contains<NoteName>(nameCheck.GetNoteName()))
				{
					spawnPoint = new Vector2(-1, 11);
					currentSprite = lane2sprite;
				}
				else if (lane3Notes.Contains<NoteName>(nameCheck.GetNoteName()))
				{
					spawnPoint = new Vector2(1, 11);
					currentSprite = lane3sprite;
				}
				else if (lane4Notes.Contains<NoteName>(nameCheck.GetNoteName()))
				{
					spawnPoint = new Vector2(3, 11);
					currentSprite = lane4sprite;
				}
			}
			GameObject newNote = Instantiate(notePrefab, spawnPoint, Quaternion.identity);
			newNote.GetComponent<SpriteRenderer>().sprite = currentSprite;
			//set it to move here
		

	}

}
