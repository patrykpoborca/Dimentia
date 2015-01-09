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
	public GameObject ball; // gets set to the object you grab
	public GameObject THINGY; // thingy = third person controller
	
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

		s2 = s1 +  FORWARD;
		ball.transform.position =  s2;
		//Debug.Log (Vector3.Distance(s1, s2));
		if (Physics.CapsuleCast (s1, s2, controller.radius *2, transform.forward, out hit, Vector3.Distance(s1, s2))) 

			return 1;
				
			
			return 0;
	}
	
	// end of utility

	void OnGUI()
	{

		if(holdingState == 1)
			GUI.TextField (new Rect (300, 300, 100, 100), "Display Info");
	}



	// Update is called once per frame
	void Update () 
	{

		//Debug.Log (rigidbody.position);
			
		s1 = THINGY.rigidbody.position ;
				
		if(holdingState != 2) holdingState = castCapsule ();

		if (holdingState == 2 && Input.GetKeyDown (KeyCode.K)) {
						holdingState = 0;
		
			hit.collider.gameObject.rigidbody.useGravity = true;
				}
		if (holdingState == 1 && hit.collider.tag == "moveit") 
		{	
			if(Input.GetKeyDown(KeyCode.K))
			{holdingState = 2;
				//hit.collider.gameObject.transform.parent = hand.transform.parent;
				//hit.collider.gameObject.rigidbody.useGravity = false;
				ball = hit.collider.gameObject;

			}
			posObj = Camera.main.WorldToScreenPoint(hit.transform.position);
			// hit.collider.gameObject.transform.position = hand.transform.position;
		}
		if (holdingState == 2) {

			handSimulation = s1 + FORWARD;
			Debug.Log("s1" + handSimulation );
			ball.collider.gameObject.transform.position = handSimulation;		
		}
		
	}
}
