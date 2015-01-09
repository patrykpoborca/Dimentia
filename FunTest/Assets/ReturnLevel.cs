using UnityEngine;
using System.Collections;

public class ReturnLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (gameObject.transform.position.x > 875)
						Application.LoadLevel ("bridgetown");
	}
}
