using UnityEngine;
using System.Collections;

public class HealthChange : MonoBehaviour {
	private Texture texture;
	public  Font font;
	// Use this for initialization
	public int health=8;
	public Texture Full;
	public Texture Hurt;
	public Texture Half;
	public Texture Quarter;
	public Texture Empty;
	public bool Dead=false;
	void Start () {
		updateHealth ();
		health=8;
	}

	void OnGUI() {
		
				if (Dead) {
			
						Screen.showCursor = true;
						//mainCam.enabled = false;
						Time.timeScale = 0;
						//Debug.Log (stringToEdit);
			
						GUIStyle myStyle = new GUIStyle ();
						myStyle.font = font;
						myStyle.normal.textColor = Color.red;
						myStyle.hover.textColor = Color.red;
						myStyle.fontSize = myStyle.fontSize + 56;
						GUIStyle myBoxStyle = new GUIStyle ();
						myBoxStyle.normal.textColor = Color.cyan;
						myBoxStyle.hover.textColor = Color.blue;
			
						GUI.Box (new UnityEngine.Rect (0, 0, Screen.width, Screen.height / 1), "");
						GUI.Label (new Rect ((Screen.width / 4) - 50, (Screen.height / 4) - 100, 100, 30), "DEAD!!!!", myStyle);
			myStyle.fontSize = myStyle.fontSize - 30;

						if (GUI.Button (new Rect ((Screen.width / 4) + 15, (Screen.height / 4) + 250, (Screen.width / 4) + 50, (Screen.height / 4) - 100), "Restart", myStyle))
			                {

				Application.LoadLevel(Application.loadedLevel);
				
						}
					
						}
				} 
		
	// Update is called once per frame
	void Update () {
		updateHealth ();
	}
	void ApplyDamage(int i){
		health = health - i;
		//updateHealth ();
		}
	public void updateHealth (){

		//Debug.Log (health);
		switch (health) {
		case 0:
			texture = Empty;
			renderer.material.mainTexture = texture;
			Dead=true;
			break;
		case 2:
		case 1:
			texture = Quarter;
			renderer.material.mainTexture = texture;
				break;
		case 4:
		case 3:
			texture = Half;
			renderer.material.mainTexture = texture;


			break;
		case 5:
		case 6:
			texture = Hurt;
			renderer.material.mainTexture = texture;

			break;
		case 8:
		case 7:
				texture = Full;
			renderer.material.mainTexture = texture;
			break;
		default:
			texture =Full;
			renderer.material.mainTexture = texture;
			break;
		}

	}
}
