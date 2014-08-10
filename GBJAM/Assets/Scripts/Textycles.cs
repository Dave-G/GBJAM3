using UnityEngine;
using System.Collections;

public class Textycles : MonoBehaviour {

    [HideInInspector]
    public bool dispText;
    public bool dmgText;

    public float prev, delay = 2f;

    [HideInInspector]
    public GameObject lilbruiser;
    [HideInInspector]
    public int dmg;
	private bool played = false;
    GUIStyle dialogueStyle, dmgTextStyle;
    Animator anim;
    public AudioClip doot;

    public Font font;
    public string heybb;

	// Use this for initialization
	void Start () {
        lilbruiser = new GameObject();
        anim = this.GetComponent<Animator>();
        styleSetup(ref dmgTextStyle, false);
        styleSetup(ref dialogueStyle, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void dialogue() {
        //If the player has trigger a text event
        if (dispText) {
            anim.SetBool("dispText", dispText);
            if (Time.time > prev + .17f) {
				if(!this.played){
					this.played = true;
                	this.audio.PlayOneShot(doot, 1);
				}
                prev = Time.time;
            }
            GUI.Label(new Rect(Screen.width / 11f, Screen.height / 1.3f, Screen.width / 1.2f, 
                Screen.height * .17f), heybb, dialogueStyle);

            //If player closes text box
			if (Input.GetKeyDown(KeyCode.Z)||Input.GetKeyDown(KeyCode.Space)) {
                this.audio.Stop();
                dispText = false;
                anim.SetBool("dispText", dispText);
            }
        }
    }
 
//     void damageText() {
//         
//         GUI.Label(new Rect(lilbruiser.transform.localPosition.x, lilbruiser.transform.localPosition.y,
//             Screen.width*.01f, Screen.height*.01f), dmg.ToString(), dmgTextStyle);
//    }

    void OnGUI() {
        dialogue();
//         if (dmgText) {
//             damageText();
//             if (Time.time > prev + delay) {
//                 dmgText = false;
//             }
//         }
    }

    void styleSetup(ref GUIStyle style, bool yup) {
        style = new GUIStyle();
        style.font = font;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Normal;
        if (yup) {
            style.fixedHeight = Screen.width / 1.1f;
            style.fixedWidth = Screen.height / 1.3f;
            style.clipping = TextClipping.Clip;
            style.wordWrap = true;
            style.normal.textColor = new Color(207f / 255, 222f / 255, 229f / 255, 1f);
        }
        else {
            style.normal.textColor = Color.yellow;
            style.fixedHeight = Screen.height;
            style.fixedWidth = Screen.width;
            style.fontSize = 100;
        }
    }
}
