//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18444
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
public class map
{	
	public int[,] mapparts = new int[15,15];
	public int[,] mineparts = new int[15,15];
	public int[] mine = new int[10];
	public bool[] enemy =new bool[10];
	public int playerx;
	public int playery;
	public int nowmine;
	public int turn;
	public map ()
	{
		for (int y=0; y<15; y++) {
			for (int x=0; x<15; x++) {
				mapparts[y,x]=0;
				mineparts[y,x]=-1;
			}
		}
		for (int x=0; x<10; x++) {
			mine[x]=0;
			enemy[x]=false;
		}
		playerx = 0;
		playery = 0;
		nowmine = 0;
		turn = 0;
	}
}

