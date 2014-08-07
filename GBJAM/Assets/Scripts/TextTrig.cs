using UnityEngine;
using System.Collections;

public class TextTrig : MonoBehaviour {

    public string message;
    public GameObject textBox;

	// Use this for initialization
	void Start () {
        textBox = GameObject.Find("TextBox");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag.Contains("Player")) {
            textBox.GetComponent<Textycles>().dispText = true;
            textBox.GetComponent<Textycles>().heybb = message;
            Destroy(this.gameObject);
        }
    }
}
