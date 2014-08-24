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
	public map dmap;
	character player;
	public GameObject[] maps;
	bool gameclear;
	public Texture rect;
	public Texture[] minetexture= new Texture[10];
	// Use this for initialization
	void Start () {
		_camera = Camera.main;
		screenWidth = _camera.pixelWidth;
		screenHeight = _camera.pixelHeight;
		dmap = new map ();
		StreamReader sr= new StreamReader("playinfo.txt");
		mode = sr.ReadLine();
		stage = int.Parse(sr.ReadLine());
		sr.Close();
		sr = new StreamReader(mode+"map\\"+stage.ToString()+".txt");
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
		for (int y=0; y<15; y++) {
			for (int x=0; x<15; x++) {
				Vector3 nextpoint= new Vector3((float)(-3+0.668*x),(float)(4.67-0.668*y));
				Instantiate (maps [dmap.mapparts[y,x]], nextpoint, transform.rotation);
			}
		}
		player = GameObject.Find ("character").GetComponent<character>();
		player.positionx = dmap.playerx;
		player.positiony = dmap.playery;
	}
	
	// Update is called once per frame
	void Update () {
		gameclear = true;
		for (int y=0; y<15; y++) {
			for (int x=0; x<15; x++) {
				if(dmap.mapparts[y,x]==2&&dmap.mineparts[y,x]==-1)
					gameclear=false;
			}
		}
		if (gameclear) {
			Application.LoadLevel ("mainscreen");
		}
	}
	void OnGUI()
	{
		GUIStyle mgui = new GUIStyle();
		mgui.fontSize = 35;
		GUI.DrawTexture (getRect(0, 0, 0.25, 1),rect);
		for (int ind=0; ind<5; ind++) {
			GUI.DrawTexture (getRect(0.025, 0.2+0.07*ind, 0.07, 0.07),minetexture[ind]);
			GUI.Label (getRect (0.125,0.2+0.07*ind,0.1,0.1), "0/"+dmap.mine[ind].ToString() , mgui);
		}
		GUI.Label (getRect (0.025,0.1,0.1,0.1), "현재 : ", mgui);
		GUI.DrawTexture (getRect(0.125, 0.10, 0.07, 0.07),minetexture[dmap.nowmine]);
		if (GUI.Button(getRect(0.025, 0.8, 0.2, 0.1), "돌아가기"))
			Application.LoadLevel("mainscreen");
	}
	Rect getRect(double x, double y, double w, double h)
	{
		return new Rect((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
