using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject platform;
	public GameObject target;
	public int right = 1;

	public Vector3 moveDir = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PatrolPlatform (){
		if (this.right == 1 && this.transform.position.x <= this.platform.collider.bounds.max.x) {
			moveDir = new Vector3 (1f,0,0);
		}
		if (this.right == -1 && this.transform.position.x >= this.platform.collider.bounds.min.x) {
			moveDir = new Vector3 (-1f,0,0);
		}
	}

	void move(){
		if (Vector3.Distance (this.transform.position, this.target.transform.position) >= .1) {
				this.PatrolPlatform ();
		}
		else{
			this.moveDir = new Vector3 ( 1,0,0);
		}
		//this.transform

	}
}
