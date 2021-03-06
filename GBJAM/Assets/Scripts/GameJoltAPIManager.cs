﻿using UnityEngine;
using System.Collections;

public class GameJoltAPIManager : MonoBehaviour {
	
	// ID and privateKey are fixed to this game
	private int gameID = 31746;
	private string privateKey = "21351239d864dfcff8eafca7904cd8ef";
	// Username and userToken are dependent on the user
	private string userName = "none";
	private string userToken = "none";
	private bool trophiesActive = false;
	private uint trophy1 = 10250;
	private uint trophy2 = 10251;
	private uint trophy3 = 10252;
	private int ach1 = 0;
	private int ach2 = 0;
	private int ach3 = 0;

	void Awake () {
		DontDestroyOnLoad(gameObject);
		// Game ID, Private key, Verbose, API version
		GJAPI.Init(gameID, privateKey);
		// Get user from website or force login
		this.autoLogin();
		// this.manualLogin();
		// this.editorLogin();
		this.ach1 = PlayerPrefs.GetInt("ach1");
		this.ach2 = PlayerPrefs.GetInt("ach2");
		this.ach3 = PlayerPrefs.GetInt("ach3");
	}

	void Update(){
		if (Application.loadedLevelName.Contains ("2")&& (ach1 == 0)){
			this.ach1 = 1;
			PlayerPrefs.SetInt ("ach1",1);
			this.unlockTrophy ("Escapist");
		}
		if (Application.loadedLevelName.Contains ("4") && (ach2 == 0)){
			this.ach2 = 1;
			PlayerPrefs.SetInt ("ach2",1);
			this.unlockTrophy ("Survivor");
		}
		if (Application.loadedLevelName.Contains ("5") && (ach3 == 0) && 
		    !GameObject.Find ("bulk")){
			this.ach3 = 1;
			PlayerPrefs.SetInt ("ach3",1);
			this.unlockTrophy ("Brawler");
		}
	}

	// Get user from GJ site
	void autoLogin(){
		GJAPIHelper.Users.GetFromWeb(OnGetFromWeb);
	}

	// Manual login - for outside of GJ site
	void manualLogin(){
		GJAPIHelper.Users.ShowLogin();
	}

	// Supply your own login parameters from the editor
	void editorLogin(){
		GJAPI.Users.Verify(userName, userToken);
	}

	// Callback
	void OnGetFromWeb (string name, string token){
		Debug.Log("User: " + name + "@" + token);
		GJAPI.Users.Verify(name, token);
	}

	void OnEnable() {
		GJAPI.Users.VerifyCallback += OnVerifyUser;
	}
	
	void OnDisable() {
		GJAPI.Users.VerifyCallback -= OnVerifyUser;
	}

	// When user is verified
	void OnVerifyUser(bool success) {
		if (success) {
			Debug.Log("GJ User verified.");
		}
		else{
			Debug.Log("Unable to verify GJ user.");
		}
		// Show a greeting
		GJAPIHelper.Users.ShowGreetingNotification();
	}

	// Show/hide trophies
	public void toggleTrophies(){
		if (this.trophiesActive){
			GJAPIHelper.Trophies.DismissTrophies();
			this.trophiesActive = false;
		}
		else{
			GJAPIHelper.Trophies.ShowTrophies();
			this.trophiesActive = true;
		}
	}

	// Unlock a trophy by name
	public void unlockTrophy(string name){
		if (name.Equals("Escapist")){
			GJAPI.Trophies.Add(this.trophy1);
			GJAPIHelper.Trophies.ShowTrophyUnlockNotification(this.trophy1);
		}
		else if (name.Equals("Survivor")){
			GJAPI.Trophies.Add(this.trophy2);
			GJAPIHelper.Trophies.ShowTrophyUnlockNotification(this.trophy2);
		}
		else if (name.Equals("Brawler")){
			GJAPI.Trophies.Add(this.trophy3);
			GJAPIHelper.Trophies.ShowTrophyUnlockNotification(this.trophy3);
		}
	}

}