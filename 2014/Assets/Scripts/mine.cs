using UnityEngine;
using System.Collections;

public class mine : MonoBehaviour {
	public int positionx;
	public int positiony;
	public Sprite[] explosion=new Sprite[10];
	SpriteRenderer sr;
	string state;
	int tick;
	// Use this for initialization
	void Start () {
		sr = transform.GetComponent<SpriteRenderer> ();
		tick = 0;
		positionx = 0;
		positiony = 0;
		state = "mine";
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "explosion") {
			sr.sprite=explosion[tick];
			tick++;
			if(tick==10)
				GameObject.Destroy(this.gameObject);
		}
	}
	public void explosiontrigger(){
		state = "explosion";
	}
}
