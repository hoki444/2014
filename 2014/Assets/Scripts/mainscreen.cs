using UnityEngine;
using System.Collections;

public class mainscreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		GUIStyle mgui = new GUIStyle ();
		mgui.fontSize = 200;
		GUI.Label (new Rect (280, 50, 600, 50), "2014", mgui);
		mgui.fontSize = 300;
		if(GUI.Button(new Rect (380, 280, 250, 50),"게임 모드"))
			Application.LoadLevel ("stageselect");
		if(GUI.Button(new Rect (380, 400, 250, 50),"맵 에디터"))
			Application.LoadLevel ("editor");
		if(GUI.Button(new Rect (380, 520, 250, 50),"게임 종료"))
			Application.Quit ();
	}
}
