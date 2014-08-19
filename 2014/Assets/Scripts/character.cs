using UnityEngine;
using System.Collections;

public class character : MonoBehaviour {

	public int positionx;
	public int positiony;
	public int direction;
	public Sprite[] motions;
	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();
		sr.sprite = motions [0];
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 nextpoint= new Vector3((float)(-3+0.668*positionx),(float)(4.67-0.668*positiony));
		transform.position = nextpoint;
	}
}
