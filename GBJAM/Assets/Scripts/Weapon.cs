using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject owner;
    public int damage;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y,0);
	}

    void OnCollisionEnter(Collision collision){
    
        if (!owner.tag.Contains ("Player") && collision.collider.gameObject.tag.Contains("Player"))
        {
           collision.collider.gameObject.GetComponent<PlayerCont>().takeDamage(this.damage);
            Destroy(this.gameObject);
        }
        if (!owner.tag.Contains("Enemy") && collision.collider.gameObject.tag.Contains("Enemy"))
        {
            collision.collider.gameObject.GetComponent<PlayerCont>().takeDamage(this.damage);
            Destroy(this.gameObject);
        }
    }
	public void setOwner ( GameObject own){

		this.owner = own;
		this.transform.localScale = owner.transform.localScale;
	}


}
