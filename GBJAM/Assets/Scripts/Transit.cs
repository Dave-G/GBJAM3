using UnityEngine;
using System.Collections;

public class Transit : MonoBehaviour {
	public int forward;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider){
		int level = int.Parse(Application.loadedLevelName.Replace("Level",""));
		PlayerPrefs.SetInt ("LastLevel",level);
		PlayerPrefs.SetInt ("NextLevel",level+forward*2 -1);
        Application.LoadLevel("Transition");
    }
}
