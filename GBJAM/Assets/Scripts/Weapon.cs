using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject owner;
    public int damage;
    private bool grounded;

	// Use this for initialization
	void Start () {
        grounded = false;
	}   
	
	// Update is called once per frame
	void Update () {

	}

    //Need to make collision not affect movement
    void OnCollisionEnter(Collision collision){

        if (collision.collider.gameObject.layer == 9)
        {
            grounded = true;
        }

        else if (((owner.tag == collision.collider.gameObject.tag) || this.grounded == true) && collision.collider.gameObject.layer != 9)
        {
            Debug.Log("doge");
            Physics.IgnoreCollision(this.collider, collision.collider);
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
