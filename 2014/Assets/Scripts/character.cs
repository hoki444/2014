﻿using UnityEngine;
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
	public GameObject[] mines;
	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();
		sr.sprite = motions [0];
		time = 0;
		state = "stand";
		dmap = GameObject.Find ("Main Camera").GetComponent<game> ().dmap;
	}
	
	// Update is called once per frame
	void Update () {
		if (dmap.mineparts [positiony, positionx] != -1) {
			state="dead";
			GameObject.Find(positionx.ToString()+"mine"+positiony.ToString()).GetComponent<mine>().explosiontrigger();
			dmap.mineparts [positiony, positionx]=-1;
		}
		Vector3 nextpoint = new Vector3 ((float)(-3 + 0.668 * positionx), (float)(4.67 - 0.668 * positiony));
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
		if (state == "stand") {
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
				switch (direction) { 
				case 0:
				{
					if (positionx != 14 && dmap.mapparts [positiony, positionx + 1] != 0) {
						state="mine";
						time=0;
						dmap.turn++;
						nextpoint = new Vector3 ((float)(-3 + 0.668 * (positionx + 1)), (float)(4.67 - 0.668 * positiony));
						nowobject = Instantiate (mines[dmap.nowmine], nextpoint, transform.rotation);
						nowobject.name= (positionx+1).ToString()+"mine"+positiony.ToString();
						GameObject.Find((positionx+1).ToString()+"mine"+positiony.ToString()).GetComponent<mine>().positionx=positionx+1;
						GameObject.Find((positionx+1).ToString()+"mine"+positiony.ToString()).GetComponent<mine>().positiony=positiony;
						dmap.mineparts [positiony, positionx + 1] = dmap.nowmine;
					}
					break;
				}
				case 1:
				{
					if (positionx != 0 && dmap.mapparts [positiony, positionx - 1] != 0) {
						state="mine";
						time=0;
						dmap.turn++;
						nextpoint = new Vector3 ((float)(-3 + 0.668 * (positionx - 1)), (float)(4.67 - 0.668 * positiony));
						nowobject = Instantiate (mines[dmap.nowmine], nextpoint, transform.rotation);
						nowobject.name= (positionx-1).ToString()+"mine"+positiony.ToString();
						GameObject.Find((positionx-1).ToString()+"mine"+positiony.ToString()).GetComponent<mine>().positionx=positionx-1;
						GameObject.Find((positionx-1).ToString()+"mine"+positiony.ToString()).GetComponent<mine>().positiony=positiony;
						dmap.mineparts [positiony, positionx - 1] = dmap.nowmine;
					}
					break;
				}
				case 2:
				{
					if (positiony != 14 && dmap.mapparts [positiony + 1, positionx] != 0) {
						state="mine";
						time=0;
						dmap.turn++;
						nextpoint = new Vector3 ((float)(-3 + 0.668 * positionx), (float)(4.67 - 0.668 * (positiony + 1)));
						nowobject = Instantiate (mines[dmap.nowmine], nextpoint, transform.rotation);
						nowobject.name= positionx.ToString()+"mine"+(positiony+1).ToString();
						GameObject.Find(positionx.ToString()+"mine"+(positiony+1).ToString()).GetComponent<mine>().positionx=positionx;
						GameObject.Find(positionx.ToString()+"mine"+(positiony+1).ToString()).GetComponent<mine>().positiony=positiony+1;
						dmap.mineparts [positiony + 1, positionx] = dmap.nowmine;
					}
					break;
				}
				case 3:
				{
					if (positiony != 0 && dmap.mapparts [positiony - 1, positionx] != 0) {
						state="mine";
						time=0;
						dmap.turn++;
						nextpoint = new Vector3 ((float)(-3 + 0.668 * positionx), (float)(4.67 - 0.668 * (positiony - 1)));
						nowobject = Instantiate (mines[dmap.nowmine], nextpoint, transform.rotation);
						nowobject.name= positionx.ToString()+"mine"+(positiony-1).ToString();
						GameObject.Find(positionx.ToString()+"mine"+(positiony-1).ToString()).GetComponent<mine>().positionx=positionx;
						GameObject.Find(positionx.ToString()+"mine"+(positiony-1).ToString()).GetComponent<mine>().positiony=positiony-1;
						dmap.mineparts [positiony - 1, positionx] = dmap.nowmine;
					}
					break;
				}
				default :
				{
					break;
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
		else if (state == "walk")
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
