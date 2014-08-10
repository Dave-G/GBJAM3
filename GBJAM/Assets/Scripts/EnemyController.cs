using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject platform, target,weapon,heart;
	public CharacterController enemyControl;
    public Vector3 moveDir;
    public AudioClip ow;

	public float velocity = 1, baseVel = 1, throwForce = 1500f, closeDist;
    private float canTurn, turnTime = 1f;

    private float timer = 0f, throwTime = .5f, timer2;

	public bool throwing = false;
    [HideInInspector]
    public int health, right = 1;
    [HideInInspector]
    public bool dead;
    [HideInInspector]
    public Animator anim;

	//public bool chargin;

	// Use this for initialization
	void Start () {
        health = 4;
        moveDir = new Vector3(1f, 0, 0);
        enemyControl = GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        dead = false;
        closeDist = .5f;
        anim.SetBool("Moving", true);
        this.audio.pitch += .5f;
	}
	
	// Update is called once per frame
	void Update () {
		Physics.IgnoreLayerCollision (12,12);
		Physics.IgnoreLayerCollision (12,9);
		this.right = Mathf.RoundToInt (Mathf.Sign (this.moveDir.x));
		//myDt = this.GetComponent<BubActivator> ().getDT ();
        if (!dead){
            animationUpdate();
            move();
			thrower();
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0f);
        }
	}

    //Only turns if time from last turn < set time
	void patrolPlatform (){
        velocity = baseVel*.5f;
        if (!platformBounds(this.gameObject) && Time.time > canTurn){
            anim.SetBool("Moving", true);
            moveDir *= -1;
            right *= -1;
            canTurn = Time.time + turnTime;
        }
	}

	void move(){
    	//Normal movement
		//this.chargin = false;
		this.transform.localScale = new Vector3 (this.right, 1f, 1f);
		if(throwing == false){
            anim.SetBool("Moving", true);
		    this.patrolPlatform();
		    //Change position

			enemyControl.Move(moveDir * velocity * Time.deltaTime);
		}
	}
	
	//Health test
    void OnControllerColliderHit(ControllerColliderHit collision){
		if(collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 11){
			this.moveDir *= -1;
			this.right *= -1;
		}

    }
	

    public void takeDamage(int damage){
        if (health < 1) {
			if(Random.Range (1,5)==3){
				Instantiate(heart,this.transform.position,Quaternion.Euler (Vector3.zero));
			}
            anim.SetBool("Dying", true);
            dead = true;
            StartCoroutine(deathTimer());
        }
        else {
            this.audio.PlayOneShot(ow, 1);
            health -= damage;
            anim.SetBool("Hurting", true);
            timer2 = Time.time;
        }
    }

    //Return true if object is within platform bounds
    public bool platformBounds(GameObject obj){
        CharacterController controller = obj.gameObject.GetComponent<CharacterController>();

        if ((obj.gameObject.transform.position.x >= (platform.collider.bounds.max.x - controller.radius))
            || (obj.gameObject.transform.position.x <= (platform.collider.bounds.min.x + controller.radius)
                || Mathf.Abs(obj.transform.position.y - platform.collider.bounds.max.y) >= .5)){

            return false;
        }
        else{
            return true;
        }
    }
/*
    //Like it sounds
    public void charge(){
        //Copies player direction
        moveDir.x = (target.GetComponent<PlayerCont>().moveDir.x);
        velocity = baseVel;

        //if the player is running towards..
        if (target.transform.position.x < this.transform.position.x && moveDir.x == 1){
            moveDir.x = -1;
            right = -1;
        }
            //""""
        else if (target.transform.position.x > this.transform.position.x && moveDir.x == -1){
            moveDir.x = 1;
            right = 1;
        }
            //if the player is moving away
        else if (moveDir.x != 0){
            right = target.GetComponent<PlayerCont>().right;
        }
        //if the player is not moving
        else{
            moveDir.x = right;
        }
    }
    */

    //Stops update() from functioning for set time when called
    IEnumerator deathTimer(){
        this.audio.pitch -= 1;
        this.audio.PlayOneShot(ow, 1);
        yield return new WaitForSeconds(.75f);
        Destroy(this.gameObject);
    }

	public void thrower(){
		this.timer += 1f*Time.deltaTime;
		if (Vector3.Distance (target.transform.position,this.transform.position) <= closeDist && Mathf.Sign ((target.transform.position.x-this.transform.position.x))*this.right > 0 ) {
			if(this.timer>=this.throwTime){
                throwing = true;
				this.timer = 0f;
				GameObject throwInstance = (GameObject) Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
				throwInstance.gameObject.GetComponent<Weapon>().setup (Random.Range (100,200),new Vector3(this.right*1f,1f,0f),10f, this.gameObject);
				Destroy (throwInstance,3f);
                anim.SetTrigger("Throw");
			}
			this.right = Mathf.RoundToInt(Mathf.Sign(target.transform.position.x-this.transform.position.x));
		}
        else if (Vector3.Distance(target.transform.position, this.transform.position) <= closeDist / 2 && Mathf.Sign(target.transform.position.x - this.transform.position.x) * this.right < 0) {
            this.moveDir *= -1;
            this.right *= -1;
        }
        else {
            this.throwing = false;
        }
	}

    void animationUpdate() {
        if (throwing) {
            anim.SetBool("Moving", false);
        }
        else {
            anim.SetBool("Moving", true);
        }
        anim.ResetTrigger("ThrowDagger");
        anim.ResetTrigger("ThrowAxe");
        if (Time.time > timer2 + .5f) {
            anim.SetBool("Hurting", false);
        }
    }
}
