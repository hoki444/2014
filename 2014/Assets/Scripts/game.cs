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
	map dmap;
	public GameObject[] maps;
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
