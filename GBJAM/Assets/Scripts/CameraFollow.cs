using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player, Background;
    private Vector3 camPos, playPos;
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
        Screen.SetResolution(160, 144, false);
	}

    // Update is called once per frame
	void Update () {
		float playlead = player.GetComponent<PlayerCont>().velocity * player.GetComponent<PlayerCont>().moveDir.x*.6f;
        //Creates a vector for player position
       	camPos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		playPos = new Vector3 (player.transform.position.x, player.transform.position.y, this.transform.position.z);

        //Forces player position to be between background bounds
		playPos.x = Mathf.Clamp (player.transform.position.x+playlead, Background.renderer.bounds.min.x+1.6f/2f, Background.renderer.bounds.max.x-1.6f/2f);
		playPos.y = Mathf.Clamp (player.transform.position.y, Background.renderer.bounds.min.y+1.44f/2f, Background.renderer.bounds.max.y-1.44f/2f);
		
        /*Detects camera's distance from player, if > .3m or the players position is within background bounds,
          move camera to the player while staying within map bounds
         */
        if((Vector3.Distance (camPos,playPos)>=.3)||(player.transform.position.x != playPos.x)||(playPos.y != player.transform.position.y)){
			this.transform.position = Vector3.Slerp (camPos, playPos, Time.deltaTime*2);
		}
	}
}
