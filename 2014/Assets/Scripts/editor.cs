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
		if (GUI.Button (getRect (0.015, 0.132, 0.065, 0.087), "")) {
		}
        if (GUI.Button(getRect(0.085, 0.132, 0.065, 0.087), ""))
        {
		}
        if (GUI.Button(getRect(0.155, 0.132, 0.065, 0.087), ""))
        {
		}
        if (GUI.Button(getRect(0.0, 0.41, 0.24, 0.12), ""))
        {
        }
        if (GUI.Button(getRect(0.0, 0.527, 0.24, 0.12), ""))
        {
        }
        if (GUI.Button(getRect(0.0, 0.645, 0.24, 0.12), ""))
        {
        }
        if (GUI.Button(getRect(0.0, 0.763, 0.24, 0.237), ""))
        {
            Application.LoadLevel("mainscreen");
        }
        if (GUI.Button(getRect(0.84, 0.925, 0.16, 0.075), ""))
        {
        }
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
