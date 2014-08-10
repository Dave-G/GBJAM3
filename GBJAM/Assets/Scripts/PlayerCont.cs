using UnityEngine;
using System.Collections;

public class PlayerCont : MonoBehaviour {

    private float timer = 0, chargeDecay = .5f, chargeGain = 1f;
    public float velocity, gravity, jumpVel, throwForce, myDt = 1.0f;
    private bool bubbling = false, stunned = false;
    public bool dead;

    [HideInInspector]
    public int health = 100, right = 1;
    [HideInInspector]
    public float charge = 10;

    public GameObject weapon, weapon2, slowBub, bubInstance;
    public AudioClip diediedie;
    public Vector3 moveDir = Vector3.zero;
    public Animator anim;

	private float TimeOfDeath = -1f;
	private bool fallThrough;
	public float Xcnt = 0;
	public float lastX = -1f;
	public float lastAtk = -1f;
    public float lastTime = -1f;
	private CharacterController controller;
    // Use this for initialization
    void Start() {
		controller = GetComponent<CharacterController>();
		int level = int.Parse (Application.loadedLevelName.Replace ("Level",""));
		int lastLvl = PlayerPrefs.GetInt ("LastLevel");
        dead = false;
		if(level<lastLvl){
			this.transform.position = GameObject.Find ("SpawnPoint2").transform.position;
		}
		else{
			this.transform.position = GameObject.Find ("SpawnPoint1").transform.position;
		}
		Camera.main.transform.position = this.transform.position;
        anim = this.GetComponent<Animator>();
        charge = 10;
        health = 6;
        right = 1;
        Debug.Log(health);
        Debug.Log(charge);
        Debug.Log(right);
    }

    // Update is called once per frame
    void Update() {
        //this.myDt = this.gameObject.GetComponent<BubActivator> ().getDT ();
        animationUpdate();
		if (stunned && !anim.GetBool ("Dying")){
			moveDir.y -= gravity * myDt * Time.deltaTime;
			controller.Move (new Vector3(0,moveDir.y,0)*myDt*Time.deltaTime);
			        
		}
        if (!stunned) {
            move();
            layerswap();
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0f);
            fire();
            bubbleDecay();
        }
		if(stunned && anim.GetBool ("Dying") && this.TimeOfDeath >= 0f){
			if(Time.time -this.TimeOfDeath >= .5f){
				PlayerPrefs.SetInt ("NextLevel",int.Parse (Application.loadedLevelName.Replace ("Level","")));
				Application.LoadLevel("Transition");
			}
		}
    }

    void bubbleDecay() {
        //Lose charge while bubble is active
        if (bubbling && Time.time > timer + chargeDecay && charge != 0) {
            charge -= 50f * Time.deltaTime;
            timer = Time.time;
        }
        //Gain charge while bubble is not active
        else if (charge < 10f && Time.time > timer + chargeGain && !bubbling) {
            charge++;
            timer = Time.time;
        }
    }

    void move() {

        if ((controller.collisionFlags & CollisionFlags.Below) != 0) {
            moveDir.y = 0f;
            //moveDir = velocity*transform.TransformDirection (moveDir);
            if (Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow)) {
                moveDir.y += jumpVel;
                anim.SetTrigger("Jump");
                anim.SetBool("Grounded", false);
            }
        }

        if ((controller.collisionFlags & CollisionFlags.Above) != 0) {
            moveDir.y *= 0f;
        }

        // add portion for in air dampening when switching directions
        float leftRight = velocity * Input.GetAxis("Horizontal");
        if (leftRight < 0) {
            right = -1;
        }
        if (leftRight > 0) {
            right = 1;
        }
        this.moveDir.x = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            moveDir.x = velocity * -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            moveDir.x = velocity * 1;
        }
        //Change player position
        this.transform.localScale = new Vector3(right, 1, 1);
        moveDir.y -= gravity * myDt * Time.deltaTime;
        controller.Move(moveDir * myDt * Time.deltaTime);
    }

    public void layerswap() {
        if (moveDir.y > 0 && !(Input.GetKeyDown(KeyCode.S))) {
            Physics.IgnoreLayerCollision(9, 10, true);
        }
        else {
            Physics.IgnoreLayerCollision(9, 10, false);
        }
       	if(this.fallThrough == true && Time.time-this.lastTime <= .3){
			Physics.IgnoreLayerCollision (9,10,true);
		}
		else {
			this.fallThrough = false;
		}
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            this.lastTime = Time.time;
            if((Time.time - this.lastTime)<.2f){
                this.lastTime = Time.time; 
				this.fallThrough = true;

            }
        }
    }

    public void fire() {
		if (Input.GetKeyDown(KeyCode.Z) && (Time.time - this.lastAtk)>.3f ) {
			this.lastAtk = Time.time;
			this.Xcnt = 0f;
			this.lastX = -1f;
			GameObject throwInstance = (GameObject)Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
			throwInstance.gameObject.GetComponent<Weapon>().setup(throwForce + velocity, new Vector3(right, 1, 0), 10, this.gameObject);
			Destroy(throwInstance, 3f);
			
			anim.SetTrigger("Attack");
			anim.SetBool("isAxe", true);
		}
		if (Input.GetKeyDown(KeyCode.X)&&(Time.time - this.lastAtk)>.3f) {
			this.lastAtk = Time.time;
			if((Time.time-this.lastX)<.6f){
				this.Xcnt += 1;
			}
			else{
				this.Xcnt = 1;
			}
			this.lastX = Time.time;
			GameObject throwInstance = (GameObject)Instantiate(weapon2, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
			throwInstance.gameObject.GetComponent<Weapon>().setup(throwForce*1.5f + velocity, new Vector3(right, .2f, 0), 5, this.gameObject);
			Destroy(throwInstance, 3f);
			if(this.Xcnt >= 3){
				this.Xcnt = 0;
				GameObject throwInstance2 = (GameObject)Instantiate(weapon2,transform.position+(new Vector3(0,.05f,0)),Quaternion.Euler(Vector3.zero));
				GameObject throwInstance3 = (GameObject)Instantiate(weapon2,transform.position+(new Vector3(0,-.05f,0)),Quaternion.Euler(Vector3.zero));
				throwInstance2.gameObject.GetComponent<Weapon>().setup (throwForce*1.5f + velocity, new Vector3(right,.1f,0),8,this.gameObject);
				Destroy(throwInstance2,3f);
				throwInstance3.gameObject.GetComponent<Weapon>().setup (throwForce*1.5f + velocity, new Vector3(right,.1f,0),8,this.gameObject);
				Destroy(throwInstance3,3f);
			}
			anim.SetTrigger("Attack");
			anim.SetBool("isDagger", true);
		}
        if (Input.GetKeyDown(KeyCode.C) && this.charge >= 5) {
            bubInstance = (GameObject)Instantiate(slowBub, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            bubInstance.gameObject.GetComponent<SlowBubble>().setOwner(this.gameObject);
            charge -= 1;
            bubbling = true;
        }
        if (Input.GetKeyUp(KeyCode.C) || this.charge <= 0) {
            if (bubInstance) {
                /*Destroy(bubInstance);*/
                bubInstance.GetComponent<Animator>().SetBool("Byebyebye", false);
                StartCoroutine(deathTimer(bubInstance));
            }
            bubbling = false;
        }
    }

    public void takeDamage(int damage) {
        if (!stunned) {
            stunned = true;
            if (health <= 1) {
                dead = true;
                GameObject.Find("Health Container").GetComponent<UI>().dying();
                anim.SetBool("Dying", true);
				this.TimeOfDeath = Time.time;
				Time.timeScale = .1f;
            }
            else {
                StartCoroutine(deathTimer(this.gameObject));
                health -= damage;
                anim.SetTrigger("Hurt");
            }
        }
        else
            return;
    }

    //Force stop update() for set duration then execute
    IEnumerator deathTimer(GameObject obj) {
        if (obj.name.Contains("slowBubble")) {
            obj.GetComponent<Animator>().SetBool("Byebyebye", true);
            yield return new WaitForSeconds(.05f);
            Destroy(obj);
        }
        else {
            yield return new WaitForSeconds(.5f);
            stunned = false;
            anim.ResetTrigger("Hurt");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit collision) {
        if (collision.collider.gameObject.layer == 13 || collision.collider.gameObject.layer == 9
            || collision.collider.gameObject.layer == 11) {
            anim.SetBool("Grounded", true);
        }
    }

    void animationUpdate() {
        anim.ResetTrigger("Jump");
        if (moveDir.y < -.06) {
            anim.SetBool("Grounded", false);
        }
        anim.SetBool("Stunned", stunned);
        anim.SetBool("isAxe", false);
        anim.SetFloat("XVelocity", Mathf.Abs(this.moveDir.x));
        anim.SetFloat("YVelocity", this.moveDir.y);
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.name.Contains("Heart")) {
            health += 2;
            Destroy(collision.gameObject);
        }
    }
}