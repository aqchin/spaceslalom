using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
	public float speed = 25;
	public float sensitivity = 10;
	Controller ctrl;
	Frame frm;
	HandList hands;
	Hand hand_l, hand_r;
	public Text taimu;

	// Use this for initialization
	void Start () {
		ctrl = new Controller ();
		taimu.text = "Time: 0";
	}
	
	// Update is called once per frame
	void Update () {
		if (Spaceshit_Navi.index == -2)
			return;
		taimu.text = "Time: " + Time.timeSinceLevelLoad.ToString ();

		frm = ctrl.Frame ();
		hands = frm.Hands;
		hand_l = hands.Leftmost;
		hand_r = hands.Rightmost;
		Vector pos = hand_r.PalmPosition;

		double pitch = hand_r.Direction.Pitch;
		double yaw = hand_r.Direction.Yaw;
		double roll = hand_r.PalmNormal.Roll;

		//if(hand_r.GrabStrength > 0.85)
			//this.transform.rotation = new Quaternion(pitch, yaw, -roll, 0.3f);

		// Pitch offset
		double pitch_off = 0.25;
		if (pitch > pitch_off) 
			pitch = pitch - pitch_off;
		else if (pitch < - pitch_off /2) 
			pitch = pitch + pitch_off /2;
		else
			pitch = 0;

		// Yaw offset
		double yaw_off = 0.25;
		if (yaw > yaw_off)
			yaw = yaw - yaw_off;
		else if (yaw < - yaw_off)
			yaw = yaw + yaw_off;
		else
			yaw = 0;

		// Roll offset
		double roll_off = 0.5;
		if (roll > roll_off)
			roll = roll - roll_off;
		else if (roll < - roll_off)
			roll = roll + roll_off;
		else
			roll = 0;

		// Rotation
		this.transform.Rotate ((float) -pitch, (float) yaw, (float) roll);

		// Forward & Back Movement
		double vel;
		if (pos.z < -sensitivity) { // Forward
			if(pos.z + sensitivity < -speed) vel = -speed;
			else vel = pos.z - sensitivity;

		} else if(pos.z > sensitivity) { // Backward
			if(pos.z - sensitivity > speed) vel = speed;
			else vel = pos.z - sensitivity;

		} else // Halt
			vel = 0;

		vel = vel * (1 - hand_r.GrabStrength);
		this.rigidbody.velocity = this.transform.forward * (float) -vel;

		/*
		// Left & Right Movement
		if (pos.x < -(sensitivity * 5)) {
			float vel_l;
			if(pos.x + sensitivity * 5 < -speed) vel_l = -speed;
			else vel_l = pos.x - sensitivity * 5;

			this.rigidbody.velocity += this.transform.right * -vel_l;
			
		} else if (pos.x > (sensitivity * 5)) {
			float vel_r;
			if(pos.x - sensitivity * 5 > speed) vel_r = speed;
			else vel_r = pos.x - sensitivity * 5;

			this.rigidbody.velocity += this.transform.right * -vel_r;
		
		} else
			this.rigidbody.velocity += this.transform.right * 0;
		*/
		/*
		if (pos.z < -sensitivity*5 && velocity < speed) {
			velocity++;
			this.rigidbody.velocity = -this.transform.forward * velocity;
		} else if (pos.z > sensitivity*5 && velocity > -speed) {
			velocity--;
			this.rigidbody.velocity = -this.transform.forward * velocity;
		} else {
			if(velocity > 0) velocity--;
			else if(velocity < 0) velocity++;
			this.rigidbody.velocity = -this.transform.forward * velocity;
		}
		*/
		/*
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");
		this.transform.Rotate (-y * sensitivity, x * sensitivity, 0);
		if (Input.GetKey ("q")) {
			this.transform.Rotate (0,0,-sensitivity);
		}
		if (Input.GetKey ("e")) {
			this.transform.Rotate (0,0,sensitivity);
		}
		this.rigidbody.velocity = -this.transform.forward * speed;
		*/
	}
}
