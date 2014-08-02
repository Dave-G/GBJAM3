using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float moveForce;
    public float maxSpeed;

    private Animator animator;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //directional input
        float hDir = Input.GetAxis("Horizontal");

        //checks if input direction != velocity direction || if the player < max speed .. if so add force in hDir
        if (hDir * rigidbody2D.velocity.x < maxSpeed)
        {
            rigidbody2D.AddForce(Vector2.right * hDir * moveForce);
        }

        //checks if player has attained or is > maximum speed.. set velocity = max speed
        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x)*maxSpeed,rigidbody2D.velocity.y);
        }

	}
}
