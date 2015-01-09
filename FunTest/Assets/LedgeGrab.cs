using UnityEngine;
using System.Collections;

public class LedgeGrab : MonoBehaviour {

	public enum PlayerState {
		Standing = 0,
		Hanging = 1
	}

	private GameObject collider;

	public static PlayerState playerState;

	// Use this for initialization
	void Start () {
		playerState = PlayerState.Standing;
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject == GameObject.Find ("3rd Person Controller")) {
			playerState = PlayerState.Hanging;
			collider = col.gameObject;
			col.gameObject.transform.position = this.transform.position;
			col.rigidbody.velocity = Vector3.zero;
			col.rigidbody.angularVelocity = Vector3.zero;
			col.rigidbody.useGravity = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerState == PlayerState.Hanging) {
			if (Input.GetKeyDown(KeyCode.W)) {
				playerState = PlayerState.Standing;
				collider.transform.position = Vector3.Lerp (collider.transform.position, new Vector3(this.transform.position.x,this.transform.position.y+3,this.transform.position.z),1F);
				collider.rigidbody.useGravity = true;
			} else if (Input.GetKeyDown (KeyCode.S)) {
				playerState = PlayerState.Standing;
				collider.rigidbody.useGravity = true;

			}
		}
	}
}
