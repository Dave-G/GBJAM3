using UnityEngine;
using System.Collections;

public class Boulderhaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void setup(Vector3 Dir, float Power, GameObject owner){
		this.transform.position = owner.transform.position;
		this.rigidbody.velocity =  (Dir*Power);
		Debug.Log(this.rigidbody.velocity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
