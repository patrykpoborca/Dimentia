using UnityEngine;
using System.Collections;

public class characterUtility : MonoBehaviour {

	// global variables
	Vector3 pFORWARD;
	Vector3 FORWARD;
	Vector3 handSimulation;
	public int holdingState;
	public RaycastHit hit;
	public Vector3 s1, s2;
	public CharacterController controller;
	public GameObject hand; // the location of the hand, just how this rigidbody was setup
	public GameObject ball; 
	public GameObject item; // gets set to the object you grab
	//public GameObject THINGY; // thingy = third person controller
	
	Vector2 posObj;
	// end of variables
	// Use this for initialization
	void Start () {
		FORWARD = Vector3.zero;
		handSimulation = Vector3.zero;
		pFORWARD = FORWARD;
		controller = GetComponent<CharacterController>();
		holdingState = 0;
	}
	
	// utility functions

	public void fwrd(Vector3 k)
	{
		FORWARD = k;
	}

	
	public int castCapsule()
	{

		s2 = s1 +  FORWARD ;
	//	ball2.transform.position =  s2;
	//	Debug.Log (ball2.transform.position);
		//Debug.Log (Vector3.Distance(s1, s2));

		if (Physics.SphereCast (transform.position, 2.0f,transform.forward, out hit,10) && hit.collider.tag == "moveit") {
			//Debug.Log (hit.collider.rigidbody.name);	
		//	hit.collider.gameObject.rigidbody.isKinematic =true;	
			return 1;
				}
				

			return 0;
	}
	
	// end of utility

	void OnGUI()
	{

		if(holdingState == 1)
			GUI.TextField (new Rect (300, 100, 100, 100), "Lift me");
	}



	// Update is called once per frame
	void Update () 
	{

		FORWARD = this.transform.forward;

		//Debug.Log (rigidbody.position);
			
		s1 = transform.position ;
				
//		Debug.Log("0");

		//Debug.Log (holdingState);

		if (holdingState != 2) {
						holdingState = castCapsule ();



//			Debug.Log("Check");
				}
		if(Input.GetKeyDown(KeyCode.J))
		{
			if (holdingState == 2 ) {

			}
		}

		if ((Input.GetKeyDown (KeyCode.K))&&(holdingState==1)) {
			if (Physics.SphereCast (transform.position, 2.0f,transform.forward, out hit,10) && hit.collider.tag == "moveit") {
				item=hit.collider.gameObject;
				hit.collider.gameObject.transform.parent=hand.transform;
				hit.collider.gameObject.transform.position=new Vector3(item.transform.position.x,item.transform.position.y,item.transform.position.z);


				holdingState=2;
			}
		
		}
		if ((Input.GetKeyUp (KeyCode.H))&&(holdingState==2)) {
			item.transform.parent=null;
			item.rigidbody.velocity= gameObject.transform.forward;
			holdingState=1;
			}
		if ((Input.GetKeyUp (KeyCode.K))&&(holdingState==2)) {
			item.transform.parent=null;
			item.rigidbody.velocity= gameObject.transform.forward*4;
			holdingState=1;

		}

			/*{
		if (holdingState == 2 ) {
			//Throw
						holdingState = 0;
		//	ball2.rigidbody.isKinematic =false;	
			hit.collider.gameObject.rigidbody.useGravity = true;
				}
		if (holdingState == 1 ) {
			// we push the object here	

			hit.rigidbody.position += FORWARD*3;
			
		}
		if (holdingState == 1 && hit.collider.tag == "moveit") 
		{	

				holdingState = 2;
				//hit.collider.gameObject.transform.parent = hand.transform.parent;
				hit.collider.gameObject.rigidbody.useGravity = false;
				hit.collider.gameObject.rigidbody.position=new Vector3(hit.collider.gameObject.rigidbody.position.x,hit.collider.gameObject.rigidbody.position.y+2,hit.collider.gameObject.rigidbody.position.z);
				//ball = hit.collider.gameObject;

			}
		//	posObj = Camera.main.WorldToScreenPoint(hit.transform.position);
			// hit.collider.gameObject.transform.position = hand.transform.position;
		}
		if (holdingState == 2) {

			handSimulation = gameObject.transform.position+FORWARD;
			//Debug.Log("s1" + handSimulation );
     		hit.collider.gameObject.rigidbody.position = handSimulation;
			//ball.collider.gameObject.transform.position = handSimulation;		
		}*/
		
	}
}
