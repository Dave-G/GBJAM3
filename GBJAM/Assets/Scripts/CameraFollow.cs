using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
        Screen.SetResolution(160, 144, false);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 camPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		Vector3 playPos = new Vector3 (player.transform.position.x, player.transform.position.y, this.transform.position.z);
		if(Vector3.Distance (camPos,playPos)>=.3){
			this.transform.position = Vector3.Slerp (camPos, playPos, Time.deltaTime*2);
		}
	}
}
