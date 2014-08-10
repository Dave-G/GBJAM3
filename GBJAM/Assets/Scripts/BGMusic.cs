using UnityEngine;
using System.Collections;

public class BGMusic : MonoBehaviour {

	public AudioClip song1,song2,song3;
	int currentSong = 1;

	// Use this for initialization
	void Start () {
		audio.clip = song1;
		this.audio.Play ();
		currentSong = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(Camera.main){
			this.transform.position = Camera.main.transform.position;
		}
		songChanger();
		if(!this.audio.isPlaying){
			this.audio.Play ();
		}
	}

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}

	void songChanger(){
		if((Application.loadedLevelName.Contains ("Splash")||Application.loadedLevelName.Contains("Title")) && currentSong != 1){
			currentSong = 1;
			audio.clip = song1;
			this.audio.Play();
		}
		else if((Application.loadedLevelName.Contains ("2")||Application.loadedLevelName.Contains ("1")) && currentSong != 2){
			audio.clip = song2;
			this.audio.Play ();
			this.currentSong =2;
		}
		else if((Application.loadedLevelName.Contains ("3")||Application.loadedLevelName.Contains ("5")) && currentSong != 3){
			audio.clip = song3;
			this.audio.Play ();
			this.currentSong =3;
		}
	}

}
