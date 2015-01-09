using UnityEngine;
using System.Collections;

public class healthChange : MonoBehaviour {
	private Texture texture;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateHealth (int x){
		switch (x) {
		case 0:
			texture = (Texture) Resources.LoadAssetAtPath("full.PNG", typeof(Texture));
			renderer.material.mainTexture = texture;
			break;
		case 1:
			texture = (Texture) Resources.LoadAssetAtPath("hurt.PNG", typeof(Texture));
			renderer.material.mainTexture = texture;
			break;
		case 2:
			texture = (Texture) Resources.LoadAssetAtPath("half.PNG", typeof(Texture));
			renderer.material.mainTexture = texture;
			break;
		case 3:
			texture = (Texture) Resources.LoadAssetAtPath("quarter.PNG", typeof(Texture));
			renderer.material.mainTexture = texture;
			break;
		case 4:
			texture = (Texture) Resources.LoadAssetAtPath("empty.PNG", typeof(Texture));
			renderer.material.mainTexture = texture;
			break;
		}

	}

}
