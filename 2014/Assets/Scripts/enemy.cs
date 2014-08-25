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
		if (state != "dead") {
			move ();
			if (positionx == player.positionx && positiony == player.positiony)
				attack ();
			for (int ind=0; ind<nowmine; ind++) {
				if (positionx == mines [ind].positionx && positiony == mines [ind].positiony) {
					mines [ind].explosiontrigger();
				}
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
}
