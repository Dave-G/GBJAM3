﻿using UnityEngine;
using System.Collections;

public class TransitLoad : MonoBehaviour {

    private float timer;

	// Use this for initialization
	void Start () {
        timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timer + 3) {
            Application.LoadLevel("Level2");
        }
	
	}
}
