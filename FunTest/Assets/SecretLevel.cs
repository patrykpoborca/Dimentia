using UnityEngine;
using System.Collections;

public class SecretLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log ((int)gameObject.transform.position.y);
		if ((int)gameObject.transform.position.y < 10230)
				if ((int)gameObject.transform.position.z> -29000.63)
				Application.LoadLevel ("ExploreLevel");
	}
}
