using UnityEngine;
using System.Collections;

public class SlowBubble : MonoBehaviour {
	public GameObject owner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = owner.transform.position;
	}

	void OnCollisionEnter(Collision collision){
		if(collision.collider.gameObject != owner){
			if (collision.collider.gameObject.GetComponent<BubActivator>()){
				collision.collider.GetComponent<BubActivator>().inBub ();
			}
		}
	}

	void OnCollisionExit(Collision collision){
		if(collision.collider.gameObject != owner){
			if (collision.collider.gameObject.GetComponent<BubActivator>()){
				collision.collider.GetComponent<BubActivator>().outBub ();
			}
		}
	}

	void setOwner (GameObject own) {
		this.owner = own;
	}
}
