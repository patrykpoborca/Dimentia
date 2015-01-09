using UnityEngine;
using System.Collections;

public class SmallGearScript : MonoBehaviour {
	public GameObject Parent;
	public GameObject ParentSub;
	private bool StopperHit;
	public GameObject Fire;
	//public Animator Anim;
	// Use this for initialization
	void Start () {
		//Parent.animation.Play ();
		StopperHit = false;
	}
	void OnCollisionEnter(Collision collision) {

		Debug.Log (collision.gameObject.name);
		if ((collision.gameObject.name.Equals ("Stopperbox"))&&(StopperHit==false))
		{
			ParentSub.transform.position= 	new Vector3 (384.1251F, 441.1715F,710.5463F);
			Debug.Log(Parent.transform.position.z );
			Parent.transform.position = new Vector3 (Parent.transform.position.x, Parent.transform.position.y, Parent.transform.position.z + 1000);
			Fire.transform.position=	new Vector3 (0F,-1000F,0F);
		}
		StopperHit=true;

		//Debug.Log (Parent.);
		//Anim.StopPlayback ();
		//gameObject.animation.Stop ("SamlGear2");
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
