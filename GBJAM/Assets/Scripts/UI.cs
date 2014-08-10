using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public int currentHealth, currentCharge;

    private int prevHealth;

    public GameObject player;
    public GameObject healthBar;

	// Use this for initialization
	void Start () {
        if(!gameObject.name.Contains("Health Container")){
            anim = GetComponent<Animator>();
        }
        player = GameObject.Find("Player");
        currentHealth = 6;
        prevHealth = 6;
        healthBar = GameObject.Find("Health Container");
        healthBar.GetComponent<AudioSource>().pitch = 1;
	}

    // Update is called once per frame
    void Update(){
        if (gameObject.name.Contains("Heart")){
            getHealth();
        }
        else if (gameObject.name.Contains("Charge")){
            getCharge();
        }
	}
    
    void getHealth(){

        currentHealth = player.gameObject.GetComponent<PlayerCont>().health;
        if (player.gameObject.GetComponent<PlayerCont>().dead == false) {
            anim.SetInteger("Health", Mathf.Abs(currentHealth));
            if (currentHealth < prevHealth) {
                GameObject.Find("Health Container").GetComponent<AudioSource>().audio.Play();
            }
            prevHealth = currentHealth;
        }
        else {
            return;
        }
    }

    void getCharge(){

        currentCharge = Mathf.CeilToInt (player.gameObject.GetComponent<PlayerCont>().charge);
        anim.SetInteger("Charge", Mathf.Abs(currentCharge));

    }

    public void dying() {
        if (gameObject.name.Contains("Health Container")) {
            this.audio.PlayOneShot(GameObject.Find("Player").GetComponent<PlayerCont>().diediedie, 1);
        }
        else
            return;
    }
}