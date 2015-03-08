using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

public class Parser : MonoBehaviour {

	List<Vector3[]> map = new List<Vector3[]>(); 
	List<GameObject> gates = new List<GameObject>();
	public GameObject cube;
	public GameObject MASTERGATEHOLDER;
	public bool[] gateSelect;
	public int gateIndex = 0;
	public int gateSize;
	int MOTHEROFALLINDEXES = 0;

	bool isLoaded = false;
	string DIR = GlobalShit.filepath;

	private void printMap() {
		for(int i = 0; i < map.Count; i++) {
			print("C: " + map[i][0].x + " " + map[i][0].y + " " + map[i][0].z);
			print("R: " + map[i][1].x + " " + map[i][1].y + " " + map[i][1].z);
			print("U: " + map[i][2].x + " " + map[i][2].y + " " + map[i][2].z);
		}
	}

	public bool Edit(string file, string rw) {
		string line;

		if (rw.CompareTo ("read") == 0) {
			StreamReader inF = new StreamReader(file, Encoding.Default);
			using (inF) {
				while((line = inF.ReadLine()) != null) {
					string[] str = line.Split (' ');
					float[] flt = new float[str.Length];

					// String to float
					for(int i = 0; i < str.Length; i++) {
						flt[i] = float.Parse(str[i]);
					}

					// Not enough arguments
					if(flt.Length != 9) return false;

					Vector3[] v = new Vector3[3];
					v[0] = new Vector3(flt[0], flt[1], flt[2]); // C Vector
					v[1] = new Vector3(flt[3], flt[4], flt[5]); // R Vector
					v[2] = new Vector3(flt[6], flt[7], flt[8]); // U Vector
					GameObject temp = (GameObject)Instantiate (cube,v[0],Quaternion.identity);
					temp.transform.right = v[1];
					temp.transform.up = v[2];
					temp.transform.name = "BILL GATE #" + MOTHEROFALLINDEXES;
					temp.GetComponentInChildren<Gate_Hitbox>().myIndex = MOTHEROFALLINDEXES;
					MOTHEROFALLINDEXES++;
					temp.transform.parent = MASTERGATEHOLDER.transform;
					map.Add(v);
					gates.Add(temp);
				}
			}

			gateSize = gates.Count;
			gateSelect = new bool[gateSize];
			gateSelect[0] = true;
			Object.Destroy (cube);
			inF.Close ();

		} else if (rw.CompareTo ("write") == 0) {
			//StreamWriter outF = new StreamWriter(file);

		} else
			// Bad input
			return false;

		//printMap ();

		// Nothing bad happened
		return true;
	}

	// Use this for initialization
	void Start () {
		isLoaded = Edit (DIR, "read");
		print ("Parsed: " + isLoaded);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
