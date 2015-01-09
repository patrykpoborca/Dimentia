using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void ActionWork()
	{
		gameObject.SendMessageUpwards ("DisableGun");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
