using UnityEngine;
using System.Collections;

public class ActionScript : MonoBehaviour {
	public Font font;
	public AudioClip VoiceWhenSeeing;
	bool PauseState = false;
	bool KeyBinding= false;
	public Camera mainCam;
	public GameObject character ;
	bool playOnceflag=true;
	string stringToEdit="Cheat";
	string pause="p";
	string rewind=",";
	string dimensionJump="g";
	string resume="r";
	string forward=".";
	string Pickup="k";
	float MusicVolume ;
		float GlobalVolume ;
	// Use this for initialization
	void Start () {
		audio.Play ();
		MusicVolume = audio.volume;
		GlobalVolume = AudioListener.volume;
	}
	
	void OnGUI() {
	
		if (KeyBinding) {
				
			Screen.showCursor = true;
			mainCam.enabled = false;
			Time.timeScale = 0;
			//Debug.Log (stringToEdit);
			
			GUIStyle myStyle = new GUIStyle ();
			myStyle.font = font;
			myStyle.normal.textColor = Color.cyan;
			myStyle.hover.textColor = Color.cyan;
			myStyle.fontSize = myStyle.fontSize + 24;
			GUIStyle myBoxStyle = new GUIStyle ();
			myBoxStyle.normal.textColor = Color.cyan;
			myBoxStyle.hover.textColor = Color.blue;
			
			GUI.Box (new UnityEngine.Rect (0, 0, Screen.width, Screen.height / 1), "");
			GUI.Label (new Rect ((Screen.width / 4) - 50, (Screen.height / 4) - 100, 100, 30), "Key Binding", myStyle);
			myStyle.fontSize = myStyle.fontSize - 24;
			GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4), 100, 30), "PauseTime", myStyle);
			pause = GUI.TextField (new Rect ((Screen.width / 4) + 140, (Screen.height / 4),30, 30), pause, 25);

			GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4)+40, 100, 30), "Rewind Time", myStyle);
			rewind = GUI.TextField (new Rect ((Screen.width / 4) + 140, (Screen.height / 4)+40,30, 30), rewind, 25);

			GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4)+80, 100, 30), "FastForward", myStyle);
			forward = GUI.TextField (new Rect ((Screen.width / 4) + 140, (Screen.height / 4)+80,30, 30), forward, 25);

			GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4)+120, 100, 30), "Resume Time", myStyle);
			resume = GUI.TextField (new Rect ((Screen.width / 4) + 140, (Screen.height / 4)+120,30, 30), resume, 25);

			GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4)+160, 100, 30), "Dimension Jump", myStyle);
			dimensionJump = GUI.TextField (new Rect ((Screen.width / 4) + 140, (Screen.height / 4)+160,30, 30), dimensionJump, 25);

			GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4)+200, 100, 30), "PickUp/Drop", myStyle);
			Pickup = GUI.TextField (new Rect ((Screen.width / 4) + 140, (Screen.height / 4)+200,30, 30), Pickup, 25);


			if (GUI.Button (new Rect ((Screen.width / 4) + 15, (Screen.height / 4) + 250, (Screen.width / 4) + 50, (Screen.height / 4) - 100), "Back", myStyle)) {
				
				KeyBinding=false;
				
			}
			if (GUI.Button (new Rect ((Screen.width / 4) + 250, (Screen.height / 4) + 250, (Screen.width / 4) + 50, (Screen.height / 4) - 100), "Apply", myStyle)) {
				//Code to set the key binding
				KeyBinding=false;
				
			}
				} 
		else if (PauseState) {
						Screen.showCursor = true;
						mainCam.enabled = false;
						Time.timeScale = 0;
						//Debug.Log (stringToEdit);
						
						GUIStyle myStyle = new GUIStyle ();
						myStyle.font = font;
						myStyle.normal.textColor = Color.cyan;
						myStyle.hover.textColor = Color.cyan;
						myStyle.fontSize = myStyle.fontSize + 24;
						GUIStyle myBoxStyle = new GUIStyle ();
						myBoxStyle.normal.textColor = Color.cyan;
						myBoxStyle.hover.textColor = Color.blue;
		
						GUI.Box (new UnityEngine.Rect (0, 0, Screen.width, Screen.height / 1), "");
						GUI.Label (new Rect ((Screen.width / 4) - 50, (Screen.height / 4) - 100, 100, 30), "PauseMenu", myStyle);
						if (GUI.Button (new Rect ((Screen.width / 4) + 15, (Screen.height / 4) - 50, (Screen.width / 4) + 50, (Screen.height / 4) - 100), "Restart", myStyle)) {
			

								Application.LoadLevel (Application.loadedLevel);
							
						}


						GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4), 100, 30), "Music Volume ", myStyle);

						MusicVolume = GUI.HorizontalScrollbar (new Rect ((Screen.width / 4) + 140, Screen.height / 4 + 40, (Screen.width / 4) + 200, 30), MusicVolume, 1F, 0.0F, 10.0F);
						GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4) + 60, 100, 30), "Global Volume ", myStyle);
						audio.volume = MusicVolume / 10f;
		
						GlobalVolume = GUI.HorizontalScrollbar (new Rect ((Screen.width / 4) + 140, (Screen.height / 4) + 120, (Screen.width / 4) + 200, 30), GlobalVolume, 1F, 0.0F, 10.0F);
						AudioListener.volume = GlobalVolume / 10f;
						GUI.Label (new Rect ((Screen.width / 4) + 15, (Screen.height / 4) + 160, 100, 30), "Cheat ", myStyle);

						stringToEdit = GUI.TextField (new Rect ((Screen.width / 4) + 140, (Screen.height / 4) + 160, (Screen.width / 4) + 200, 30), stringToEdit, 25);
			if (GUI.Button (new Rect ((Screen.width / 4) + 15, (Screen.height / 4) + 250, (Screen.width / 4) + 50, (Screen.height / 4) - 100), "Set Key Binding", myStyle)) {
				
				KeyBinding=true;
				
			}
						if (GUI.Button (new Rect ((Screen.width / 4) + 15, (Screen.height / 4) + 300, (Screen.width / 4) + 50, (Screen.height / 4) - 100), "Quit Game", myStyle)) {

								Application.Quit ();
				
						}

			            
				} else {
						Time.timeScale = 1;
			mainCam.enabled = true;		
		}
		if(stringToEdit=="ZapMe")
		{
			if(Application.loadedLevelName=="level1")
			{character.gameObject.rigidbody.transform.position=new Vector3(309,430,683);
				stringToEdit="";
			}
			else
			{	character.gameObject.rigidbody.transform.position=new Vector3(5364,10373,-29362);
				stringToEdit="";
			}

		//	character.transform.position=new Vector3(309,430,683);

			//Debug.Log("Zapped");
		}

	}

	// Update is called once per frame
	void Update () {
		RaycastHit hit1  ;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit1)) {
			if(hit1.collider.name=="arc reactor")
			{
				if(playOnceflag)
				{
					mainCam.audio.PlayOneShot(VoiceWhenSeeing);
					playOnceflag=false;
				}
			}
		}
		float Distance;
		int TheDamage=0;
		float MaxDistance = 3F;
		if (Input.GetKeyDown (KeyCode.Escape)) {
			PauseState=!PauseState;
		}
		if(Input.GetKeyDown(KeyCode.E))
		{

			//Debug.Log("Test");
			//TheWepon.animation.Play("Attack");
			RaycastHit hit  ;
			if (Physics.SphereCast (transform.position, 2.0f,transform.forward, out hit,10))
			{
				//Debug.Log(hit.ToString());

			//	if(hit.collider.name.Equals("Stm_button02")){}
				if((hit.collider.tag=="moveit")&&(hit.distance<2))
				{
//					Debug.Log(hit.collider.name);
					hit.rigidbody.position += transform.forward;
				}

				Distance= hit.distance;
				//Debug.Log(Distance);
				if(Distance<MaxDistance)
					hit.transform.SendMessage("ActionWork",SendMessageOptions.DontRequireReceiver);

//				Debug.Log(hit.collider.name);

				if(hit.collider.name=="arc reactor")
				{
					Debug.Log("asdasd");
					Time.timeScale=1;
					Application.LoadLevel("level1to2");



				}

			}
		}
		if ((Input.GetKeyDown (KeyCode.Space))) {
			if(Input.GetKeyDown (KeyCode.C))
			Debug.Log("You Cheat");
			}

	}


}
 