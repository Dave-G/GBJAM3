using UnityEngine;
using System.Collections;

public class TextTrig : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag.Contains("Player")) {
            GameObject.Find("TextBox").GetComponent<Textycles>().dispText = true;
            Debug.Log("true");
        }
    }
}
