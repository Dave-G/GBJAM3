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

		if( (controller.collisionFlags & CollisionFlags.Below) != 0){
			moveDir.y = 0F;
			//moveDir = velocity*transform.TransformDirection (moveDir);
			if (Input.GetButton ("Jump") || Input.GetKey (KeyCode.UpArrow)) {
				moveDir.y += jumpVel;
			}
		}

		if ((controller.collisionFlags & CollisionFlags.Above) != 0) {
			moveDir.y *= 0F;
		}

		// add portion for in air dampening when switching directions
		moveDir.x = velocity*Input.GetAxis ("Horizontal");

		moveDir.y -= gravity * playerDt * Time.deltaTime;
		controller.Move (moveDir * playerDt * Time.deltaTime);
	}
}
