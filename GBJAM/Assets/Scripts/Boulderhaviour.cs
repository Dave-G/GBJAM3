using UnityEngine;
using System.Collections;

public class Boulderhaviour : MonoBehaviour {
	private float gravity;
	private float myDt=1;
	public int done = 1;
	public float vel;
	// Use this for initialization\
	public Vector3 direction;
	void Start () {
	
	}

	public void setup(Vector3 Dir, float Power,float gravity, GameObject owner){
		this.direction = Dir;
		this.gravity = gravity;
		this.transform.position = owner.transform.position;
		this.vel =  Power;
		this.rigidbody.velocity = (Dir*Power*myDt*done);
		Debug.Log (Dir*Power*myDt*done);
	}
	
	// Update is called once per frame
	void Update () {
		this.myDt = this.GetComponent<BubActivator> ().getDT ();
		if(this.done != 0){
			this.rigidbody.velocity = new Vector3(this.direction.x * this.vel * this.myDt,this.myDt*this.rigidbody.velocity.y,0);
		}
		this.rigidbody.velocity -= new Vector3(0,this.myDt* this.gravity *Time.fixedDeltaTime,0);
		Debug.Log (rigidbody.velocity);
	}

	void OnCollisionEnter(Collision collision){
		if(collision.collider.gameObject.layer == 11 || collision.collider.gameObject.layer == 9){

			this.done = 0;
		}
	}
}
