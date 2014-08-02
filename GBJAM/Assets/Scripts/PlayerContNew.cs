using UnityEngine;
using System.Collections;

public class PlayerContNew : MonoBehaviour {
	public float playerDt = 1.0F;
	public float velocity = 15F;
	public float gravity = 15.0F;
	public float jumpVel = 60.0F;
	public float inAir = 0.6F;

	public bool grounded = false;

	public Vector3 moveDir = Vector3.zero;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		move ();
	}

	void move(){
		CharacterController controller = GetComponent<CharacterController>();

		if(controller.isGrounded == true ){
			moveDir = new Vector3 (velocity*Input.GetAxis ("Horizontal"), 0F, 0F);
			//moveDir = velocity*transform.TransformDirection (moveDir);
			if (Input.GetButton ("Jump")) {
				moveDir.y += jumpVel;
			}
		}
		// add portion for in air dampening when switching directions
		else{
			moveDir.x = velocity*Input.GetAxis ("Horizontal");
		}

		moveDir.y -= gravity * playerDt * Time.deltaTime;
		controller.Move (moveDir * playerDt * Time.deltaTime);
	}
}
