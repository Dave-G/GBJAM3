using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public int damage;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision collision){
    
        if (collision.collider.gameObject.name.Contains("Player"))
        {
           collision.collider.gameObject.GetComponent<PlayerCont>().takeDamage(this.damage);
            Destroy(this.gameObject);
        }

    }


}
