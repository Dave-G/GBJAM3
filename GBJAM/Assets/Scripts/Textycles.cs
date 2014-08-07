using UnityEngine;
using System.Collections;

public class Textycles : MonoBehaviour {

    [HideInInspector]
    public bool dispText;

    GUIStyle style;
    public Font font;
    public string heybb;

	// Use this for initialization
	void Start () {
        style = new GUIStyle();
        style.font = font;
        style.normal.textColor = new Color(207f / 255, 222f / 255, 229f / 255, 1f);
        style.fontSize = 20;
        style.fontStyle = FontStyle.Normal;
        style.clipping = TextClipping.Clip;
//         style.fixedHeight = Screen.width/1.1f;
//         style.fixedWidth = Screen.height/1.3f;
        style.wordWrap = true;
        dispText = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        if (dispText)
        {
            heybb = "JAM JAM JAM JAM kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk";
            GUI.Label(new Rect(Screen.width/11f, Screen.height/1.3f, Screen.width/1.2f, Screen.height*.15f), heybb, style);
        }
    }
}
