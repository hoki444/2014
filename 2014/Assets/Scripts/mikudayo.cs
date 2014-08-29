using UnityEngine;
using System.Collections;

public class mikudayo : enemy {
	public override void turnAI(enemy[] enemies,mine[] mines,int nowenemy,int nowmine) {
		if (state == "stand") {
			move ();
			string tempstate=state;
			state="attack";
			if (positionx == player.positionx && positiony == player.positiony)
				attack ();
			state=tempstate;
			minecheck(mines,nowmine);
			for (int ind=0; ind<nowenemy; ind++) {
				if (positionx == enemies [ind].positionx && positiony == enemies [ind].positiony) {
					enemies [ind].knuckback (direction,2,mines,nowmine);
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
	public override void knuckback(int direction,int size, mine[] mines, int nowmine){
	}
}
