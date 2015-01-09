using UnityEngine;
using System.Collections;

public class digitChange : MonoBehaviour {
	private Texture texture;
	public Texture Digit1;
	public Texture Digit2;
	public Texture Digit3;
	public Texture Digit4;
	public Texture Digit5;
	public Texture Digit6;
	public Texture Digit7;
	public Texture Digit8;
	public Texture Digit9;
	public Texture Digit0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void UpdateMoney(int x)
	{
		//Debug.Log (x);
		if (gameObject.name == "digitOne") {
			updateDigit(((int)x/100)%10);
		}
		if (gameObject.name == "digitTwo") {
			updateDigit(((int)x/10)%10);
		}
		if (gameObject.name == "digitThree") {
			updateDigit((int)x%10);
		}

	}
	public void updateDigit (int x){
		if (x <= 9 || x >= 0)
		switch (x) {
			case 0:
			//texture = (Texture) Resources.LoadAssetAtPath("zero.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit0;
			break;
			case 1:
			//texture = (Texture) Resources.LoadAssetAtPath("one.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit1;
			break;
			case 2:
			//texture = (Texture) Resources.LoadAssetAtPath("two.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit2;
			break;
			case 3:
			//texture = (Texture) Resources.LoadAssetAtPath("three.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit3;
			break;
			case 4:
		//	texture = (Texture) Resources.LoadAssetAtPath("four.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit4;
			break;
			case 5:
		//	texture = (Texture) Resources.LoadAssetAtPath("five.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit5;
			break;
			case 6:
		//	texture = (Texture) Resources.LoadAssetAtPath("six.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit6;
			break;
			case 7:
		//	texture = (Texture) Resources.LoadAssetAtPath("seven.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit7;
			break;
			case 8:
		//	texture = (Texture) Resources.LoadAssetAtPath("eight.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit8;
			break;
			case 9:
		//	texture = (Texture) Resources.LoadAssetAtPath("nine.PNG", typeof(Texture));
			renderer.material.mainTexture = Digit9;
			break;
			
		}
	}

}
