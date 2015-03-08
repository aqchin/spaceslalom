using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spaceshit_Tracker : MonoBehaviour {
	public GameObject spaceshit;
	Vector3 off;
	public Text taimu;

	// Use this for initialization
	void Start () {
		off = spaceshit.transform.position - transform.position;
		taimu.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		float x = spaceshit.transform.eulerAngles.x;
		float y = spaceshit.transform.eulerAngles.y;
		Quaternion q = Quaternion.Euler (x, y, 0);

		transform.position = spaceshit.transform.position - (q * off);
		transform.LookAt (spaceshit.transform);

		taimu.text = Time.timeSinceLevelLoad.ToString ();
	}
}
