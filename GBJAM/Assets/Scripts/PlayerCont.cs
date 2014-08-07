using UnityEngine;
using System.Collections;

public class PlayerCont : MonoBehaviour {

    private float timer, chargeDecay = .5f, chargeGain = 1f;
	public float velocity, gravity, jumpVel, throwForce, myDt = 1.0f;
    private bool bubbling = false, dead = false;

    [HideInInspector]
    public int health = 100, charge = 10, right = 1;

    public GameObject weapon, slowBub, bubInstance;
	public Vector3 moveDir = Vector3.zero;
    public Animator anim;
	
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//this.myDt = this.gameObject.GetComponent<BubActivator> ().getDT ();
        if (dead){
            StartCoroutine(deathTimer());
        }
        if (!dead) {
            move();
            layerswap();
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0f);
            fire();
            bubbleDecay();
        }
	}

    void bubbleDecay() {
        //Lose charge while bubble is active
        if (bubbling && Time.time > timer + chargeDecay && charge != 0) {
            charge -= 1;
            timer = Time.time;
        }
        //Gain charge while bubble is not active
        else if (charge < 10 && Time.time > timer + chargeGain && !bubbling) {
            charge++;
            timer = Time.time;
        }
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
			right = -1;
		}
		if(leftRight > 0){
			right = 1;
		}
		this.moveDir.x = 0;
		if(Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)){
			moveDir.x = velocity*-1;
		}
		else if(Input.GetKey (KeyCode.RightArrow)||Input.GetKey (KeyCode.D)){
			moveDir.x = velocity*1;
		}
        //Change player position
		this.transform.localScale = new Vector3 (right, 1, 1);
		moveDir.y -= gravity * myDt * Time.deltaTime;
		controller.Move (moveDir * myDt * Time.deltaTime);
	}

	public void layerswap(){
		if (moveDir.y > 0) {
			Physics.IgnoreLayerCollision(9,10,true);
		}
		else {
			Physics.IgnoreLayerCollision (9,10,false);
		}
	}

    public void fire(){
        if (Input.GetKeyDown(KeyCode.Z)){
            GameObject throwInstance = (GameObject) Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
			throwInstance.gameObject.GetComponent<Weapon>().setup (throwForce+velocity,new Vector3(right,1,0),this.gameObject);
			Destroy (throwInstance,3f);
        }
		if (Input.GetKeyDown (KeyCode.C)){
			bubInstance = (GameObject) Instantiate (slowBub,transform.position, Quaternion.Euler (new Vector3(0,0,0)));
			bubInstance.gameObject.GetComponent<SlowBubble>().setOwner(this.gameObject);
            bubbling = true;
		}
		if (Input.GetKeyUp (KeyCode.C)){
			Destroy (bubInstance);
            bubbling = false;
		}
    }

    public void takeDamage(int damage){
        health -= damage;
        if (health <= 0){
            dead = true;
            anim.Play("tempPlayerDeath");
        }
    }

    //Force stop update() for set duration then execute
    IEnumerator deathTimer(){
        yield return new WaitForSeconds(.6f);
        Destroy(this.gameObject);
    }
}