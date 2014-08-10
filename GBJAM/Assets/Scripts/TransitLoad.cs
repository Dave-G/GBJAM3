using UnityEngine;
using System.Collections;

public class TransitLoad : MonoBehaviour {

    private float timer;
	public GameObject axe;

	// Use this for initialization
	void Start () {
        timer = Time.time;
		Time.timeScale = 1f;
		this.createAxes(50);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timer + 2.5f) {
			string level = "Level" + PlayerPrefs.GetInt ("NextLevel").ToString();
            Application.LoadLevel(level);
        }	
	}

	// Procedurally drop axes
	void createAxes(int num){
		for (int i=0;i<num;i++){
			Vector3 axePos = new Vector3(this.transform.position.x+Random.Range(-1f,1f),this.transform.position.y+Random.Range(1f,2.5f),this.transform.position.z);
			GameObject axeClone = (GameObject) Instantiate(this.axe, axePos, this.transform.rotation);
			axeClone.transform.Rotate(0, 0, Random.Range(0, 360f));
		}
	}
}
