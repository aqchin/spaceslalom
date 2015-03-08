using UnityEngine;
using System.Collections;
using System.IO;

public class FileName : MonoBehaviour {

	void Start () {
		DirectoryInfo dir = new DirectoryInfo("/Users/Yuuki/SpaceThingy/Assets/");
		FileInfo[] info = dir.GetFiles ("*.*");
		foreach (FileInfo f in info) {
			print (f.Name);
		}
	}
	
}