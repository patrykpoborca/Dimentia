using UnityEngine;
using System.Collections;

public class GateOpenScript : MonoBehaviour {
	public GameObject openGear;
	public GameObject gate;
	public AudioClip ButtonPress;
	public static bool OnOff =false;
	// Use this for initialization
	void Start () {
		
	
		
	}
	void ActionWork()
	{  
		//openGear.animation.Play("OpenGateGear");
		//Debug.Log ("GateOpening");
		//gate.animation.Play ("GateOpening");
		audio.PlayOneShot (ButtonPress);
		gate.transform.position = new Vector3 (gate.transform.position.x,420.1385f,gate.transform.position.z);
		OnOff = true;

	}
	// Update is called once per frame
	void Update () {
	
	}
}
