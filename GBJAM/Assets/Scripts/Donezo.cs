using UnityEngine;
using System.Collections;

public class Donezo : MonoBehaviour {
	public GameObject finalBulky;
	public GameObject finalText;
	public AudioClip ender;
	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
		finalText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Camera.main.transform.position;
		if(!finalBulky){

			this.renderer.enabled = true;
			if(finalText){
				finalText.SetActive(true);
				if(ender){
					this.audio.PlayOneShot (ender,1);
				}
			}
			if(Input.GetKeyDown (KeyCode.Space)){
				Application.LoadLevel ("Splash");
			}
		}
	}
}
