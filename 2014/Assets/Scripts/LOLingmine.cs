using UnityEngine;
using System.Collections;

public class LOLingmine : mine {
	int direction;
	int moveturn;
	int time;
	// Use this for initialization
	void Start () {
		state = "mine";
		dmap = GameObject.Find ("Main Camera").GetComponent<game> ().dmap;
		moveturn = 0;
		time = 0;
		direction = GameObject.Find ("character").GetComponent<character> ().direction;
	}
	
	// Update is called once per frame
	void Update () {
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
		if (state == "walk")
		{
			time++;
			if(time>10){
				state="stand";
			}
		}
	}
	public override void turnAI(enemy[] enemies,mine[] mines,int nowenemy,int nowmine){
		moveturn++;
		if (moveturn == 3) {
			switch (direction) { 
			case 0 :
			{
				if (positionx != 14 && dmap.mapparts [positiony, positionx + 1] != 0) {
					positionx++;
					state="walk";
				}
				else
					direction=1;
				break;
			}
			case 1 :
			{
				if (positionx != 0 && dmap.mapparts [positiony, positionx - 1] != 0) {
					positionx--;
					state="walk";
				}
				else
					direction=0;
				break;
			}
			case 2 :
			{
				if (positiony != 14 && dmap.mapparts [positiony + 1, positionx] != 0) {
					positiony++;
					state="walk";
				}
				else
					direction=3;
				break;
			}
			case 3 :
			{
				if (positiony != 0 && dmap.mapparts [positiony - 1, positionx] != 0) {
					positiony--;
					state="walk";
				}
				else
					direction=2;
				break;
			}
			default :
			{
				break;
			}
			}
			moveturn=0;
		}
		for(int ind=0;ind<nowenemy;ind++){
			if(positionx==enemies[ind].positionx&&positiony==enemies[ind].positiony){
				explosiontrigger();
			}
		}
	}
	public override void explosiontrigger(){
		state="explosion";
		dmap.minenumber [1]--;
		int[,] explosions= new int[3,2];
		for (int ind=0; ind<3; ind++) {
			switch (direction) { 
			case 0 :
			{
				explosions [ind, 0] = positionx+ind;
				explosions [ind, 1] = positiony;
				break;
			}
			case 1 :
			{
				explosions [ind, 0] = positionx-ind;
				explosions [ind, 1] = positiony;
				break;
			}
			case 2 :
			{
				explosions [ind, 0] = positionx;
				explosions [ind, 1] = positiony+ind;
				break;
			}
			case 3 :
			{
				explosions [ind, 0] = positionx;
				explosions [ind, 1] = positiony-ind;
				break;
			}
			default :
			{
				break;
			}
			}
		}
		GameObject.Find ("Main Camera").GetComponent<game> ().deleteallunit (explosions,3);
		GameObject.Destroy(this.gameObject);
	}
}
