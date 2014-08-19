using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class game : MonoBehaviour {
	private Camera _camera;
	float screenWidth;
	float screenHeight;
	string mode;
	int stage;
	// Use this for initialization
	void Start () {
		_camera = Camera.main;
		screenWidth = _camera.pixelWidth;
		screenHeight = _camera.pixelHeight;
		StreamReader sr= new StreamReader("playinfo.txt");
		mode = sr.ReadLine();
		stage = int.Parse(sr.ReadLine());
		sr.Close();
		sr = new StreamReader(mode+"map\\"+stage.ToString()+".txt");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
