using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
	int tick;
	public Sprite[] explosions=new Sprite[10];
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = transform.GetComponent<SpriteRenderer> ();
		tick = 0;
	}
	
	// Update is called once per frame
	void Update () {
		sr.sprite=explosions[tick];
		tick++;
		if(tick==10)
			GameObject.Destroy(this.gameObject);
	}
}
