using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
	public int positionx;
	public int positiony;
	public character player;
	public int direction;
	public string state;
	public int time;
	public map dmap;
	public int stunturn;
	// Use this for initialization
	void Start () {
		direction = 0;
		time=0;
		state = "stand";
		player = GameObject.Find ("character").GetComponent<character>();
		dmap = GameObject.Find ("Main Camera").GetComponent<game> ().dmap;
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
	public virtual void turnAI(enemy[] enemies,mine[] mines,int nowenemy,int nowmine) {
		if (state == "stand") {
			move ();
			if (positionx == player.positionx && positiony == player.positiony)
				attack ();
			minecheck(mines,nowmine);
		} else if (state == "stun") {
			if(stunturn==dmap.turn)
				state="stand";
		}
	}
	public virtual void minecheck(mine[] mines,int nowmine){
		for (int ind=0; ind<nowmine; ind++) {
			if (positionx == mines [ind].positionx && positiony == mines [ind].positiony) {
				mines [ind].explosiontrigger ();
			}
		}
	}
	public virtual void move(){
		direction = Random.Range (0, 4);
		switch (direction) { 
		case 0 :
		{
			if (positionx != 14 && dmap.mapparts [positiony, positionx + 1] != 0) {
				positionx++;
				state="walk";
			}
			break;
		}
		case 1 :
		{
			if (positionx != 0 && dmap.mapparts [positiony, positionx - 1] != 0) {
				positionx--;
				state="walk";
			}
			break;
		}
		case 2 :
		{
			if (positiony != 14 && dmap.mapparts [positiony + 1, positionx] != 0) {
				positiony++;
				state="walk";
			}
			break;
		}
		case 3 :
		{
			if (positiony != 0 && dmap.mapparts [positiony - 1, positionx] != 0) {
				positiony--;
				state="walk";
			}
			break;
		}
		default :
		{
			break;
		}
		}
	}
	public virtual void attack(){
		player.state="dead";
	}
	public virtual void knuckback(int direction,int size, mine[] mines, int nowmine){
		for (int ind=0; ind<size; ind++) {
			switch (direction) { 
			case 0:
			{
				if (positionx != 14 && dmap.mapparts [positiony, positionx + 1] != 0) {
					positionx++;
				}
				else{
					state="stun";
					time=0;
					stunturn=dmap.turn+1;
				}
				break;
			}
			case 1:
			{
				if (positionx != 0 && dmap.mapparts [positiony, positionx - 1] != 0) {
					positionx--;
				}else{
					state="stun";
					time=0;
					stunturn=dmap.turn+1;
				}
				break;
			}
			case 2:
			{
				if (positiony != 14 && dmap.mapparts [positiony + 1, positionx] != 0) {
					positiony++;
				}else{
					state="stun";
					time=0;
					stunturn=dmap.turn+1;
				}
				break;
			}
			case 3:
			{
				if (positiony != 0 && dmap.mapparts [positiony - 1, positionx] != 0) {
					positiony--;
				}else{
					state="stun";
					time=0;
					stunturn=dmap.turn+1;
				}
				break;
			}
			default :
			{
				break;
			}
			}
			for (int ind2=0; ind2<nowmine; ind2++) {
				if (positionx == mines [ind2].positionx && positiony == mines [ind2].positiony) {
					mines [ind2].explosiontrigger ();
				}
			}
		}
	}
}
