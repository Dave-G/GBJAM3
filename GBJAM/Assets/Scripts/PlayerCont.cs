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
		layerswap ();
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
		float leftRight = velocity*Input.GetAxis ("Horizontal");
		if (leftRight < 0) {
			this.right = -1;
		}
		if(leftRight > 0){
			this.right = 1;
		}
		this.moveDir.x = 0;
		if(Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)){
			this.moveDir.x = velocity*-1;
		}
		else if(Input.GetKey (KeyCode.RightArrow)||Input.GetKey (KeyCode.D)){
			this.moveDir.x = velocity*1;
		}
		this.transform.localScale = new Vector3 (this.right, 1, 1);
		moveDir.y -= gravity * playerDt * Time.deltaTime;
		controller.Move (moveDir * playerDt * Time.deltaTime);
	}

	public void layerswap(){
		if (this.moveDir.y > 0) {
			Physics.IgnoreLayerCollision(9,10,true);
		}
		else {
			Physics.IgnoreLayerCollision (9,10,false);
		}
	}

    public void fire()
    {
        if (Input.GetKeyDown(attackButton))
        {
            GameObject throwInstance = (GameObject) Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
			throwInstance.gameObject.GetComponent<Weapon>().setOwner(this.gameObject);
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

	public int signZero(float numb){
		if(numb < 0){
			return -1;
		}
		else if(numb > 0){
			return 1;
		}
		else{
			return 0;
		}

	}
}

