using UnityEngine;
using System.Collections;

public class CrateFallMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnCollisionEnter(Collision collision) {
		audio.Play ();
	
	}
	// Update is called once per frame
	void Update () {
	
	}
}
