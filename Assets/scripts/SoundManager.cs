using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip clickSound;

	public AudioClip naziWonSong;
	public AudioClip frenchySong;
	public AudioClip spainWonSong;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StopMusic(){
		audioSource.Stop ();
	}

	public void PauseMusic(){
		audioSource.Pause ();
	}

	public void ResumeMusic(){
		audioSource.UnPause ();
	}

	public void PlayClick(){
		audioSource.PlayOneShot (clickSound);
	}

	public void PlayNaziWonSong(){
		StopMusic ();
		audioSource.PlayOneShot (naziWonSong);
	}

	public void PlayFrenchySong(){
		StopMusic ();
		audioSource.PlayOneShot (frenchySong);
	}

	public void PlaySpainWonSong(){
		StopMusic ();
		audioSource.PlayOneShot (spainWonSong);
	}
}
