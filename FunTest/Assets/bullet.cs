using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	private float Timer = 1.5f;
	// Use this for initialization
	void Start () {
	Object.Destroy (this.gameObject, 1.5F);
		//rigidbody.detectCollisions = false;

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision other)
	{
		//Debug.Log (other.gameObject.name);
		if(other.gameObject.name=="3rd Person Controller")
			other.gameObject.SendMessage("ApplyDamage", 1F , SendMessageOptions.DontRequireReceiver);
		//Destroy (other.gameObject);
	}
}
