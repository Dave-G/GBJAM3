using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject player;
	public GameObject Background;


	// Use this for initialization
	void Start () {
        Screen.SetResolution(160, 144, false);
	}
    //bal
	
	// Update is called once per frame
	void Update () {
        Screen.SetResolution(160, 144, false);
		Vector3 camPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		Vector3 playPos = new Vector3 (this.player.transform.position.x, this.player.transform.position.y, this.transform.position.z);
		playPos.x = Mathf.Clamp (this.player.transform.position.x, this.Background.renderer.bounds.min.x+1.6f/2f,this.Background.renderer.bounds.max.x-1.6f/2f);
		playPos.y = Mathf.Clamp (this.player.transform.position.y, this.Background.renderer.bounds.min.y+1.44f/2f,this.Background.renderer.bounds.max.y-1.44f/2f);
		if((Vector3.Distance (camPos,playPos)>=.3)||(player.transform.position.x != playPos.x)||(playPos.y != player.transform.position.y)){
			this.transform.position = Vector3.Slerp (camPos, playPos, Time.deltaTime*2);
		}
	}
}
