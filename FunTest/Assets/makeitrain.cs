using UnityEngine;
using System.Collections;

public class makeitrain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		timer = 5f;
	}
	float timer;
		public Rigidbody prefab;
	public Rigidbody temp;
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			temp = Instantiate (prefab, new Vector3(-65, 50, -12), this.transform.rotation ) as Rigidbody;
			timer = 5f;

				}
	}
}
