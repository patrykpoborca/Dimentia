using UnityEngine;
using System.Collections;


/*
 * the way this entire thing works
 * localFM is the gameManager script reference of gameFM so that all object refer to the same objects
 * there we manage the dimension, and the "state" of the game -1 = running, 0 = frozen, 1 = rewind 2 = forward
 * dimensions 1, 2, 3. 
 * 
 * the OBJECT (this script's possessor) holds a "collider" sphere and is responsible for adding itself to to the
 * player object's script (cTest in development) where cTest will from there forth manage this object, removing
 * it if it leaves the entrance range, this is simply because of the ontriggerenter behaviour.
 * 
 * rules:
 * in 3rd dimension you can pause / resume but cannot forward/rewind, if you get within the sphere radius you will
 * thaw the object
 * 
 * in 1st dimension you can pause/rewind/forward/resume, but you cannot move, you can only switch dimensions.
 */ 




public class objectTime : MonoBehaviour {
	
	// Use this for initialization
	public class tSave //general class to save any/all data for objects, so far AngleVelocity, Velocity, Position
	{
		
		public Vector3 movement = new Vector3();
		public Vector3 position = new Vector3();
		public Vector3 angular = new Vector3();
		public Quaternion rotation = new Quaternion(); 
		
		public tSave(Vector3 m, Vector3 p, Vector3 a, Quaternion r)
		{
			angular = a;
			rotation = r;
			movement = m;
			position = p;
		}
	}
	//end of general class
	
	
	SphereCollider sphere; // triger collider!!!
	
	public GameObject gameMan; // needed so that all things share gameFM !!!!!! 
	public gameFM localFM; // casted from gameMan
	
	
	public int gStatus;
	public int dimension;
	
	public tSave iteration;
	public float startTime;
	
	public bool resume; // uses this to reset velocity to objects when game is unpaused
	public tSave current; //used to keep track of the current "instance" of object on stack
	public int index; //used for clearing old arrays, not really needed now but when physics change during dimension shifts will be needed
	public int thirdState;
	
	
	public ArrayList stack = new ArrayList();
	
	
	
	
	void Start () 
	{	
		
		sphere = gameObject.AddComponent<SphereCollider> ();
		sphere.radius = 2;
		sphere.isTrigger = true;
		
		
		gameMan = GameObject.Find("gameFM");
		localFM = (gameFM)gameMan.GetComponent (typeof(gameFM));
		localFM.addSelf (this.gameObject, 0);
		
		iteration = null;
		thirdState = 0; // doing nothing /init
		index = -1; //initialization
		resume = false;
		updateFields ();
		
	}
	
	// Update is called once per frame
	void Update () {
		gameMan = GameObject.Find ("gameFM");
		if (rigidbody.detectCollisions == false)
			Debug.Log ("FFUCKED");
		
		if (gStatus == 100) {
			rigidbody.velocity = new Vector3(0,0,0);
			rigidbody.angularVelocity = new Vector3(0,0,0);
			
			return;
		}
		
		
		/// 3rd dimension
		//Debug.Log ("state: " + gameFM.gStatus + " -- " + thirdState);
		if (gStatus == 0 && thirdState == 1 && (iteration == null || iteration.movement == current.movement)) { // change to non 1d pause state
			
			if(iteration == null) // unit
			{
				rigidbody.useGravity = true;
				iteration = new tSave( new Vector3(0,0,0), new Vector3(0,0,0), new Vector3(0,0,0), new Quaternion(0,0,0,0));
				startTime = Time.time;         
			}
			else
			{// Vector3 m, Vector3 p, Vector3 a, Quaternion r
				float timeHolder = (Time.time - startTime) / 2; // change divider to change " duration of thaw"
				
				if(iteration.movement != current.movement) iteration.movement += timeHolder * current.movement;
				// no need for position 
				if(iteration.angular != current.angular) iteration.angular += timeHolder * current.angular;
				// no need for rotation
				
				if(Time.time - startTime >= 2) // fail safefeee
				{
					iteration.movement = current.movement;
					iteration.angular = current.angular;
					
				}
			}
			
		}
		
		/// end of 3rd dimension
		
		if(gStatus == -1)
		{
			if(resume) //when you resume you have to give all properties back to each object, velocities and gravity 
			{
				
				iteration = null; // clears it for the next time we use
				rigidbody.velocity = current.movement;
				rigidbody.angularVelocity = current.angular;
				
				rigidbody.useGravity = true;
				// cut everything AFTER index (because we're resuming at previous frame
				if(index +1 != stack.Count) stack.RemoveRange(index, stack.Count-index);
			}
			resume = false; // keep it false so we can know when it's our "first" pause update for later below, needed for logical comparissons
			index = -1; // we only need index when game is paused, at which point we set it to the size -1 (aka index of newest push onto stack)
			tSave inst = new tSave (rigidbody.velocity, transform.position, rigidbody.angularVelocity, rigidbody.rotation);
		
			stack.Add(inst); //here we save all data for each object for each stack
			//stack.Capacity=512;
			
		}
		else
			
		{ 
			if(index == -1) index = stack.Count-1;
			
			if(!resume) current = new tSave(rigidbody.velocity, transform.position, rigidbody.angularVelocity, rigidbody.rotation); // saves/initiatilizes current
			
			if(gStatus== 0 && !resume) // now only called once at the start
				pauseObject();
			else
				if(gStatus == 0)
			{} // do nothing simply used for logic at this point
			else
				if(gStatus == 1 && index != 0 && dimension == 1)
			{
				
				index--;  //as long as we don't go negative in index we can iterate backwards
				current = (tSave)stack[index];
				pUpdate();
			}
			else if(gStatus == 2 && index + 1 < stack.Count && dimension == 1)
			{
				// as long as we don't exceed the range of the array we can iterate forwards
				index ++;
				current = (tSave)stack[index];
				pUpdate();
			}
			else if(gStatus == 1)
			{
				localFM.setStatus(100);	
			}
			
		}
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "3rd Person Controller")
		{
			other.SendMessage("addDistance", Vector3.Distance(transform.position, other.transform.position));
			other.SendMessage("addObject", this.name);
			
		}
		
	}
	
	
	
	//used to "thaw" an object from the frozen state of time, thing "unwindws" over x seconds
	public void thaw()
	{
		
		if (dimension == 3) {
			thirdState = 1; //thawing or letting it resume to standard state.
			
		}
	}
	
	
	public void pauseObject()
	{
		
		resume = true; //once more we use "resume" for a logical comparisson
		rigidbody.velocity = new Vector3(0,0,0);
		rigidbody.angularVelocity = new Vector3(0,0,0);
		rigidbody.useGravity = false; //zero out all objects to simulate a "pause"
		
	}
	
	
	// refreezes the state
	public void freeze()
	{
		pauseObject ();
		thirdState = 0;
		iteration = null;
		
	}
	
	void pUpdate() //little function for updating the position whenever we update
	{
		
		transform.position = current.position;
		rigidbody.rotation = current.rotation; 
	}
	public void updateFields()
	{
		gStatus = localFM.getStatus ();
		dimension = localFM.getDimension ();
		
		// clear stack
		if (dimension == 3) {
			stack.Clear ();
			index = 0;
		}
		
	}
}
