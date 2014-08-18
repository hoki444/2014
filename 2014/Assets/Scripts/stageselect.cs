using UnityEngine;
using System.Collections;

public class stageselect : MonoBehaviour {
    private Camera _camera;
    float screenWidth;
    float screenHeight;
    string mode;
    int stage;
	// Use this for initialization
	void Start () {
        _camera = Camera.main;
        screenWidth = _camera.pixelWidth;
        screenHeight = _camera.pixelHeight;
        mode = "main";
        stage = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        GUIStyle mgui = new GUIStyle();
        mgui.fontSize = 200;
        if (GUI.Button(getRect(0.1, 0.1, 0.3, 0.1), "메인 맵"))
            mode = "main";
        if (GUI.Button(getRect(0.6, 0.1, 0.3, 0.1), "커스텀 맵"))
            mode = "custom";
        for(int y=0;y<2;y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (GUI.Button(getRect(0.05+0.2*x, 0.3+0.2*y, 0.1, 0.1), (5 * y + x + 1).ToString()))
                {
                    stage = 5 * y + x + 1;
                    Application.LoadLevel("game");
                }
            }
        }
        if (GUI.Button(getRect(0.4, 0.8, 0.2, 0.1), "돌아가기"))
            Application.LoadLevel("mainscreen");
    }
    Rect getRect(double x, double y, double w, double h)
    {
        return new Rect((float)x * screenWidth, (float)y * screenHeight, (float)w * screenWidth, (float)h * screenHeight);
    }
}
