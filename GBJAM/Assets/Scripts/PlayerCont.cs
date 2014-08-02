using UnityEngine;
using System.Collections;

public class PlayerCont : MonoBehaviour {
	public float playerDt = 1.0f;
	public float velocity = 15f;
	public float gravity = 15.0f;
	public float jumpVel = 60.0f;
	public float inAir = 0.6f;

    public int health;

	public KeyCode attackButton = KeyCode.Z;

	public bool grounded = false;

    public GameObject weapon;
    public float throwForce;

	public Vector3 moveDir = Vector3.zero;

	public int right = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		move ();
        fire();
	}

	void move(){
		CharacterController controller = GetComponent<CharacterController>();

		if( (controller.collisionFlags & CollisionFlags.Below) != 0){
			moveDir.y = 0f;
			//moveDir = velocity*transform.TransformDirection (moveDir);
			if (Input.GetButton ("Jump") || Input.GetKey (KeyCode.UpArrow)) {
				moveDir.y += jumpVel;
			}
		}

		if ((controller.collisionFlags & CollisionFlags.Above) != 0) {
			moveDir.y *= 0f;
		}

		// add portion for in air dampening when switching directions
		moveDir.x = velocity*Input.GetAxis ("Horizontal");
		if (moveDir.x < 0) {
			this.right = -1;
		}
		if(moveDir.x > 0){
			this.right = 1;
		}
		this.transform.localScale = new Vector3 (this.right, 1, 1);
		moveDir.y -= gravity * playerDt * Time.deltaTime;
		controller.Move (moveDir * playerDt * Time.deltaTime);
	}

    public void fire()
    {
        if (Input.GetKeyDown(attackButton))
        {
            GameObject throwInstance = (GameObject) Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            throwInstance.rigidbody.AddForce(new Vector3(this.right*throwForce, throwForce, 0));
			Destroy (throwInstance,3f);
        }
    }

    public void takeDamage(int damage)
    {
        this.health -= damage;
        if (health <= 0)
        {
            //player.die
        }
    }
}

