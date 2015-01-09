using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class gameFM : MonoBehaviour {
	
	public static int gStatus;
	public KeyCode antiStuck;
	public int dimension;
	public  static int fStatus;
	public int fDimension;
	public cTest singleObject;
	public GameObject money1,money2,money3;
	public static int moneyValue=0;
	List<objectTime> objectList;
	private static bool DestroyBool = false;
	
		// Use this for initialization
	void Start () {
		
		fDimension = dimension;
		fStatus = gStatus;
		

	}
	public void UpdateMoney(int x)
	{moneyValue += x;
	}
	public void ResetMoney()
	{
		Debug.Log ("Yo");moneyValue = 0;
	}
	void Awake()
	{
		dimension = 1; // starts in first dimension
		gStatus = -1;
		objectList = new List<objectTime>();
		moneyValue = 0;
		if(DestroyBool)
		DontDestroyOnLoad(this);
		DestroyBool = false;
	}
	
	public int getStatus()
	{
		return gStatus;
	}
	
	public int getDimension()
	{
		return dimension;
	}
	
	public void setStatus(int x)
	{
		gStatus = x;
	}
	
	void updateFields(string selection)
	{
		if (selection == "dimension") 
			fDimension = dimension;
		else  // "time"
			fStatus = gStatus;
		
		for (int a=0; a < objectList.Count; a ++) {
			objectList[a].updateFields(); // has object update all fields on instance change
			
		}
			singleObject.updateFields ();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		money1=GameObject.Find("digitOne");
		money2=GameObject.Find("digitTwo");
		money3=GameObject.Find("digitThree");
//		Debug.Log (moneyValue);
		money1.SendMessage("UpdateMoney", moneyValue);
		money2.SendMessage("UpdateMoney", moneyValue);
		money3.SendMessage("UpdateMoney", moneyValue);

		// if a change is detected, please let all sub functions know...
		if (gStatus != fStatus) 
			updateFields ("time");
		if (dimension != fDimension)
			updateFields ("dimension");
		
		
		if (Input.GetKeyDown (KeyCode.G)) 
			dimension = (dimension == 1) ? 3 : 1;
		
		if (gStatus == 100) {// this is the "antistuck" state, if user refuses to let go,
			
			
			if(Input.GetKeyUp(antiStuck)) gStatus = 0;
			
			return;
		}
		if(Input.GetKey(KeyCode.R)) //resume
		{
			gStatus = -1;
		}
		
		if (Input.GetKey (KeyCode.P)) { //pause
			gStatus = 0;
		}
		
		if (gStatus != -1) {
			if (Input.GetKey (KeyCode.Comma)) { gStatus = 1; antiStuck = KeyCode.Comma; }  // rewind
			else
			if (Input.GetKey (KeyCode.Period)) { gStatus = 2; antiStuck = KeyCode.Period;} 		// forward	
			else gStatus = 0; // stops moving them when no longer pressed
		}
	}
	
	// objects add themselves to list to reduce cpu burden on game state changes
	public void addSelf(GameObject o, int type)
	{
		if(type == 0) //objectTime
			objectList.Add ((objectTime)o.GetComponent(typeof(objectTime)));
		if (type == 1) // cTest
			singleObject = (cTest)o.GetComponent (typeof(cTest));
	}
	
}

