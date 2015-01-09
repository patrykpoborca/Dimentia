using UnityEngine;
using System.Collections;

public class Button1ActionScript : MonoBehaviour {
	public GameObject Platform;
	public AudioClip ButtonPress;
	public GameObject Flame;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ActionWork()
	{   Flame.transform.position = new Vector3 (373.5954F,397.4471F,685.0751F);
		Platform.transform.Rotate (new Vector3(0,90,0));
		Debug.Log ("Check");
		audio.PlayOneShot (ButtonPress);
		Flame.audio.Play ();
	}
}
