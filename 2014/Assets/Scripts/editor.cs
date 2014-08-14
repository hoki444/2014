using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public class editor : MonoBehaviour {
	private Camera _camera;
	float screenWidth;
	int nowmap=0;
	float screenHeight;
	public Texture[] maptexture= new Texture[10];
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
			nowmap=0;
		}
        if (GUI.Button(getRect(0.085, 0.132, 0.065, 0.087), ""))
        {
			nowmap=1;
		}
        if (GUI.Button(getRect(0.155, 0.132, 0.065, 0.087), ""))
        {
			nowmap=2;
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
		for (int y=0; y<15; y++) {
			for (int x=0; x<15; x++) {
				GUI.DrawTexture(getRect(0.24+0.76/15*x, 0.85/15*y, 0.76/15, 0.85/15),maptexture[dmap.mapparts[y,x]]);
				if(GUI.Button(getRect(0.24+0.76/15*x, 0.85/15*y, 0.76/15, 0.85/15),"")){
					dmap.mapparts[y,x]=nowmap;
				}
			}
		}
		for (int x=0; x<5; x++) {
			GUI.Label (getRect(0.35+0.65/15*(3*x+1), 0.85, 0.65/15, 0.03), dmap.mine[x].ToString(), mgui);
			if(GUI.Button(getRect(0.35+0.65/15*(3*x+2), 0.85, 0.65/15, 0.03),"")){
				dmap.mine[x]++;
			}
			if(GUI.Button(getRect(0.35+0.65/15*(3*x+2), 0.89, 0.65/15, 0.03),"")){
				dmap.mine[x]--;
			}
		}
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
