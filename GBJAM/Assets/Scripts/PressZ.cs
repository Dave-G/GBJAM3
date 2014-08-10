using UnityEngine;
using System.Collections;

public class PressZ : MonoBehaviour {
	private bool goingUp = true;

	// Update is called once per frame
	void Update () {
		this.bounceScale();
		this.keyControls();
	}

	// Scale up and down
	void bounceScale(){
		if (this.goingUp){
			if (this.transform.localScale.x < 1.2f){
				this.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
			}
			else{
				this.goingUp = false;
			}
		}
		else{
			if (this.transform.localScale.x > 1f){
				this.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
			}
			else{
				this.goingUp = true;
			}
		}
	}

	// Key controls to swap screen/quit
	void keyControls(){
		if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)){
			PlayerPrefs.SetInt("LastLevel",0);
			PlayerPrefs.SetInt("NextLevel",1);
			Application.LoadLevel("Transition");
		}
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
