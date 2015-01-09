using UnityEngine;
using System.Collections;

public class PressurePlate_OpeGate : MonoBehaviour {
	public GameObject Gate;
	public GameObject GateGear;
	public bool Active=false;
	// Use this for initialization
	void Start () {
	
	}
	void OnCollisionEnter(Collision item){
		Debug.Log (item.gameObject.name);
		if (item.gameObject.name == "Stopperbox") {
			Active=true;
				}
		}
		   

	// Update is called once per frame
	void Update () {
		if(GateOpenScript.OnOff)
	if(Active){

			Gate.animation.Play ("GateOpening");
			GateGear.animation.Play("OpenGateGear");	

	}
}
}
