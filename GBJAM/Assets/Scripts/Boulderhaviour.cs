using UnityEngine;
using System.Collections;

public class Boulderhaviour : MonoBehaviour {
	public GameObject particleBig;
	public GameObject particleSmall;
	public GameObject pebbles;
	public int damage;
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
			createPebbles();
			Destroy(this.gameObject.collider);
			Destroy(this.gameObject);
		}
		if (collision.collider.gameObject.tag.Contains("Player")){
			createParticles();
			collision.collider.gameObject.GetComponent<PlayerCont>().takeDamage(damage);
			Destroy(this.gameObject.collider);
			Destroy(this.gameObject);
		}

	}
	void createParticles(){
		Vector3 dir1 = -1*this.rigidbody.velocity;
		Vector3 dir2 = Quaternion.Euler (0,0,30)*dir1;
		Vector3 dir3 = Quaternion.Euler (0,0,-30)*dir1;
		GameObject Particle1 = (GameObject) Instantiate(this.particleBig, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		GameObject Particle2 = (GameObject) Instantiate(this.particleSmall, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		GameObject Particle3 = (GameObject) Instantiate(this.particleSmall, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		Particle1.GetComponent<ParticleCont>().setup(100,dir1);
		Particle2.GetComponent<ParticleCont>().setup(100,dir2);
		Particle3.GetComponent<ParticleCont>().setup(100,dir3);
		Destroy(Particle1,2f);
		Destroy(Particle2,2f);
		Destroy(Particle3,2f);
		
	}

	void createPebbles(){
		Vector3 dir1 = -1*this.rigidbody.velocity;
		Vector3 dir2 = Quaternion.Euler (0,0,30)*dir1;
		Vector3 dir3 = Quaternion.Euler (0,0,-30)*dir1;
		GameObject rock1 = (GameObject) Instantiate(this.pebbles, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		GameObject rock2 = (GameObject) Instantiate(this.pebbles, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		GameObject rock3 = (GameObject) Instantiate(this.pebbles, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		rock1.GetComponent<ParticleCont>().setup(100,dir1);
		rock2.GetComponent<ParticleCont>().setup(100,dir2);
		rock3.GetComponent<ParticleCont>().setup(100,dir3);
		Destroy(rock1,2f);
		Destroy(rock2,2f);
		Destroy(rock3,2f);
	}

}
