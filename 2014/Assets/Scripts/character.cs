using UnityEngine;
using System.Collections;

public class character : MonoBehaviour {

	public int positionx;
	public int positiony;
	public int direction;
	public Sprite[] motions;
	SpriteRenderer sr;
	map dmap;
	public GameObject wm;
	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();
		sr.sprite = motions [0];
		dmap = GameObject.Find ("Main Camera").GetComponent<game> ().dmap;
	}
	
	// Update is called once per frame
	void Update () {
		if (dmap.mineparts [positiony, positionx] != -1) {
			Application.LoadLevel ("mainscreen");
		}
		Vector3 nextpoint= new Vector3((float)(-3+0.668*positionx),(float)(4.67-0.668*positiony));
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
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (direction==2 &&positiony!=14&&dmap.mapparts[positiony+1,positionx]!=0){
				positiony++;
			}
			else if(direction!=2){
				direction=2;
			}
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (direction==3 &&positiony!=0&&dmap.mapparts[positiony-1,positionx]!=0){
				positiony--;
			}
			else if(direction!=3){
				direction=3;
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (direction==1 &&positionx!=0&&dmap.mapparts[positiony,positionx-1]!=0){
				positionx--;
			}
			else if(direction!=1){
				direction=1;
			}
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (direction==0 &&positionx!=14&&dmap.mapparts[positiony,positionx+1]!=0){
				positionx++;
			}
			else if(direction!=0){
				direction=0;
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			switch (direction) { 
			case 0 :
			{
				if(positionx!=14&&dmap.mapparts[positiony,positionx+1]!=0){
					nextpoint= new Vector3((float)(-3+0.668*(positionx+1)),(float)(4.67-0.668*positiony));
					Instantiate (wm, nextpoint, transform.rotation);
					dmap.mineparts[positiony,positionx+1]=0;
				}
				break;
			}
			case 1 :
			{
				if(positionx!=0&&dmap.mapparts[positiony,positionx-1]!=0){
					nextpoint= new Vector3((float)(-3+0.668*(positionx-1)),(float)(4.67-0.668*positiony));
					Instantiate (wm, nextpoint, transform.rotation);
					dmap.mineparts[positiony,positionx-1]=0;
				}
				break;
			}
			case 2 :
			{
				if(positiony!=14&&dmap.mapparts[positiony+1,positionx]!=0){
					nextpoint= new Vector3((float)(-3+0.668*positionx),(float)(4.67-0.668*(positiony+1)));
					Instantiate (wm, nextpoint, transform.rotation);
					dmap.mineparts[positiony+1,positionx]=0;
				}
				break;
			}
			case 3 :
			{
				if(positiony!=0&&dmap.mapparts[positiony-1,positionx]!=0){
					nextpoint= new Vector3((float)(-3+0.668*positionx),(float)(4.67-0.668*(positiony-1)));
					Instantiate (wm, nextpoint, transform.rotation);
					dmap.mineparts[positiony-1,positionx]=0;
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
			dmap.nowmine=0;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			dmap.nowmine=1;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			dmap.nowmine=2;
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			dmap.nowmine=3;
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			dmap.nowmine=4;
		}
	}
}
