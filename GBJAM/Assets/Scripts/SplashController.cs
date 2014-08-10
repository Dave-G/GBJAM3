using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {

	private float timer;
	
	// Use this for initialization
	void Start () {
		timer = Time.time;
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timer + 1f) {	
			Application.LoadLevel("Title");
		}		
	}

}