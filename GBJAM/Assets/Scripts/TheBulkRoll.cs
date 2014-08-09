﻿using UnityEngine;
using System.Collections;

public class TheBulkRoll : MonoBehaviour {
	public float ThrowPower;
	public GameObject target;
	public GameObject brock;
	public float LastToss = -1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - this.LastToss > 2f){
			if(Mathf.Abs(this.transform.position.y - target.transform.position.y)<.3 && Mathf.Abs (this.transform.position.x - target.transform.position.x)<1f){
				rollRock();
			}
			else{
				rockToss();
			}
		}
	}

	void rockToss(){
		this.LastToss = Time.time;
		float Delx = -1*(target.transform.position.x - this.transform.position.x);
		float calc = Delx * 9.8f / (Mathf.Pow (ThrowPower,2));
		float Theta = Mathf.Asin (calc)/2f;
		Debug.Log (Theta);
		GameObject rockInstance = (GameObject) Instantiate(brock,this.transform.position,Quaternion.Euler(new Vector3(0, 0, 0)));
		if(Mathf.Abs(Theta) <= .15){
			Theta = Mathf.PI/2 - Theta;
		}
		rockInstance.GetComponent<Boulderhaviour>().setup(new Vector3(-1*Mathf.Cos (Theta),Mathf.Sin (Theta),0),ThrowPower,this.gameObject);
		Destroy(rockInstance,3f);
		//rockInstance.GetComponent<Boulderhaviour>().setup(new Vector3(-1,1,0),ThrowPower,this.gameObject);
	}

	void rollRock(){
		this.LastToss = Time.time;
		float Delx = this.transform.position.x-target.transform.position.x;
		GameObject rockInstance = (GameObject) Instantiate (brock,this.transform.position,Quaternion.Euler (new Vector3(0,0,0)));
		rockInstance.GetComponent<Boulderhaviour>().setup (new Vector3(-1*Mathf.Sign(Delx),0,0),ThrowPower/2f,this.gameObject);
		Destroy(rockInstance,3f);
	}
}
