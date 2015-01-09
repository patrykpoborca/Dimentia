using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (gameObject.transform.position.z == 0)
						Application.LoadLevel ("Menu");
	}
}
