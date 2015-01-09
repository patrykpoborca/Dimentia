using UnityEngine;
using System.Collections;

public class GUnAttach : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void ActionWork(){

			GameObject hand=GameObject.Find("Bip001 R Hand");	
		GameObject  wrench=GameObject.Find("wrench");
			this.transform.parent=hand.transform;
		this.transform.position= new Vector3(wrench.transform.position.x,wrench.transform.position.y,wrench.transform.position.z);
		this.transform.localScale = new Vector3 (0.008f,0.008f,0.008f);
		this.transform.rotation = new Quaternion (0, 0, 250, 0); 
		Destroy(wrench);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
