using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {
	private float veloc;
	private Vector3 direction;

	public void setup (float Vel, Vector3 Dir) {
		this.direction = Dir/Dir.magnitude;
		this.veloc = Vel;
		this.rigidbody.AddForce (this.veloc*this.direction);
	}  
	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
