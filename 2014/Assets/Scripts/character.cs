using UnityEngine;
using System.Collections;

public class character : MonoBehaviour {

	public int positionx;
	public int positiony;
	public int direction;
	public Sprite[] motions;
	SpriteRenderer sr;
	map dmap;
	int time;
	Object nowobject;
	public string state;
	Vector3 nextpoint;
	game mygame;
	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();
		sr.sprite = motions [0];
		time = 0;
		state = "stand";
		mygame = GameObject.Find ("Main Camera").GetComponent<game> ();
		dmap = mygame.dmap;
		nextpoint = new Vector3 ((float)(-3 + 0.668 * positionx), (float)(4.67 - 0.668 * positiony));
	}
	
	// Update is called once per frame
	void Update () {
		getnextposition ();
		updateminetile ();
		if (state == "stand")
			getinput();
		else
			showmotion();
		mygame.moveresult ();
	}
	void getnextposition(){		
		nextpoint = new Vector3 ((float)(-3 + 0.668 * positionx), (float)(4.67 - 0.668 * positiony));
		if (state == "walk") {
			switch (direction) { 
			case 0 :
			{
				nextpoint = new Vector3 ((float)(-3 + 0.668 * (positionx-0.5)), (float)(4.67 - 0.668 * positiony));
				break;
			}
			case 1 :
			{
				nextpoint = new Vector3 ((float)(-3 + 0.668 * (positionx+0.5)), (float)(4.67 - 0.668 * positiony));
				break;
			}
			case 2 :
			{
				nextpoint = new Vector3 ((float)(-3 + 0.668 * positionx), (float)(4.67 - 0.668 * (positiony-0.5)));
				break;
			}
			case 3 :
			{
				nextpoint = new Vector3 ((float)(-3 + 0.668 * positionx), (float)(4.67 - 0.668 * (positiony+0.5)));
				break;
			}
			default :
			{
				break;
			}
			}
		}
		transform.position = nextpoint;
	}
	void updateminetile(){
		switch (direction) { 
		case 0 :
		{
			nextpoint= new Vector3((float)(-3+0.668*(positionx+1)),(float)(4.67-0.668*positiony));
			GameObject.Find ("minetile").transform.position = nextpoint;
			break;
		}
		case 1 :
		{
			nextpoint= new Vector3((float)(-3+0.668*(positionx-1)),(float)(4.67-0.668*positiony));
			GameObject.Find ("minetile").transform.position = nextpoint;
			break;
		}
		case 2 :
		{
			nextpoint= new Vector3((float)(-3+0.668*positionx),(float)(4.67-0.668*(positiony+1)));
			GameObject.Find ("minetile").transform.position = nextpoint;
			break;
		}
		case 3 :
		{
			nextpoint= new Vector3((float)(-3+0.668*positionx),(float)(4.67-0.668*(positiony-1)));
			GameObject.Find ("minetile").transform.position = nextpoint;
			break;
		}
		default :
		{
			break;
		}
		}
	}
	void getinput(){
		sr.sprite=motions[0];
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (direction == 2 && positiony != 14 && dmap.mapparts [positiony + 1, positionx] != 0) {
				positiony++;
				state="walk";
				time=0;
				dmap.turn++;
			} else if (direction != 2) {
				direction = 2;
			}
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (direction == 3 && positiony != 0 && dmap.mapparts [positiony - 1, positionx] != 0) {
				positiony--;
				state="walk";
				time=0;
				dmap.turn++;
			} else if (direction != 3) {
				direction = 3;
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (direction == 1 && positionx != 0 && dmap.mapparts [positiony, positionx - 1] != 0) {
				positionx--;
				state="walk";
				time=0;
				dmap.turn++;
			} else if (direction != 1) {
				direction = 1;
			}
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (direction == 0 && positionx != 14 && dmap.mapparts [positiony, positionx + 1] != 0) {
				positionx++;
				state="walk";
				time=0;
				dmap.turn++;
			} else if (direction != 0) {
				direction = 0;
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(dmap.minenumber[dmap.nowmine]<dmap.mine[dmap.nowmine]||dmap.mine[dmap.nowmine]==99){
				switch (direction) { 
				case 0:
				{
					if (positionx != 14 && dmap.mapparts [positiony, positionx + 1] != 0) {
						state="mine";
						time=0;
						dmap.turn++;
						mygame.plusmine(positionx + 1, positiony);
					}
					break;
				}
				case 1:
				{
					if (positionx != 0 && dmap.mapparts [positiony, positionx - 1] != 0) {
						state="mine";
						time=0;
						dmap.turn++;
						mygame.plusmine(positionx - 1, positiony);
					}
					break;
				}
				case 2:
				{
					if (positiony != 14 && dmap.mapparts [positiony + 1, positionx] != 0) {
						state="mine";
						time=0;
						dmap.turn++;
						mygame.plusmine(positionx, positiony+1);
					}
					break;
				}
				case 3:
				{
					if (positiony != 0 && dmap.mapparts [positiony - 1, positionx] != 0) {
						state="mine";
						time=0;
						dmap.turn++;
						mygame.plusmine(positionx, positiony-1);
					}
					break;
				}
				default :
				{
					break;
				}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			dmap.nowmine = 0;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			dmap.nowmine = 1;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			dmap.nowmine = 2;
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			dmap.nowmine = 3;
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			dmap.nowmine = 4;
		}
	}
	void showmotion(){
		if (state == "walk")
		{
			sr.sprite = motions [1];
			time++;
			if(time>10){
				state="stand";
			}
		}
		else if (state == "mine")
		{
			sr.sprite = motions [2];
			time++;
			if(time>10){
				state="stand";
			}
		}
	}
}
