using UnityEngine;
using System.Collections;
/* Menu Click C# file
 * Script to handle the mouse click and menu generation with script file.
 * Has the script to generate the option menu and modify sound and graphics control
 * @author Karthik Rajakumar Thiagarajan
 * @version 1.0, Feburary 2014
 */
public class MenuClick : MonoBehaviour {
	public Texture aTexture;
	public Font font;
	public GUISkin OptionSkin;
	private bool SubTitles = false;

	private bool OptionFlag =false;
	public float MusicVolume;
	public float DialogVolume;
	public float GlobalVolume;
	public GameObject NewGameText;
	public GameObject LoadGameText;
	public GameObject OptionText;
	public GameObject ExitText;

	private int selGridInt = 0;
	private string[] selStrings = new string[] {"Fast", "Simple", "Good", "Excellent"};
	// Use this for initialization
	void Start () {
		OptionText.audio.Play ();
	}
	void OnGUI() {
		GUI.skin = OptionSkin;
		if (OptionFlag) {
			GUI.skin = OptionSkin;
		

						GUIStyle myStyle = new GUIStyle();
						myStyle.font = font;
			myStyle.normal.textColor=Color.cyan;
			myStyle.hover.textColor=Color.cyan;
						myStyle.fontSize=myStyle.fontSize+24;
			GUIStyle myBoxStyle = new GUIStyle();
			myBoxStyle.normal.textColor=Color.cyan;
			myBoxStyle.hover.textColor=Color.blue;

				GUI.Box (new UnityEngine.Rect (0, 0, Screen.width, Screen.height / 1),"");
						if (!aTexture) {
								Debug.LogError ("Please assign a texture in the inspector.");
								return;
							}
			GUI.Label(new Rect ((Screen.width/4)-50, (Screen.height/4)-50, 100, 30), "Audio ", myStyle);
			GUI.Label(new Rect ((Screen.width/4)+15, Screen.height/4, 100, 30), "Music ", myStyle);
			//toggleTxt = GUI.Toggle (new Rect (Screen.width/4, Screen.height/4, 100, 30), toggleTxt, "");

			MusicVolume = GUI.HorizontalScrollbar(new Rect ((Screen.width/4)+100, Screen.height/4, (Screen.width/4)+200, 30),MusicVolume , 1F, 0.0F, 10.0F );
			GUI.Label(new Rect ((Screen.width/4)+15, (Screen.height/4)+40 , 100, 30), "Dialog ", myStyle);
			OptionText.audio.volume=MusicVolume/10f;
			DialogVolume = GUI.HorizontalScrollbar(new Rect ((Screen.width/4)+100, ( Screen.height/4) +40, (Screen.width/4)+200, 30),DialogVolume , 1F, 0.0F, 10.0F);
			GUI.Label(new Rect ((Screen.width/4)+15, (Screen.height/4)+80 , 100, 30), "Master  ", myStyle);
			GlobalVolume = GUI.HorizontalScrollbar(new Rect ((Screen.width/4)+100, ( Screen.height/4) +80, (Screen.width/4)+200, 30),GlobalVolume , 1F, 0.0F, 10.0F);
			AudioListener.volume=GlobalVolume/10f;

			GUI.Label(new Rect ((Screen.width/4)+15, (Screen.height/4)+120 , 100, 30), "Graphics Quality", myStyle);

			selGridInt = GUI.SelectionGrid(new Rect ((Screen.width/4)+100, ( Screen.height/4) +150, (Screen.width/4)+200,50 ), selGridInt, selStrings, 2);
			GUI.Label(new Rect ((Screen.width/4)+15, (Screen.height/4)+220 , 100, 30), "Subtitles ", myStyle);
			SubTitles = GUI.Toggle (new Rect ((Screen.width/4)+150,( Screen.height/4) +220, 100, 30), SubTitles, "On");
			if(GUI.Button (new Rect ((Screen.width/4)+400,( Screen.height/4) +420, 100, 30),"Apply"))
			{
				OptionFlag=false;
				ExitText.renderer.enabled = true;
				NewGameText.renderer.enabled = true;
				LoadGameText.renderer.enabled = true;
				OptionText.renderer.enabled = true;
				QualitySettings.SetQualityLevel(selGridInt+1);

			}

		}


	}
	// Update is called once per frame
	void Update () {
	if (Input.GetKey (KeyCode.Escape)) 
				{
					//	Debug.Log(QualitySettings.names.Length);
						OptionFlag = false;
						ExitText.renderer.enabled = true;
						NewGameText.renderer.enabled = true;
						LoadGameText.renderer.enabled = true;
						OptionText.renderer.enabled = true;
						QualitySettings.SetQualityLevel(selGridInt+1);


				}

	}
	void OnMouseDown ()
	{

		//Debug.Log (gameObject.name);
		if (gameObject.name.Equals ("NewGame")) {
			Application.LoadLevel("Story");
		
		}
		if (gameObject.name.Equals ("Options")) {
			ExitText.renderer.enabled=false;
			NewGameText.renderer.enabled=false;
			LoadGameText.renderer.enabled=false;
			OptionText.renderer.enabled=false;
				
			OptionFlag=true;
		
		
		}

	}

}

