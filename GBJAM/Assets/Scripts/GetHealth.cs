using UnityEngine;
using System.Collections;

public class GetHealth : MonoBehaviour {

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public int currentHealth;

    public GameObject player;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        getHealth();
	}

    void getHealth()
    {
        currentHealth = player.gameObject.GetComponent<PlayerCont>().health;
        anim.SetInteger("Health", Mathf.Abs(currentHealth));
    }
}
