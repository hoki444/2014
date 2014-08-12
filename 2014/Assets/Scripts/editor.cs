using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public class editor : MonoBehaviour {
	private Camera _camera;
	float screenWidth;
	float screenHeight;
	map dmap;
	// Use this for initialization
	void Start () {
		_camera = Camera.main;
		screenWidth = _camera.pixelWidth;
		screenHeight = _camera.pixelHeight;
		dmap= new map();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI()
	{
		GUIStyle mgui = new GUIStyle ();
		mgui.fontSize = 30;
		if (GUI.Button (getRect (0.01, 0.79, 0.18, 0.06), "")) {
		}
		if (GUI.Button (getRect (0.01, 0.86, 0.18, 0.06), "")) {
		}
		if (GUI.Button (getRect (0.01, 0.93, 0.18, 0.06), "")) {
		}
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
