using UnityEngine;
using System.Collections;

public class mikudayo : enemy {
	public override void turnAI(enemy[] enemies,mine[] mines,int nowenemy,int nowmine) {
		if (state != "dead") {
			move ();
			string tempstate=state;
			state="attack";
			if (positionx == player.positionx && positiony == player.positiony)
				attack ();
			state=tempstate;
			for (int ind=0; ind<nowmine; ind++) {
				if (positionx == mines [ind].positionx && positiony == mines [ind].positiony) {
					mines [ind].explosiontrigger();
				}
			}
		}
	}
	public override void attack(){
		if (state != "attack") {
			switch (player.direction) { 
			case 0 :
			{
				direction=1;
				break;
			}
			case 1 :
			{
				direction=0;
				break;
			}
			case 2 :
			{
				direction=3;
				break;
			}
			case 3 :
			{
				direction=2;
				break;
			}
			default :
			{
				break;
			}
			}
				}
		player.knuckback (direction,3);
	}
}
