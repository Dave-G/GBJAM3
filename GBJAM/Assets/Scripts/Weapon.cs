using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject owner;
    public int damage;
    private bool grounded;
	public float myDt;
	public float vel;
	public Vector3 direction;
	public float done = 1f;
	public float gravity = 15f;

	// Use this for initialization
	public void setup (float Vel, Vector3 Dir, GameObject owner) {
		this.done = 1f;
		this.gravity = 10f;
		this.owner = owner;
		this.direction = Dir/Dir.magnitude;
		this.vel = Vel;
		this.rigidbody.velocity = this.direction * this.vel * myDt * Time.fixedDeltaTime*this.done;
	}   
	void Start (){
		Physics.IgnoreCollision (this.collider, owner.collider);
		}
	
	// Update is called once per frame
	void Update () {
		this.myDt = this.gameObject.GetComponent<BubActivator> ().getDT ();
		move ();
	}

	public void move(){
		this.rigidbody.velocity -= new Vector3(0,myDt * Time.deltaTime * this.gravity * this.done,0);
	}

    //Need to make collision not affect movement
    void OnCollisionEnter(Collision collision)
    {

        if ((collision.collider.gameObject.layer == 9) || collision.collider.gameObject.layer == 13)
        {
            //Destroy(this.rigidbody);
			this.rigidbody.velocity *= 0;
			this.done = 0;
			this.rigidbody.angularVelocity = Vector3.zero;
            Destroy(this.collider);
			this.grounded = true;
            return;
        }

        if (owner.tag == collision.collider.gameObject.tag)
        {
			Debug.Log("why");
            return;
        }

        else
        {
            if (!owner.tag.Contains("Player") && collision.collider.gameObject.tag.Contains("Player"))
            {
                collision.collider.gameObject.GetComponent<PlayerCont>().takeDamage(this.damage);
            }
            else if (!owner.tag.Contains("Enemy") && collision.collider.gameObject.tag.Contains("Enemy"))
            {
                collision.collider.gameObject.GetComponent<EnemyController>().takeDamage(this.damage);
            }
            if (!grounded)
            {
                Destroy(this.gameObject);
            }
        }
    }

	public void setOwner (GameObject own){
		this.owner = own;
		this.transform.localScale = owner.transform.localScale;
	}


}
