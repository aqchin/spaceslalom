﻿using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
//using Leap;

public class MainMenu : MonoBehaviour {

	//public UnityEngine.UI.Button butt;
	public GameObject butt;
	public GameObject ButtonHolder;

	//Controller ctrl;
	//Frame frm;

	void Start() {
		GlobalShit.filepath = @"Assets/Levels/competition_7_easy.txt";
		//ctrl = new Controller();
		//GlobalShit.filepath = "";
	}

	bool loaded = false;

	public void LoadGame () {
		float x = 0.12f;
		float y = 0.945f;
		int ind = 0;
		//float x = -Screen.width/2 + Screen.width * 0.05f;
		DirectoryInfo dir = new DirectoryInfo(@"Assets/Levels/");
		FileInfo[] info = dir.GetFiles ("*.txt");
		if (loaded) {
			print ("Please choose a file to load.");
			return;
		}
		foreach (FileInfo f in info) {
			//UnityEngine.UI.Button temp = (UnityEngine.UI.Button) Instantiate (butt, new Vector3(x,y,0f),Quaternion.identity);
			//UnityEngine.UI.Button temp = Instantiate (UnityEngine.UI.Button) as UnityEngine.UI.Button;
			GameObject temp = Instantiate(butt) as GameObject;
			temp.GetComponentInChildren<Text>().text = f.Name;
			temp.name = ButtonHolder.name + " " + ind;
			ind++;
			temp.transform.parent = ButtonHolder.transform;
			temp.transform.position = new Vector3(Screen.width * x, Screen.height * y, 0f);
			//temp.transform.localScale = new Vector3(1- Screen.width * 0.1f, 1f, 1f);
			y -= 0.075f;

			//temp.transform.Translate (Screen.width/2 - Screen.width * 0.15f, -Screen.height/2 + Screen.height * 0.2f, 0);
			//y -= 60f;
			//print (f.Name);
		}
		Destroy (butt);
		loaded = true;

		Button[] bar = ButtonHolder.GetComponentsInChildren<Button> ();;
		foreach (Button bu in bar) {
			GlobalShit.ba.Add (bu);
			UnityEngine.Events.UnityAction action = () => { AssignFile(bu.GetComponentInChildren<UnityEngine.UI.Text>().text); LoadGameLevel(); };
			bu.onClick.AddListener(action);
		}
		//GlobalShit.ba = bar.OfType<Button>().ToList();
	}

	public void StartGame(string level) {
		Application.LoadLevel (level);
	}

	public void EditLevel() {
		Application.LoadLevel ("level_maker");
	}

	private void AssignFile(string name) {
		GlobalShit.filepath = @"Assets/Levels/" + name;
		print (GlobalShit.filepath);
	}

	private void LoadGameLevel() {
		//print ("calling action");
		Application.LoadLevel ("main_scene");
	}
} 