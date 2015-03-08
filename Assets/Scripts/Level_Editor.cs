using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Leap;

public class Level_Editor : MonoBehaviour {
	public GameObject gate;
	public GameObject gate_cursor;
	public float speed = 10;
	public float sensitivity = 15;

	bool dropGate = false;
	int dropTime = 50;
	int dropCount = 0;

	List<Vector3[]> gateList = new List<Vector3[]>();
	List<GameObject> gateObj = new List<GameObject>();

	Controller ctrl;
	Frame frm;
	HandList hands;
	Hand hand_l, hand_r;

	private static string dir = @"Assets/Levels/";

	public bool Write(string s) {
		string file = s;
		string line;
		/*
		StreamWriter outF = new StreamWriter(dir + file + ".txt");
		using (outF) {

		}
		outF.Close ();
		return true;
		*/
		string crap = "";
		for (int n = 0; n < gateList.Count; n++) {
			for (int m = 0; m < 3; m++) {
				crap += gateList [n] [m].x + " " + gateList [n] [m].y + " " + gateList [n] [m].z;
				if(m != 2) crap += " ";
			}
			crap += "\n";
		}
		System.IO.File.WriteAllText (dir + "Custum Thingy" + GlobalShit.track++ + ".txt", crap);
		return true;
	}

	// Use this for initialization
	void Start () {
		//cursor = (GameObject)Instantiate (gate_cursor, new Vector3 (), Quaternion.identity);
		ctrl = new Controller ();
		ctrl.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
		
		Vector3[] v = new Vector3[3];
		v[0] = gate.transform.position; // C Vector
		v[1] = gate.transform.right;    // R Vector
		v[2] = gate.transform.up; 	   // U Vector
		gateList.Add (v);
		//Object.Destroy (gate_cursor);
	}
	
	// Update is called once per frame
	void Update () {

		// Gate Time
		if (dropGate) {
			dropCount++;
			if(dropCount == dropTime) {
				dropCount = 0;
				dropGate = false;
			}
		}

		frm = ctrl.Frame ();
		hands = frm.Hands;
		hand_l = hands.Leftmost;
		hand_r = hands.Rightmost;
		Vector pos = hand_r.PalmPosition;

		// Orientation
		double pitch = hand_r.Direction.Pitch;
		double yaw = hand_r.Direction.Yaw;
		double roll = hand_r.PalmNormal.Roll;
		
		// Pitch offset
		double pitch_off = 1;
		if (pitch > pitch_off) 
			pitch = pitch - pitch_off;
		else if (pitch < - pitch_off / 2) 
			pitch = pitch + pitch_off / 2;
		else
			pitch = 0;
		
		// Yaw offset
		double yaw_off = 1;
		if (yaw > yaw_off)
			yaw = yaw - yaw_off;
		else if (yaw < - yaw_off)
			yaw = yaw + yaw_off;
		else
			yaw = 0;
		
		// Roll offset
		double roll_off = 1;
		if (roll > roll_off)
			roll = roll - roll_off;
		else if (roll < - roll_off)
			roll = roll + roll_off;
		else
			roll = 0;


		if (hand_r.GrabStrength > 0.7) {
			// Rotation
			gate_cursor.rigidbody.velocity = gate_cursor.transform.forward * 0;
			gate_cursor.transform.Rotate ((float)-pitch, (float)yaw, (float)roll);
		} else {
			// Forward & Back Movement
			double vel;
			if (pos.z < -sensitivity) { // Forward
				if (pos.z + sensitivity < -speed)
					vel = -speed;
				else
					vel = pos.z - sensitivity;
				
			} else if (pos.z > sensitivity) { // Backward
				if (pos.z - sensitivity > speed)
					vel = speed;
				else
					vel = pos.z - sensitivity;
				
			} else // Halt
				vel = 0;
			
			vel = vel * (1 - hand_r.GrabStrength);
			gate_cursor.rigidbody.velocity = gate_cursor.transform.forward * (float)-vel;	
		}

		// Drop a gate via left pinch
		if(!dropGate && hand_l.PinchStrength == 1) {
			print ("Adding gate");
			GameObject g = (GameObject)Instantiate (gate, gate_cursor.transform.position, 
			                                        Quaternion.Euler(gate_cursor.transform.eulerAngles));
			g.transform.parent = this.transform;
			
			Vector3[] v = new Vector3[3];
			v[0] = gate_cursor.transform.position; // C Vector
			v[1] = gate_cursor.transform.right;    // R Vector
			v[2] = gate_cursor.transform.up; 	   // U Vector

			gateList.Add (v);
			dropGate = true;
		}

		for(int g = 0; g < frm.Gestures().Count; g++)
		{
			switch (frm.Gestures()[g].Type) {
			case Gesture.GestureType.TYPE_CIRCLE:
				Write ("Hi");
				//Application.LoadLevel("MainMenu");
				Application.Quit ();
				//Handle circle gestures
				break;
			default:
				//Handle unrecognized gestures
				break;
			}
		}
		/*
		// Save and quit
		if(frm.Gestures().Count > 0) {
		}
		*/
	}
}
