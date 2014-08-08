using UnityEngine;
using System.Collections;

public class PlayerCont : MonoBehaviour {

    private float timer = 0, chargeDecay = .5f, chargeGain = 1f;
    public float velocity, gravity, jumpVel, throwForce, myDt = 1.0f;
    private bool bubbling = false, dead = false;

    [HideInInspector]
    public int health = 100, right = 1;
    [HideInInspector]
    public float charge = 10;

    public GameObject weapon, slowBub, bubInstance;
    public Vector3 moveDir = Vector3.zero;
    public Animator anim;

    public float lastTime = -1f;

    // Use this for initialization
    void Start() {
        anim = this.GetComponent<Animator>();
        charge = 10;
        health = 100;
        right = 1;
        Debug.Log(health);
        Debug.Log(charge);
        Debug.Log(right);
    }

    // Update is called once per frame
    void Update() {
        //this.myDt = this.gameObject.GetComponent<BubActivator> ().getDT ();
        animationUpdate();
        if (dead) {
            StartCoroutine(deathTimer(this.gameObject));
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
        CharacterController controller = GetComponent<CharacterController>();

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
        /*
        if (Input.GetKeyDown (KeyCode.S)){
            this.lastTime = Time.time;
            if((Time.time - this.lastTime)<.2f){
                Debug.Log ("didit");
                this.lastTime = Time.time; 
                Physics.IgnoreLayerCollision (9,10,true);

            }
        }
        */
    }

    public void fire() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            GameObject throwInstance = (GameObject)Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            throwInstance.gameObject.GetComponent<Weapon>().setup(throwForce + velocity, new Vector3(right, 1, 0), this.gameObject);
            Destroy(throwInstance, 3f);

            anim.SetTrigger("Attack");
            anim.SetBool("isAxe", true);
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
        health -= damage;
        if (health <= 0) {
            dead = true;
            anim.Play("tempPlayerDeath");
        }
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
            Destroy(obj);
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
        anim.SetBool("isAxe", false);
        anim.SetFloat("XVelocity", Mathf.Abs(this.moveDir.x));
        anim.SetFloat("YVelocity", this.moveDir.y);
    }
}