using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public int currentHealth, currentCharge;

    public GameObject player;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
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
        if (currentHealth >= 0) {
            anim.SetInteger("Health", Mathf.Abs(currentHealth));
        }
        else
            return;
    }

    void getCharge(){

        currentCharge = Mathf.CeilToInt (player.gameObject.GetComponent<PlayerCont>().charge);
        anim.SetInteger("Charge", Mathf.Abs(currentCharge));

    }
}
