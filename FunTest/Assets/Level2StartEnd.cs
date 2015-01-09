using UnityEngine;
using System.Collections;

public class Level2StartEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y == 10370)
						Application.LoadLevel ("bridgetown");
	}
}
