using UnityEngine;
using System.Collections;

public class TheBulkRoll : MonoBehaviour {
	public float ThrowPower;
	public GameObject target;
	public GameObject brock;
	public GameObject rubs;
    Animator anim;
    public AudioClip ow;
	public float LastToss = -1;
	public float throwGrav = 5f;
	public int health = 10;
	public Vector3 posInit;
	public int counter = 0;
	public float shakeStart;
	public bool dead = false;
	void Start(){
		this.posInit = this.gameObject.transform.position;
        anim = this.GetComponent<Animator>();
        this.audio.pitch = 1;
	}
	// Update is called once per frame
	void Update () {
        anim.ResetTrigger("Throw");
		if(health <= 4){
			if(this.shakeStart == -1){
				this.shakeStart = Time.time;
			}
			shakeItUP(counter);
			counter+=1;
		}
		if(Time.time - this.LastToss > 2f){
			if(Mathf.Abs(this.transform.position.y - target.transform.position.y)<.1 && Mathf.Abs (this.transform.position.x - target.transform.position.x)<1f){
				rollRock();
			}
			else{
				rockToss();
			}
		}
		if (health < 1){
            this.audio.pitch = .5f;
            this.audio.PlayOneShot(ow, 1);
            dead = true;
			StartCoroutine(deathTimer());
		}
	}

	void rockToss(){
		this.LastToss = Time.time;
		float Delx = -1*(target.transform.position.x - this.transform.position.x);
		float calc = Delx *throwGrav / (Mathf.Pow (ThrowPower,2));
		float Theta = Mathf.Asin (calc)/2f;
		GameObject rockInstance = (GameObject) Instantiate(brock,this.transform.position,Quaternion.Euler(new Vector3(0, 0, 0)));
		if(Mathf.Abs(Theta) <= .2){
			Theta = Mathf.PI/2 - Theta;
		}
        anim.SetTrigger("Throw");
		this.transform.localScale = new Vector3(-1*Mathf.Sign(Delx),1,1);
		rockInstance.GetComponent<Boulderhaviour>().setup(new Vector3(-1*Mathf.Cos (Theta),Mathf.Sin (Theta),0),ThrowPower,throwGrav,this.gameObject);
		Destroy(rockInstance,3f);
		//rockInstance.GetComponent<Boulderhaviour>().setup(new Vector3(-1,1,0),ThrowPower,this.gameObject);
	}

	void rollRock(){
		this.LastToss = Time.time;
		float Delx = this.transform.position.x-target.transform.position.x;
		GameObject rockInstance = (GameObject) Instantiate (brock,this.transform.position,Quaternion.Euler (new Vector3(0,0,0)));
		this.transform.localScale = new Vector3(-1*Mathf.Sign(Delx),1,1);
		rockInstance.GetComponent<Boulderhaviour>().setup (new Vector3(-1*Mathf.Sign(Delx),-.2f,0),ThrowPower,throwGrav,this.gameObject);
        anim.SetTrigger("Throw");
		Destroy(rockInstance,3f);
	}

	public void takeDamage(int damage){
		this.health -= damage;
        anim.SetBool("Hurt", true);
        this.audio.PlayOneShot(ow, 1);
        StartCoroutine(deathTimer());

	}

	public void shakeItUP(int count){
		if ( count % 3 == 0){
			this.transform.position = this.posInit + new Vector3(Random.Range (-.02f,.02f),Random.Range (-.02f,.02f),0);
			int RubNumb = Random.Range (1,5);
			for(int i = 1;i<RubNumb;i++){
				GameObject rubble = (GameObject) Instantiate(rubs,new Vector3(this.transform.position.x + Random.Range(-1.2f,1.2f),1.5f + Random.Range(-1f,.2f),0),Quaternion.Euler(new Vector3(0, 0, 0)));
				Destroy(rubble,2f);
			}
		}
		if(Time.time - this.shakeStart >= 4f){
			GameObject rockInstance1 = (GameObject) Instantiate(brock,this.transform.position,Quaternion.Euler(new Vector3(0, 0, 0)));
			GameObject rockInstance2 = (GameObject) Instantiate(brock,this.transform.position,Quaternion.Euler(new Vector3(0, 0, 0)));
			GameObject rockInstance3 = (GameObject) Instantiate(brock,this.transform.position,Quaternion.Euler(new Vector3(0, 0, 0)));
			GameObject rockInstance4 = (GameObject) Instantiate(brock,this.transform.position,Quaternion.Euler(new Vector3(0, 0, 0)));
			rockInstance1.GetComponent<Boulderhaviour>().setup (new Vector3(-1f,1f,0f),ThrowPower/4,throwGrav,this.gameObject);
			rockInstance2.GetComponent<Boulderhaviour>().setup (new Vector3(1f,-.2f,0f),ThrowPower/4,throwGrav,this.gameObject);
			rockInstance3.GetComponent<Boulderhaviour>().setup (new Vector3(-1f,-.2f,0f),ThrowPower/4,throwGrav,this.gameObject);
			rockInstance4.GetComponent<Boulderhaviour>().setup (new Vector3(1f,1f,0f),ThrowPower/4,throwGrav,this.gameObject);
			Destroy(rockInstance1,3f);
			Destroy(rockInstance2,3f);
			Destroy(rockInstance3,3f);
			Destroy(rockInstance4,3f);
			this.shakeStart = Time.time;
			// if you want baby mode...
			//this.LastToss = Time.time;
		}
	}

	IEnumerator deathTimer(){
        if (dead) {
            yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
        }
        else {
            yield return new WaitForSeconds(1f);
            anim.SetBool("Hurt", false);
        }
	}
}
