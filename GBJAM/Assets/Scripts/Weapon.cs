using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject owner;
    public int damage;

	// Use this for initialization
	void Start () {
      //  this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0f);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision collision){
        if (owner.tag.Contains("Player") && collision.collider.gameObject.tag.Contains("Player"))
        {
            Physics.IgnoreCollision(this.collider, collision.collider);
        }
        if (owner.tag.Contains("Enemy") && collision.collider.gameObject.tag.Contains("Enemy"))
        {
            Physics.IgnoreCollision(this.collider, collision.collider);
        }
        if (!owner.tag.Contains ("Player") && collision.collider.gameObject.tag.Contains("Player"))
        {
           collision.collider.gameObject.GetComponent<PlayerCont>().takeDamage(this.damage);
            Destroy(this.gameObject);
        }
        if (!owner.tag.Contains("Enemy") && collision.collider.gameObject.tag.Contains("Enemy"))
        {
            collision.collider.gameObject.GetComponent<EnemyController>().takeDamage(this.damage);
            Destroy(this.gameObject);
        }
    }
	public void setOwner (GameObject own){
		this.owner = own;
		this.transform.localScale = owner.transform.localScale;
	}


}
