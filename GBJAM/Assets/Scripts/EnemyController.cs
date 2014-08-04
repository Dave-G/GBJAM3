using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject platform;
	public GameObject target;
	public int right = 1;
	public float enemyDt;
	public CharacterController enemyControl;
	public Vector3 moveDir = Vector3.zero;

    [HideInInspector]
    public int health;
    [HideInInspector]
    public bool dead;
    [HideInInspector]
    public Animator anim;

    private float canTurn;
    private float turnTime = 1f;

    //Health test
    public int mydamage = 5;

	// Use this for initialization
	void Start () {
        health = 100;
        this.moveDir = new Vector3(1f, 0, 0);
        enemyControl = GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        dead = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (dead)
        {
            StartCoroutine(deathTimer());
        }
        if (!dead)
        {
            this.move();
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0f);
        }
	}

    //Only turns if time from last turn < set time
	void patrolPlatform (){
        enemyDt = .5f;
        if (!platformBounds(this.gameObject) && Time.time > canTurn)
        {
            moveDir *= -1;
            this.right *= -1;
            canTurn = Time.time + turnTime;
        }
	}

	void move(){
		//If player is within 1m and both objects are on the same platform (Need to fix Y value)
		if(((Vector3.Distance (this.transform.position, this.target.transform.position) <= 1) 
            && platformBounds(this.gameObject)) && platformBounds(target.gameObject)){
               
            charge();
		}

        else
        {
            this.patrolPlatform();
        }

		this.transform.localScale =new Vector3 (this.right, 1f, 1f);
		this.enemyControl.Move (moveDir * enemyDt * Time.deltaTime);

	}

    //Health test
    void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.collider.gameObject.tag.Contains("Player"))
        {
            Debug.Log("bam");
            moveDir.x = 0;
            collision.collider.gameObject.GetComponent<PlayerCont>().takeDamage(mydamage);
        }

    }

    public void takeDamage(int damage)
    {
        this.health -= damage;
        if (health <= 0)
        {
            dead = true;
            anim.Play("tempEnemyDeath");
        }
    }
    //Return true if object is not outside platform bounds
    public bool platformBounds(GameObject obj)
    {
        CharacterController controller = obj.gameObject.GetComponent<CharacterController>();
        if ((obj.gameObject.transform.position.x >= (this.platform.collider.bounds.max.x - controller.radius))
            || (obj.gameObject.transform.position.x <= (this.platform.collider.bounds.min.x + controller.radius)
                || Mathf.Abs(obj.transform.position.y - this.platform.collider.bounds.max.y) >= .5))
        {
            
            return false;
        }
        else
        {
            return true;
        }
    }

    //Like it sounds
    public void charge()
    {
        //Copies player direction
        moveDir.x = (this.target.GetComponent<PlayerCont>().moveDir.x);

        enemyDt = 1f;
        //if the player is running towards..
        if (this.target.transform.position.x < this.transform.position.x && moveDir.x == 1)
        {
            moveDir.x = -1;
            this.right = -1;
        }
            //""""
        else if (this.target.transform.position.x > this.transform.position.x && moveDir.x == -1)
        {
            moveDir.x = 1;
            this.right = 1;
        }
            //if the player is moving away
        else if (moveDir.x != 0)
        {
            this.right = this.target.GetComponent<PlayerCont>().right;
        }
        //if the player is not moving
        else
        {
            moveDir.x = this.right;
        }
    }

    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(.6f);
        Destroy(this.gameObject);
    }
}
