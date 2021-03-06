﻿using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour {

	private float timer;
	private bool goingUp;
	public AudioClip pow;
	public GameObject axe;
	private bool playedPow = false;
	
	// Use this for initialization
	void Start () {
		timer = Time.time;
		Time.timeScale = 1f;
		this.createAxes(50);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timer + .5f && !playedPow){
			if(pow){
				playedPow = true;
				this.audio.PlayOneShot (pow,1);
			}
		}
		if (Time.time > timer + 3f) {	
			Application.LoadLevel("Title");
		}
	}

	// Procedurally drop axes
	void createAxes(int num){
		for (int i=0;i<num;i++){
			Vector3 axePos = new Vector3(this.transform.position.x+Random.Range(-1f,1f),this.transform.position.y+Random.Range(1f,2.5f),this.transform.position.z);
			GameObject axeClone = (GameObject) Instantiate(this.axe, axePos, this.transform.rotation);
			axeClone.transform.Rotate(0, 0, Random.Range(0, 360f));
		}
	}
	

}