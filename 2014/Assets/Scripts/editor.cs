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
	public Texture[] enemytexture= new Texture[10];
	public Texture playert;
	public Texture check;
	public Texture rect;
	string mode="work";
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
		if (mode == "work") {
			if (GUI.Button (getRect (0.015, 0.132, 0.065, 0.087), "")) {
				nowmap = 0;
			}
			if (GUI.Button (getRect (0.085, 0.132, 0.065, 0.087), "")) {
				nowmap = 1;
			}
			if (GUI.Button (getRect (0.155, 0.132, 0.065, 0.087), "")) {
				nowmap = 2;
			}
			if (GUI.Button (getRect (0.0, 0.41, 0.24, 0.12), "")) {
				nowmap = -1;
			}
			if (GUI.Button (getRect (0.0, 0.527, 0.24, 0.12), "")) {
				mode = "save";
			}
			if (GUI.Button (getRect (0.0, 0.645, 0.24, 0.12), "")) {
				mode = "load";
			}
			if (GUI.Button (getRect (0.0, 0.763, 0.24, 0.237), "")) {
				Application.LoadLevel ("mainscreen");
			}
			for (int y=0; y<15; y++) {
				for (int x=0; x<15; x++) {
					GUI.DrawTexture (getRect (0.24 + 0.76 / 15 * x, 0.85 / 15 * y, 0.76 / 15, 0.85 / 15), maptexture [dmap.mapparts [y, x]]);
					if (GUI.Button (getRect (0.24 + 0.76 / 15 * x, 0.85 / 15 * y, 0.76 / 15, 0.85 / 15), "")) {
						if (nowmap != -1)
							dmap.mapparts [y, x] = nowmap;
						else {
							dmap.playerx = x;
							dmap.playery = y;
						}
					}
				}
			}
			GUI.DrawTexture (getRect (0.24 + 0.76 / 15 * dmap.playerx, 0.85 / 15 * dmap.playery, 0.76 / 15, 0.85 / 15), playert);
			for (int x=0; x<5; x++) {
				GUI.Label (getRect (0.35 + 0.65 / 15 * (3 * x + 1), 0.85, 0.65 / 15, 0.03), dmap.mine [x].ToString (), mgui);
				if (GUI.Button (getRect (0.35 + 0.65 / 15 * (3 * x + 2), 0.85, 0.65 / 15, 0.03), "")) {
					dmap.mine [x]++;
					if (dmap.mine [x] >= 100)
						dmap.mine [x] = 0;
				}
				if (GUI.Button (getRect (0.35 + 0.65 / 15 * (3 * x + 2), 0.89, 0.65 / 15, 0.03), "")) {
					dmap.mine [x]--;
					if (dmap.mine [x] < 0)
						dmap.mine [x] = 99;
				}
			}
			for (int x=0; x<5; x++) {
				GUI.DrawTexture (getRect (0.35 + 0.65 / 10 * (2 * x), 0.93, 0.65 / 10, 0.06), enemytexture [x]);
				if (dmap.enemy [x])
					GUI.DrawTexture (getRect (0.35 + 0.65 / 10 * (2 * x + 1), 0.93, 0.65 / 10, 0.06), check);
				if (GUI.Button (getRect (0.35 + 0.65 / 10 * (2 * x + 1), 0.93, 0.65 / 10, 0.06), "")) {
					if (dmap.enemy [x])
						dmap.enemy [x] = false;
					else
						dmap.enemy [x] = true;
				}
			}
		}
		else
		{
			mgui.alignment=TextAnchor.MiddleCenter;
			GUI.DrawTexture (getRect (0.2,0.2,0.6,0.6), rect);
			GUI.Label (getRect (0.45,0.23,0.1,0.07), mode, mgui);
			for (int ind=0; ind<10; ind++) {
				GUI.Label (getRect (0.23,0.3+0.04*ind,0.03,0.04), (ind+1).ToString()+".", mgui);
				if(GUI.Button (getRect (0.27,0.3+0.04*ind,0.47,0.04), "")){
					if(mode=="save"){
						StreamWriter sw= new StreamWriter("costommap\\"+(ind+1).ToString()+".txt");
						string nextstring="";
						for (int y=0; y<15; y++) {
							nextstring="";
							for (int x=0; x<15; x++) {
								nextstring=nextstring+dmap.mapparts [y, x].ToString();
							}
							sw.WriteLine(nextstring);
						}
						nextstring="";
						for (int x=0; x<5; x++) {
							if(dmap.mine[x]<10)
								nextstring=nextstring+"0"+dmap.mine[x].ToString();
							else
								nextstring=nextstring+dmap.mine[x].ToString();
						}
						sw.WriteLine(nextstring);
						nextstring="";
						for (int x=0; x<5; x++) {
							if(dmap.enemy[x])
								nextstring=nextstring+"1";
							else
								nextstring=nextstring+"0";
						}
						sw.WriteLine(nextstring);
						sw.WriteLine(dmap.playerx.ToString());
						sw.WriteLine(dmap.playery.ToString());
						sw.Close();
					}
				}
			}
			GUI.Label (getRect (0.38,0.7,0.25,0.07), "return", mgui);
			if(GUI.Button (getRect (0.38,0.7,0.25,0.07), "")){
				mode="work";
			}
		}
	}
	Rect getRect(double x, double y, double w,double h){
		return new Rect ((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
