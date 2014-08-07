using UnityEngine;
using System.Collections;

public class BubActivator : MonoBehaviour {
	public float outDt = 1f;
	public float inBubDt = .01f;
	public float myDt = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float getDT(){
			return this.myDt;
	}

	public void inBub(){
		this.myDt = inBubDt;
	}

	public void outBub()
	{
		this.myDt = outDt;
	}
}
