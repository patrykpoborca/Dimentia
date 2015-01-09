using UnityEngine;
using System.Collections;

public class PrefabScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnCollisionEnter(Collision collision) {
      Debug.Log ("Thev");
		Debug.Log (collision.gameObject.name);
	
	}

	// Update is called once per frame
	void Update () {
		animation.Stop ();
	}
}
