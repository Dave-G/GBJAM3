﻿using UnityEngine;
using System.Collections;

public class ParticleCont : MonoBehaviour {
	
	private float veloc;
	private Vector3 direction;
	
	public void setup (float Vel, Vector3 Dir) {
		this.direction = Dir/Dir.magnitude;
		this.veloc = Vel;
		Vector3 force = this.veloc*this.direction;
		if(force != Vector3.zero){
			this.rigidbody.AddForce (this.veloc*this.direction);
		}
	}  
}