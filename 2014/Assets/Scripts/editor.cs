using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public class editor : MonoBehaviour {
    private Camera _camera;
    float screenWidth;
    float screenHeight;
    int nowmap = 0;
	public string[] dates = new string[10];
	public Texture[] maptexture= new Texture[10];
	public Texture[] minetexture= new Texture[10];
	public Texture[] enemytexture= new Texture[10];
	public Texture playert;
	public Texture check;
	public Texture rect;
	public Texture editbase;
	public Texture target;
	public Texture up;
	public Texture down;
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
			GUI.DrawTexture(getRect(0,0,1,1),editbase);
			GUI.DrawTexture(getRect(0.015, 0.132, 0.065, 0.087),maptexture[0]);
			GUI.DrawTexture(getRect(0.085, 0.132, 0.065, 0.087),maptexture[1]);
			GUI.DrawTexture(getRect(0.155, 0.132, 0.065, 0.087),maptexture[2]);
			if(nowmap>=0)
				GUI.DrawTexture(getRect(0.015+0.07*nowmap, 0.132, 0.065, 0.087),target);
			else
				GUI.DrawTexture(getRect (0.0, 0.41, 0.24, 0.12),target);
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
					if (GUI.Button (getRect (0.24 + 0.76 / 15 * x, 0.85 / 15 * y, 0.76 / 15, 0.85 / 15), "")) {
						if (nowmap != -1)
							dmap.mapparts [y, x] = nowmap;
						else {
							dmap.playerx = x;
							dmap.playery = y;
						}
					}
					GUI.DrawTexture (getRect (0.24 + 0.76 / 15 * x, 0.85 / 15 * y, 0.76 / 15, 0.85 / 15), maptexture [dmap.mapparts [y, x]]);
				}
			}
			GUI.DrawTexture (getRect (0.24 + 0.76 / 15 * dmap.playerx, 0.85 / 15 * dmap.playery, 0.76 / 15, 0.85 / 15), playert);
			for (int x=0; x<5; x++) {
				GUI.DrawTexture (getRect (0.35 + 0.65 / 15 * (3 * x), 0.85, 0.65 / 15, 0.06), minetexture [x]);
				GUI.Label (getRect (0.35 + 0.65 / 15 * (3 * x + 1), 0.85, 0.65 / 15, 0.06), dmap.mine [x].ToString (), mgui);
				GUI.DrawTexture (getRect (0.35 + 0.65 / 15 * (3 * x + 2), 0.85, 0.65 / 15, 0.03), up);
				if (GUI.Button (getRect (0.35 + 0.65 / 15 * (3 * x + 2), 0.85, 0.65 / 15, 0.03), "")) {
					dmap.mine [x]++;
					if (dmap.mine [x] >= 100)
						dmap.mine [x] = 0;
				}
				GUI.DrawTexture (getRect (0.35 + 0.65 / 15 * (3 * x + 2), 0.89, 0.65 / 15, 0.03), down);
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
			StreamReader sr2 = new StreamReader("date.txt");
			for (int ind=0; ind<10; ind++) {
				dates[ind]=sr2.ReadLine();
				GUI.Label (getRect (0.27,0.3+0.04*ind,0.47,0.04), dates[ind], mgui);
			}
			sr2.Close();
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
						sw= new StreamWriter("date.txt");
						for(int ind2=0;ind2<10;ind2++){
							if(ind==ind2)
								sw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
							else
								sw.WriteLine(dates[ind2]);
						}
						sw.Close();
					}
					else{
						StreamReader sr= new StreamReader("costommap\\"+(ind+1).ToString()+".txt");
						string nextstring="";
						char[] cstring= new char[100];
						for (int y=0; y<15; y++) {
							nextstring=sr.ReadLine();
							cstring=nextstring.ToCharArray();
							for (int x=0; x<15; x++) {
								dmap.mapparts [y, x]=int.Parse(cstring[x].ToString());
							}
						}
						nextstring=sr.ReadLine();
						cstring=nextstring.ToCharArray();
						for (int x=0; x<5; x++) {
							dmap.mine[x]=int.Parse(cstring[2*x].ToString())*10+int.Parse(cstring[2*x+1].ToString());
						}
						nextstring=sr.ReadLine();
						cstring=nextstring.ToCharArray();
						for (int x=0; x<5; x++) {
							if(cstring[x]=='0')
								dmap.enemy[x]=false;
							else
								dmap.enemy[x]=true;
						}
						dmap.playerx=int.Parse(sr.ReadLine());
						dmap.playery=int.Parse(sr.ReadLine());
						sr.Close();
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
