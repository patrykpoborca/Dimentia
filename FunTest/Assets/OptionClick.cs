using UnityEngine;
using System.Collections;

public class OptionClick : MonoBehaviour {

	public Font font;
	public GUISkin OptionSkin;
	private bool SubTitles = false;
	
	private bool OptionFlag =false;
	public float MusicVolume;
	public float DialogVolume;
	public float GlobalVolume;
	public Camera Cam;
	private int selGridInt = 0;
	private string[] selStrings = new string[] {"Fast", "Simple", "Good", "Excellent"};
	// Use this for initialization
		void OnGUI() {
		GUI.skin = OptionSkin;
		if (OptionFlag) {
			Cam.enabled=false;
			GUI.skin = OptionSkin;
			
			
			GUIStyle myStyle = new GUIStyle();
			myStyle.font = font;
			myStyle.normal.textColor=Color.blue;
			myStyle.hover.textColor=Color.blue;
			myStyle.fontSize=myStyle.fontSize+24;
			GUIStyle myBoxStyle = new GUIStyle();
			myBoxStyle.normal.textColor=Color.cyan;
			myBoxStyle.hover.textColor=Color.blue;
			
			GUI.Box (new UnityEngine.Rect (0, 0, Screen.width, Screen.height / 1),"");
		    GUI.Label(new Rect ((Screen.width/4)-50, (Screen.height/4)-50, 100, 30), "Audio ", myStyle);
			GUI.Label(new Rect ((Screen.width/4)+15, Screen.height/4, 100, 30), "Music ", myStyle);
			//toggleTxt = GUI.Toggle (new Rect (Screen.width/4, Screen.height/4, 100, 30), toggleTxt, "");
			
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
				QualitySettings.SetQualityLevel(selGridInt+1);
				Cam.enabled=true;

				
			}
			
		}
		
		
	}
	// Update is called once per frame

	void OnMouseDown ()
	{
		
	
			
			OptionFlag=true;
			

	}

}
