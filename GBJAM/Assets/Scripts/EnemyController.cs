using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject platform;
	public GameObject target;
	public int right = 1;
	public float enemyDt = 1f;
	public CharacterController enemyControl;
	public Vector3 moveDir = Vector3.zero;

    public int health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.move ();
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0f);
	}

	void patrolPlatform (){
		if (this.right == 1 && this.transform.position.x <= this.platform.collider.bounds.max.x-this.enemyControl.radius) {
			this.moveDir = new Vector3 (1f,0,0);
		}
		else if (this.right == -1 && this.transform.position.x >= this.platform.collider.bounds.min.x+this.enemyControl.radius) {
			this.moveDir = new Vector3 (-1f,0,0);
		}
		else {
			this.right *= -1;
		}

	}

	void move(){
		enemyControl = GetComponent<CharacterController>();
		if (Vector3.Distance (this.transform.position, this.target.transform.position) >= .5) {
				this.patrolPlatform ();
		}
		else{
			this.moveDir = Vector3.zero;
		}
		this.transform.localScale =new Vector3 (this.right, 1f, 1f);
		this.enemyControl.Move (moveDir * enemyDt * Time.deltaTime);

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
