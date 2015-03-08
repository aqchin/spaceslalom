using UnityEngine;
using System.Collections;

public class Spaceshit_Navi : MonoBehaviour {
	public GameObject spaceshit;
	public GameObject MGH;
	public static int index = 0;
	public static int gateSize = -1;
	Vector3 off;

	Transform goal;

	// Use this for initialization
	void Start () {
		off = spaceshit.transform.position - transform.position;
		gateSize = spaceshit.GetComponentInParent<Parser> ().gateSize;
	}
	
	// Update is called once per frame
	void Update () {
		float x = spaceshit.transform.eulerAngles.x;
		float y = spaceshit.transform.eulerAngles.y;
		float z = spaceshit.transform.eulerAngles.z;
		Quaternion q = Quaternion.Euler (x, y, z);

		if (index < 0) {
			goal = spaceshit.transform;
		} else {
			string name = "BILL GATE #" + index;
			goal = MGH.transform.FindChild (name);
		}

		transform.position = spaceshit.transform.position - (q * off);
		transform.LookAt (goal);
		transform.Rotate (new Vector3(90, 0, 0));
	}
}
