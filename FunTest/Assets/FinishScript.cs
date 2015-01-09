using UnityEngine;
using System.Collections;

public class FinishScript : MonoBehaviour {
	private bool gui=false;
	public Font font;
	void OnGUI() {
		if (gui) {
						GUIStyle myStyle = new GUIStyle ();
						myStyle.font = font;
						myStyle.normal.textColor = Color.red;
						myStyle.hover.textColor = Color.red;
						myStyle.fontSize = myStyle.fontSize + 24;
						GUIStyle myBoxStyle = new GUIStyle ();
						myBoxStyle.normal.textColor = Color.cyan;
						myBoxStyle.hover.textColor = Color.blue;
		
						GUI.Box (new UnityEngine.Rect (0, 0, Screen.width, Screen.height / 1), "");
						GUI.Label (new Rect ((Screen.width / 4) - 50, (Screen.height / 4) - 100, 100, 30), "To be Continued ....", myStyle);
						myStyle.fontSize = myStyle.fontSize + 10;
						GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4), 100, 30), "Dimentia 2 ", myStyle);
						if (GUI.Button (new Rect ((Screen.width / 4) + 15, (Screen.height / 4) + 250, (Screen.width / 4) + 50, (Screen.height / 4) - 100), "Back", myStyle)) {				
								Application.LoadLevel ("menuScene");
						}
			if (GUI.Button (new Rect ((Screen.width / 4) + 270, (Screen.height / 4) + 250, (Screen.width / 4) + 50, (Screen.height / 4) - 100), "Credits", myStyle)) {				
				Application.LoadLevel ("Credits");
			}
				}
		
		

		}
		

	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y == 10429)
						gui = true;
	}
}
