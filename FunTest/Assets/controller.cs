using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {
	
	// Use this for initialization
	
	private Animator anim;							// a reference to the animator on the character
	private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState;	// a reference to the current state of the animator, used for layer 2
	private CharacterController col;					// a reference to the capsule collider of the character
	private BoxCollider box;
	
	public float animSpeed = 1.5f;				// a public setting for overall animator animation speed
	
	static int loco = Animator.StringToHash("Base Layer.Locomotion");	
	static int rollRun = Animator.StringToHash("Base Layer.Roll_To_Run");	
	static int grab = Animator.StringToHash("Base Layer.IdleGrab_LowFront");	
	static int haltAll = Animator.StringToHash("Base Layer.haltAll");	
	static int walkBack = Animator.StringToHash("Base Layer.walkBackQuick");
	static int highHoist = Animator.StringToHash("Base Layer.highHoist");
	static int medHoist = Animator.StringToHash("Base Layer.mediumHoist");
	
	private bool isGround = false;
	private float[] lookupHeight;
	
	private float height;
	private float [] timers;
	private bool lockWalk = false;
	private bool grounded;
	
	public Vector3 gravity;
	public Vector3 currentVector;
	
	private Vector3 lastPos;
	private float lastPosTimer;
	
	private float currentHeight;
	private float fallingTimer;//used to insantiate animation of falling.
	
	static Vector3 resetPos;
	void Start () {
		grounded = true;
		lastPosTimer = 0.25f;
		lastPos = transform.position;
		
		gravity = new Vector3 (0, -9.8f, 0);
		currentVector = new Vector3(0f, 0f, 0f);
		lookupHeight = new float[3];
		lookupHeight[0] = 0f; lookupHeight [1] = 7f; lookupHeight [2] = 11f;
		
		timers = new float[5];
		for (int a=0; a < 5; a++)
			timers [a] = 0f;
		resetPos = transform.position;
		anim = GetComponent<Animator>();					  
		col = GetComponent<CharacterController>();	
		
		box = GetComponent<BoxCollider>();
		
	}
	
	//returns whether or not the object is free to be acted upon
	bool actionFree()
	{
		if (anim.GetCurrentAnimatorStateInfo(0).nameHash == highHoist)
			return false;
		if (anim.GetCurrentAnimatorStateInfo(0).nameHash  == medHoist)
			return false;
		
		return true;
		
	}
	
	
	// Update is called once per frame
	void Update () {
		if(lastPosTimer <= 0)
			lastPos = transform.position;
		
		lastPosTimer += (lastPosTimer <= 0) ? 0.25f : -Time.deltaTime;
		
		
		
		
		if(actionFree()) applyPhysics (); //means we're not in middle of animation
		
		//climbing movement logic
		if (timers [1] != 0) { // 1 holds the "transition time" timers[0] is the elapsing time
			if(timers[0] == 0)
			{
				Debug.Log ("heihfefh  " + height);
			}
			timers [0] += Time.deltaTime;
			
			if (timers [0] >= timers [1]) //times each one pulls up
			{
				//Debug.Log ("FORWARD");
				transform.position += new Vector3(transform.forward.x, 0f, transform.forward.z) *Time.deltaTime *2; //iterate forward
			}
			else {
				
				//Debug.Log ("transforming " + transform.position.y);
				transform.position = new Vector3 (transform.position.x, transform.position.y + ((height) / (timers [1]) * Time.deltaTime), transform.position.z);
			}
		}
		
		timers [0] = (timers [0] >= timers[2]) ? 0 : timers [0];
		timers[1] = (timers[0]== 0) ? 0 : timers[1];	
		timers [2] = (timers [1] == 0) ? 0 : timers [2]; //involve truth value... todo...?
		if (timers [2] == 0 && height != 0) {
			
		}
		//height = (timers[1] == 0) ? 0 : height;
		if (timers [1] == 0 && height !=0) {
			height = 0;
			col.Move(Vector3.up);
				}
		//end of climbing movement logic
		
		
		
	}
	
	
	
	
	void haltOrder (){
		anim.SetBool ("haltOrder", true);
	}
	
	int ledgeToGrab() //ledge grab logic
	{
		
		RaycastHit hit;
		int type = 2;
		
		
		height = 0;
		if (Physics.Raycast (transform.position + new Vector3 (0f, 13.5f, 0f), transform.forward, 4f)) //too large of an object
			return 0;
		
		for (float a =0; a < 13.5f; a+= 0.1f) {
			if (Physics.Raycast (transform.position + new Vector3 (0f, 13.5f - a, 0f), transform.forward, out hit, 4f))
			{
				height = 13.5f-a;
				break;
			}
		}
		if (height == 0 || height <= 4f)
			return 0;
		if (height > 2f && height <= 4f) //decides what animation we play
			type = 1;
		if (height > 4f && height <= 8.8f) {
			type = 2;
			if (anim.GetFloat ("speed") < 0.8f) {
				timers [1] = 1.06f;
				timers [2] = 1.22f;
			}
			else
			{	timers [1] = 1.2f;
				timers [2] = 2f;
			}
		}
		if (height > 8.8f && height <= 13.5f) {
			type = 3;
			if(anim.GetFloat("speed") <0.8f)
			{timers[1] = 3f; timers[2] = 3.4f;}
			else
			{timers[1] = 1.2f; timers[2] = 2f;}
		}
		
		
		height += 1f;
		height -= lookupHeight[type-1]; //subtract's animation height
		
		//	height = 0.1f;
		return type; //high grab
	}
	
	
	//applies jump.... to vector, does some "vector" stuff based upon last pos
	void applyJump()
	{
		

		//anim.applyRootMotion = false;
		currentVector += new Vector3(transform.forward.x, 4f, transform.forward.z)* 4;
		
	}
	
	bool errorCheck()
	{
		RaycastHit hit;
		bool bef = grounded;
		return( Physics.Raycast (transform.position + new Vector3 (0f, -0.2f, 0f), Vector3.down * 4, out hit, 0.3f));
		
	}
	
	
	bool applyPhysics()
	{

		RaycastHit hit;
		bool bef = grounded;
		//grounded = Physics.Raycast (transform.position + new Vector3 (0f, -0.2f, 0f), Vector3.down * 3, out hit, 0.3f);
		//Physics.CapsuleCast (transform.position + new Vector3 (0f, -0.2f, 0f), transform.position + new Vector3 (0f, -0.200001f, 0f), 4f, Vector3.down, out hit, 2f);
		grounded = IsGrounded ();
		//Physics.Raycast (transform.position + new Vector3 (0f, -0.2f, 0f), Vector3.down * 3, out hit, 0.3f);
		
		if (bef == true && grounded == false)
			fallingTimer += Time.deltaTime; //start counting time falling...
		
		if (bef = false && grounded == true) { //landing
			if(fallingTimer >= 1f)
				anim.SetFloat("jumpTypes", 50f);
			fallingTimer = 0f;
			
		}
		else if(grounded == false && fallingTimer > 1f)
		{} //falling animation
		
		
		if (grounded == false) //falling
						currentVector += gravity * Time.deltaTime;
				else
			if (currentVector.y < 0f) {
						currentVector.y = 0f;

				}
		//if(anim.hasRootMotion == false && grounded) anim.applyRootMotion = true;
		
		Debug.Log ("Grounded " + grounded);
		
		
		col.Move (currentVector * Time.deltaTime);
		
		currentVector.x -= currentVector.x * Time.deltaTime;
		currentVector.z -= currentVector.z * Time.deltaTime;
		return false;
	}

	

	bool IsGrounded () {
		return (col.collisionFlags & CollisionFlags.CollidedBelow) != 0;
	}

	bool trial = true;
	void FixedUpdate()
	{
		if (Input.GetKeyDown (KeyCode.Alpha7))
			transform.position = new Vector3 (transform.position.x, transform.position.y + 7f, transform.position.z);
		
		if (Input.GetKeyDown (KeyCode.Alpha0))
			Application.LoadLevel ("level1");
		if (Input.GetKeyDown (KeyCode.Alpha8))
			transform.position = new Vector3(resetPos.x, resetPos.y+1, resetPos.z);
		
		
		if (!actionFree ()) {trial = false;
			return;
		}
		
		
		bool sprinting = Input.GetKey (KeyCode.LeftShift);
		
		
		
		//if(Input.GetKeyDown
		
		//roll logic...
		
		//end of roll logic
		
		//if (!col.isGrounded) // we're falling, animations shouldn't be issued right now...
		//				return;
		
		
		
		float s = anim.GetFloat ("speed");
		
		int rVal;		
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			
			rVal =ledgeToGrab();
			if(rVal != 0)
			{
				if(rVal == 3)
					anim.SetFloat("jumpTypes", 100f);
				if(rVal == 2)
					anim.SetFloat("jumpTypes", 90f);
			}else
			{
				if (s < 1.1f)
					anim.SetFloat ("jumpTypes", 1f); //jog jump
				else if(s >= 1.1f)
					anim.SetFloat ("jumpTypes", 1.5f); //spring jump
				applyJump();		
			}
			
			
			//jump function		
		} else {
			
			anim.SetFloat ("jumpTypes", 0f);		
		}
		
		
		
		
		
		
		
		
		//Debug.Log (s);
		float d = anim.GetFloat ("direction");
		float action = anim.GetInteger ("action");
		
		
		d = Input.GetAxis ("Horizontal");
		
		if (Input.GetAxis ("Vertical") < 0 && s <=0)
			s -= (s <= 0 && (Input.GetAxis ("Vertical") < 0)) ? 0.016f : 0;
		else 
		{
			s += (Input.GetAxis ("Vertical") > 0) ? 0.016f : (float)(-0.016f); // increase or decrement/elim the speed;
			s = (s < 0.05 && !(Input.GetAxis ("Vertical") > 0)) ? 0 : s;
			s = (s >= 1.00001 && !sprinting) ? 1 : s;
			s = (sprinting && s == 1) ? 1.5f : s;
			s = (sprinting && s >= 1.5) ? 1.5f : s;
			
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha5))
			anim.SetFloat("jumpTypes", 34f);
		
		
		lockWalk = (Input.GetKeyDown (KeyCode.LeftControl)) ? !lockWalk : lockWalk;
		if (Input.GetAxis ("Vertical") > 0) {
			s = (lockWalk) ? 0.3f : s;
		}
		
		
		anim.SetFloat ("speed", s);
		anim.SetFloat ("direction", d);
		if ((Input.GetAxis("Vertical") < 0) && s > 0.2)
			anim.SetBool ("slowDown", true);
		else
			anim.SetBool ("slowDown",false);		
		/*
				s += (Input.GetKey (KeyCode.Alpha9)) ? 0.02f : (float)(-s * 0.25); // increase or decrement/elim the speed;
				s = (s < 0.2 && !Input.GetKey(KeyCode.Alpha9)) ? 0 : s;
				s = (s >= 1.00001 && !sprinting) ? 1 : s;
				s = (sprinting && s == 1) ? 1.5f : s;
				if (s == 0)
						anim.SetBool ("slowDown", true);
				else
						anim.SetBool ("slowDown",false);
			*/
		// setup h variable as our horizontal input axis
		//float v = Input.GetAxis("Vertical");				// setup v variables as our v		ertical input axis
		anim.SetFloat ("speed", s);
		
		anim.SetBool ("haltOrder", false);
	}
	
	
	
	/// <summary>
	/// return true if we're cool and grounded, false otherwise, meaning time to start falling.
	/// <returns><c>true</c>, if pathing height was checked, <c>false</c> otherwise.</returns>
	bool checkPathingHeight()
	{
		
		return false;
	}
	
	
}
