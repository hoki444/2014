using UnityEngine;
using System.Collections;

public class mikumiku : mine {
	int plusturn;

	// Use this for initialization
	void Start () {
		state = "mine";
		plusturn = 0;
		dmap = GameObject.Find ("Main Camera").GetComponent<game> ().dmap;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public override void turnAI(enemy[] enemies,mine[] mines,int nowenemy,int nowmine){
		plusturn++;
		if (plusturn == 10) {
			switch (Random.Range(0,4)) { 
			case 0 :
			{
				if (positionx != 14 && dmap.mapparts [positiony, positionx + 1] != 0) {
					GameObject.Find ("Main Camera").GetComponent<game> ().changemine(positionx + 1, positiony);
				}
				break;
			}
			case 1 :
			{
				if (positionx != 0 && dmap.mapparts [positiony, positionx - 1] != 0) {
					GameObject.Find ("Main Camera").GetComponent<game> ().changemine(positionx - 1, positiony);
				}
				break;
			}
			case 2 :
			{
				if (positiony != 14 && dmap.mapparts [positiony + 1, positionx] != 0) {
					GameObject.Find ("Main Camera").GetComponent<game> ().changemine(positionx, positiony+1);
				}
				break;
			}
			case 3 :
			{
				if (positiony != 0 && dmap.mapparts [positiony - 1, positionx] != 0) {
					GameObject.Find ("Main Camera").GetComponent<game> ().changemine(positionx, positiony-1);
				}
				break;
			}
			default :
			{
				break;
			}
			}
			plusturn=0;
		}
		enemycheck (enemies,nowenemy);
	}
	public override void explosiontrigger(){
		state="explosion";
		dmap.minenumber [2]--;
		int[,] explosions= new int[9,2];
		for (int y=0; y<3; y++) {
			for (int x=0; x<3; x++) {
				explosions [3*y+x, 0] = positionx+x-1;
				explosions [3*y+x, 1] = positiony+y-1;
			}
		}
		GameObject.Find ("Main Camera").GetComponent<game> ().deleteallunit (explosions,9);
		GameObject.Destroy(this.gameObject);
	}
	public override void delete(){
		state="explosion";
		dmap.minenumber [2]--;
		GameObject.Destroy(this.gameObject);
	}
}
