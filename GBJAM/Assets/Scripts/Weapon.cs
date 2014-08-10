using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject owner;
	public GameObject particleBig;
	public GameObject particleSmall;
    public AudioClip tossitup;
    public int damage;
    private bool grounded;
	public float myDt, done, veloc;
    public Vector3 direction, enemyPos;
	private float gravity;

	// Use this for initialization
	public void setup (float Vel, Vector3 Dir,float gravity, GameObject owner) {
		done = 1f;
		this.gravity = gravity;
		this.owner = owner;
		direction = Dir/Dir.magnitude;
		this.veloc = Vel;
		this.rigidbody.transform.localScale = new Vector3 (Mathf.Sign (Dir.x),1,1);
		this.rigidbody.velocity = direction * Vel * this.myDt * Time.fixedDeltaTime*done;

	}   

	void Start (){
            this.audio.PlayOneShot(tossitup, 1);
		}
	
	// Update is called once per frame
	void Update (){
		this.myDt = this.GetComponent<BubActivator> ().getDT ();

        if (done != 0) {
			this.rigidbody.velocity = new Vector3 (this.direction.x * this.veloc * this.myDt * Time.fixedDeltaTime * done,this.myDt*this.rigidbody.velocity.y,0);
            move();
        }
	}

	public void move(){
		this.rigidbody.velocity -= new Vector3(0,this.myDt * Time.fixedDeltaTime * gravity * done,0);
	}

    void OnCollisionEnter(Collision collision){

        if ((collision.collider.gameObject.layer == 9) || collision.collider.gameObject.layer == 13){
			grounded = true;
            rekt();
            return;
        }
        
        /*Keep this for when you have multiple enemies firing to prevent friendly fire
         or if player somehow runs into own weapon
         */
        if (owner.tag == collision.collider.gameObject.tag || collision.collider.gameObject.tag == this.tag){
            Physics.IgnoreCollision(collider, collision.collider);
            return;
        }

        else{
            if (collision.collider.gameObject.tag.Contains("Player")){
				createParticles();
                collision.collider.gameObject.GetComponent<PlayerCont>().takeDamage(damage);
            }
            else if (collision.collider.gameObject.tag.Contains("Enemy")){
				createParticles();
                collision.collider.gameObject.GetComponent<EnemyController>().takeDamage(damage);
            }
//             enemyPos = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, 0f);
//             GameObject.Find("TextBox").GetComponent<Textycles>().lilbruiser = collision.collider.gameObject;
//             GameObject.Find("TextBox").GetComponent<Textycles>().dmg = damage;
//             GameObject.Find("TextBox").GetComponent<Textycles>().dmgText = true;
//             GameObject.Find("TextBox").GetComponent<Textycles>().prev = Time.time;
            rekt();
        }
    }

    void rekt() {
        if (!grounded) {
            Destroy(this.gameObject);
        }
        else {
            rigidbody.velocity *= 0;
            done = 0;
           // Destroy(this.rigidbody);
            this.rigidbody.angularVelocity = Vector3.zero;
            Destroy(this.collider);
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

}
