using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {
	
	public Rigidbody prefab;
	public Rigidbody bull;
	public GameObject target;
	public float ShootRate;
	public float UpdateCount=0;

	public bool Active=true;
	// Use this for initialization
	void Start () {
	
	}
	void DisableGun()
	{Active = false;

	}
	// Update is called once per frame
	void Update () {
				UpdateCount++;
				//target=gameObject.Find("3rd Person Controller");
				RaycastHit hit;
		if(gameFM.fStatus==-1)
	if(Active)
		{
			if (Physics.SphereCast (transform.position, 7.0f, transform.forward, out hit, 1000)) {
					//	Debug.Log (hit.collider.name);
						if (hit.collider.gameObject.Equals (target)) {
								Vector3 targetDir = target.transform.position - transform.position;
								Vector3 forward = transform.forward;
								float angle = Vector3.Angle (targetDir, forward);
								//Quaternion Qangle = Quaternion.FromToRotation(targetDir, mth);

								if (UpdateCount % ShootRate == 0) {
						      

										bull = Instantiate (prefab, new Vector3 (transform.position.x, transform.position.y + 15, transform.position.z) + (transform.forward * 2), Quaternion.identity) as Rigidbody;
										bull.transform.LookAt (hit.collider.gameObject.transform.position);
										//	bull.transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation,100);
										// mth=target.transform.position - bull.transform.position* 1;
										//mth.y=target.transform.position.y;
						Vector3 mth = (target.transform.position - bull.transform.position) *2;
						mth.z+=2;
										if (mth.z > 0) {//Debug.Log (mth.x + " " + mth.y );
												bull.velocity = mth;
										}
								}

						}

	
		
				}
		}
}

}