using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject owner;
    public int damage;
    private bool grounded;
	public float myDt, done, gravity;
	public Vector3 direction;

	// Use this for initialization
	public void setup (float Vel, Vector3 Dir, GameObject owner) {
		done = 1f;
		gravity = 10f;
		this.owner = owner;
		direction = Dir/Dir.magnitude;
		this.rigidbody.velocity = direction * Vel * myDt * Time.fixedDeltaTime*done;
	}   

	void Start (){
		}
	
	// Update is called once per frame
	void Update (){
		myDt = this.GetComponent<BubActivator> ().getDT ();
		move ();
	}

	public void move(){
		this.rigidbody.velocity -= new Vector3(0,myDt * Time.deltaTime * gravity * done,0);
	}

    void OnCollisionEnter(Collision collision){

        if ((collision.collider.gameObject.layer == 9) || collision.collider.gameObject.layer == 13){
            //Destroy(this.rigidbody);
			rigidbody.velocity *= 0;
			done = 0;
			this.rigidbody.angularVelocity = Vector3.zero;
            Destroy(this.collider);
			grounded = true;
            return;
        }
        
        /*Keep this for when you have multiple enemies firing to prevent friendly fire
         or if player somehow runs into own weapon
         */
        if (owner.tag == collision.collider.gameObject.tag){
            Physics.IgnoreCollision(collider, collision.collider);
            return;
        }

        else{
            if (collision.collider.gameObject.tag.Contains("Player")){
                collision.collider.gameObject.GetComponent<PlayerCont>().takeDamage(damage);
            }
            else if (collision.collider.gameObject.tag.Contains("Enemy")){
                collision.collider.gameObject.GetComponent<EnemyController>().takeDamage(damage);
            }
            if (!grounded){
                Destroy(this.gameObject);
            }
        }
    }

}
