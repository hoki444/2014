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
	public int turn;
	public map dmap;
	character player;
	enemy[] enemies;
	int nowenemy;
	int enemynumber;
	mine[] mines;
	int nowmine;
	int minenumber;
	public GameObject[] maps;
	public GameObject[] enemys;
	public GameObject[] mineobjects;
	public GameObject explosion;
	Object nowobject;
	bool gameclear;
	bool thereismine;
	bool gameover;
	public Texture rect;
	public Texture clear;
	public Texture fail;
	public Texture[] minetexture= new Texture[10];
	// Use this for initialization
	void Start () {
		_camera = Camera.main;
		screenWidth = _camera.pixelWidth;
		screenHeight = _camera.pixelHeight;
		enemies = new enemy[20];
		enemynumber = 0;
		nowenemy = 0;
		mines = new mine[1000];
		minenumber = 0;
		nowmine = 0;
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
		turn = dmap.turn;
	}
	
	// Update is called once per frame
	void Update () {
		clearjudge ();
		turnchange ();
	}
	void OnGUI()
	{
		GUIStyle mgui = new GUIStyle();
		mgui.fontSize = 35;
		GUI.DrawTexture (getRect(0, 0, 0.25, 1),rect);
		for (int ind=0; ind<5; ind++) {
			GUI.DrawTexture (getRect(0.025, 0.2+0.07*ind, 0.07, 0.07),minetexture[ind]);
			GUI.Label (getRect (0.125,0.2+0.07*ind,0.1,0.1), dmap.minenumber[ind].ToString()+"/"+dmap.mine[ind].ToString() , mgui);
		}
		GUI.Label (getRect (0.025,0.1,0.1,0.1), "현재 : ", mgui);
		
		GUI.Label (getRect (0.025,0.55,0.1,0.1), "턴 : "+dmap.turn.ToString(), mgui);
		GUI.DrawTexture (getRect(0.135, 0.10, 0.07, 0.07),minetexture[dmap.nowmine]);
		if (GUI.Button(getRect(0.025, 0.8, 0.2, 0.1), "돌아가기"))
			Application.LoadLevel("mainscreen");
		if(gameclear)
			GUI.DrawTexture (getRect(0.375, 0.40, 0.5, 0.2),clear);
		if(gameover)
			GUI.DrawTexture (getRect(0.375, 0.40, 0.5, 0.2),fail);
	}
	void clearjudge(){
		gameclear = true;
		for (int y=0; y<15; y++) {
			for (int x=0; x<15; x++) {
				if(dmap.mapparts[y,x]==2){
					thereismine=false;
					for(int ind=0;ind<nowmine;ind++){
						if(mines[ind].positionx==x&&mines[ind].positiony==y)
							thereismine=true;
					}
					if(!thereismine)
						gameclear=false;
				}
			}
		}
		if (gameclear) {
			player.state="clear";
		}
		if (player.state == "dead") {
			gameover = true;
			GameObject.Destroy(GameObject.Find ("character"));
		}
	}
	public void plusmine(int positionx,int positiony){
		Vector3 nextpoint = new Vector3 ((float)(-3 + 0.668 * positionx), (float)(4.67 - 0.668 * positiony));
		if(nowmine<1000){
			nowobject = Instantiate (mineobjects[dmap.nowmine], nextpoint, transform.rotation);
			nowobject.name="mine"+minenumber.ToString();
			mines[nowmine]=GameObject.Find ("mine"+minenumber.ToString()).GetComponent<mine>();
			mines[nowmine].positionx=positionx;
			mines[nowmine].positiony=positiony;
			nowmine++;
			minenumber++;
			dmap.minenumber[dmap.nowmine]++;
		}
	}
	public void changemine(int positionx,int positiony){
		Vector3 nextpoint = new Vector3 ((float)(-3 + 0.668 * positionx), (float)(4.67 - 0.668 * positiony));
		for(int ind=0;ind<nowmine;ind++){
			if(positionx==mines[ind].positionx&&positiony==mines[ind].positiony){
				mines[ind].delete();
				for(int ind2=ind;ind2<nowmine-1;ind2++){
					mines[ind2]=mines[ind2+1];
				}
				nowmine--;
				ind--;
			}
		}
		if(nowmine<1000){
			nowobject = Instantiate (mineobjects[2], nextpoint, transform.rotation);
			nowobject.name="mine"+minenumber.ToString();
			mines[nowmine]=GameObject.Find ("mine"+minenumber.ToString()).GetComponent<mine>();
			mines[nowmine].positionx=positionx;
			mines[nowmine].positiony=positiony;
			nowmine++;
			minenumber++;
			dmap.minenumber[2]++;
		}
	}
	public void deleteallunit(int[,] explosions,int explosionnum){
		for(int ind=0; ind<explosionnum; ind++){
			Vector3 nextpoint = new Vector3 ((float)(-3 + 0.668 * explosions[ind,0]), (float)(4.67 - 0.668 * explosions[ind,1]));
			Instantiate(explosion,nextpoint,transform.rotation);
			for(int ind2=0;ind2<nowenemy;ind2++){
				if(explosions[ind,0]==enemies[ind2].positionx&&explosions[ind,1]==enemies[ind2].positiony){
					enemies[ind2].state="dead";
					GameObject.Destroy(enemies[ind2].gameObject);
					for(int ind3=ind2;ind3<nowenemy-1;ind3++){
						enemies[ind3]=enemies[ind3+1];
					}
					nowenemy--;
					ind2--;
				}
			}
			if(player.positionx==explosions[ind,0]&&player.positiony==explosions[ind,1])
				player.state="dead";
		}
	}
	public void moveresult(){
		for(int ind=0;ind<nowmine;ind++){
			if(mines[ind].state!="explosion"&&player.positionx==mines[ind].positionx&&player.positiony==mines[ind].positiony){
				mines[ind].explosiontrigger();
			}
		}
		for(int ind=0;ind<nowenemy;ind++){
			if(player.positionx==enemies[ind].positionx&&player.positiony==enemies[ind].positiony){
				enemies[ind].attack();
			}
		}
	}
	void turnchange(){
		if (turn != dmap.turn) {
			turn=dmap.turn;
			for(int ind=0;ind<nowmine;ind++){
				if(mines[ind]==null||mines[ind].state=="explosion"){
					for(int ind2=ind;ind2<nowmine-1;ind2++){
						mines[ind2]=mines[ind2+1];
					}
					nowmine--;
					ind--;
					continue;
				}
				mines[ind].turnAI(enemies,mines,nowenemy,nowmine);
			}
			for(int ind=0;ind<nowenemy;ind++){
				if(enemies[ind]==null&&enemies[ind].state=="dead"){
					for(int ind2=ind;ind2<nowenemy-1;ind2++){
						enemies[ind2]=enemies[ind2+1];
					}
					nowenemy--;
					ind--;
					continue;
				}
				enemies[ind].turnAI(enemies,mines,nowenemy,nowmine);
			}
			if(dmap.turn%5==0){
				for (int y=0; y<15; y++) {
					for (int x=0; x<15; x++) {
						if(dmap.mapparts[y,x]==3){
							Vector3 nextpoint = new Vector3 ((float)(-3 + 0.668 * x), (float)(4.67 - 0.668 * y));
							if(nowenemy<20){
								nowobject = Instantiate(enemys[0],nextpoint,transform.rotation);
								nowobject.name="enemy"+enemynumber.ToString();
								enemies[nowenemy]=GameObject.Find ("enemy"+enemynumber.ToString()).GetComponent<enemy>();
								enemies[nowenemy].positionx=x;
								enemies[nowenemy].positiony=y;
								nowenemy++;
								enemynumber++;
							}
						}
					}
				}
			}
		}
	}
	Rect getRect(double x, double y, double w, double h)
	{
		return new Rect((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
	}
}
