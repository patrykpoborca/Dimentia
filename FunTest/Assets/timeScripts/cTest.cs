using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cTest : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		distance = 0;
		timer = 0;
		gameMan = GameObject.Find("gameManager");
		localFM = (gameFM)gameMan.GetComponent (typeof(gameFM));
		localFM.addSelf (this.gameObject, 1);
		updateFields ();
		
	}
	
	void Awake()
	{
		nearby = new List<GameObject>();
	}
	
	
	List<GameObject> nearby;
	float timer;
	
	public int dimension, gStatus;
	
	public float distance;
	public GameObject gameMan; // needed so that all things share gameFM !!!!!! 
	public gameFM localFM; // casted from gameMan
	
	// Update is called once per frame
	void Update () {
		
		
		if (timer <= 0.1) // used to aid performance... lower as needed.
			timer += Time.deltaTime;
		else { // ever x micro seconds we remove anything that exited the range... aka freeze them
			timer = 0; 
			if(nearby.Count > 0)
				removeExits();
		}
		
		if (dimension == 3 && nearby.Count != 0)
			THAW ();
		
		
		//	Collider[] hitColliders = Physics.OverlapSphere (transform.position, 10f);
		
		
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "moveit")
		{
			Debug.Log("moveit");
			
		}
		
	}
	void OnTriggerExit(Collider other) {
		if (other.tag == "moveit")
			Debug.Log ("exiting");
		
	}
	
	
	void THAW()
	{
		
		for (int a=0; a < nearby.Count; a ++) {
			nearby[a].SendMessage("thaw");
		}
	}
	
	void OnGUI()
	{
		if(nearby.Count != 0)
			GUI.TextArea (new Rect (0, 0, 100, 100), nearby[0].name);
		GUI.TextArea (new Rect (500, 0, 100, 30), "dimension" + dimension);
		
	}
	
	
	//iterates through all objects that leave 
	void removeExits()
	{
		
		List<int> ints = new List<int> (); // we remove the found exits after, to not corrupt for iteration
		Vector3 tempPos;
		for(int a=0; a < nearby.Count; a++)
		{
			tempPos=  nearby[a].rigidbody.position;
			if(Vector3.Distance(tempPos, transform.position) > distance )
			{
				
				if(dimension == 3)
					nearby[a].SendMessage("freeze");
				ints.Add(a);
			}
		}
		
		for (int a = 0; a < ints.Count; a++) {
			nearby.RemoveAt(ints[a]);
		}
	}
	
	// addobject/distance is used by object time 
	
	void addObject(string Name)
	{
		
		nearby.Add( GameObject.Find (Name));
	}
	
	void addDistance(float dist)
	{
		
		if (dist > distance) {
			if (distance == 0)
				distance = dist + 0.01F; // small threshold for stutter steps
			else
				distance = dist;
		}
	}
	
	
	public void updateFields()
	{
		
		gStatus = localFM.getStatus ();
		dimension = localFM.getDimension ();
	}
	
}
