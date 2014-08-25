using UnityEngine;
using System.Collections;

public class bi : enemy {
	public static int mineeatcounter=0;
	public override void turnAI(enemy[] enemies,mine[] mines,int nowenemy,int nowmine) {
		if (state == "stand") {
			move ();
			if (positionx == player.positionx && positiony == player.positiony)
				attack ();
			for (int ind=0; ind<nowmine; ind++) {
				if (positionx == mines [ind].positionx && positiony == mines [ind].positiony) {
					mines [ind].delete ();
					mineeatcounter++;
				}
			}
			if(mineeatcounter>=3){
				mineeatcounter=0;
				GameObject.Find ("Main Camera").GetComponent<game> ().biexplosion();
			}
		} else if (state == "stun") {
			if(stunturn==dmap.turn)
				state="stand";
		}
	}
	public override void attack(){
		mineeatcounter++;
	}
}
