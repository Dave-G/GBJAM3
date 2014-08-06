using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int charge;

    public GameObject player;
   // public Font font;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
     //   buildTextGen();
	}

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.Contains("Heart"))
        {
            getHealth();
        }
        else if (gameObject.name.Contains("Charge"))
        {
            getCharge();
        }
        else if (gameObject.name.Contains("Text"))
        {
            anim.SetBool("dispText", true);
        }
	}

    void getHealth()
    {
        currentHealth = player.gameObject.GetComponent<PlayerCont>().health;
        anim.SetInteger("Health", Mathf.Abs(currentHealth));
    }

    void getCharge()
    {
        charge = player.gameObject.GetComponent<PlayerCont>().charge;
        anim.SetInteger("Charge", Mathf.Abs(charge));

    }

//     void buildTextGen()
//     {
//         TextGenerationSettings settings = new TextGenerationSettings();
//         settings.color = Color.white;
//         
//         settings.extents = new Vector2(Camera.main.transform.position.x+30, Camera.main.transform.position.y + 120);
//         settings.pivot = Vector2.zero;
//         settings.richText = true;
//         settings.font = font;
//         settings.wrapMode = TextWrapMode.Wrap;
//         TextGenerator generator = new TextGenerator();
//         generator.Populate("BAM", settings);
//         Debug.Log("drawing text");
//     }

//     void OnGUI()
//     {
//         getCharge();
//         if (charge > 0)
//         {
//             GUI.DrawTextureWithTexCoords(new Rect(30, 30, 124, 13), chargeBar, new Rect(0, .1f * (charge - 1), 1, 1 / 10f));
//         }
//         else
//         {
//             GUI.DrawTexture(new Rect(30, 30, 124, 13), charge0);
//         }
// 
//     }
}
