﻿using UnityEngine;
using System.Collections;

public class mine : MonoBehaviour {
	public int positionx;
	public int positiony;
	public string state;
	public map dmap;
	// Use this for initialization
	void Start () {
		state = "mine";
		dmap = GameObject.Find ("Main Camera").GetComponent<game> ().dmap;
	}
	
	// Update is called once per frame
	void Update () {

	}
	public virtual void turnAI(enemy[] enemies,mine[] mines,int nowenemy,int nowmine){
		for(int ind=0;ind<nowenemy;ind++){
			if(positionx==enemies[ind].positionx&&positiony==enemies[ind].positiony){
				explosiontrigger();
			}
		}
	}
	public virtual void explosiontrigger(){
		state="explosion";
		dmap.minenumber [0]--;
		int[,] explosions= new int[1,2];
		explosions [0, 0] = positionx;
		explosions [0, 1] = positiony;
		GameObject.Find ("Main Camera").GetComponent<game> ().deleteallunit (explosions,1);
		GameObject.Destroy(this.gameObject);
	}
}
