using UnityEngine;
using System.Collections;

public class Textycles : MonoBehaviour {

    [HideInInspector]
    public bool dispText;

    GUIStyle style;
    Animator anim;
    public Font font;
    public string heybb;
    public KeyCode textClear = KeyCode.E;

	// Use this for initialization
	void Start () {
        style = new GUIStyle();
        style.font = font;
        style.normal.textColor = new Color(207f / 255, 222f / 255, 229f / 255, 1f);
        style.fontSize = 20;
        style.fontStyle = FontStyle.Normal;
        style.clipping = TextClipping.Clip;
        style.fixedHeight = Screen.width/1.1f;
        style.fixedWidth = Screen.height/1.3f;
        style.wordWrap = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        if(col.tag.Contains("textTrigger")){
            dispText = true;
        }
    }
    void OnGUI()
    {
        if (dispText)
        {
            anim.SetBool("dispText", dispText);
            GUI.Label(new Rect(Screen.width / 11f, Screen.height / 1.3f, Screen.width / 1.2f, Screen.height * .15f), heybb, style);

            if (Input.GetButtonDown("textClear")) {
                dispText = false;
            }
        }
    }
}
