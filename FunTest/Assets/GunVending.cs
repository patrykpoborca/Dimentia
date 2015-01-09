using UnityEngine;
using System.Collections;

public class GunVending : MonoBehaviour {
	public GameObject cheapGun;
	public GameObject mediumGun;
	public GameObject InsaneGun;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void ActionWork () {
		Rigidbody gun;
		if(gameFM.moneyValue>500)
		{	gun=Instantiate (InsaneGun, new Vector3 (25051,10224, -29138) , Quaternion.identity) as Rigidbody;
			gameFM.moneyValue-=500;
		}
		else if(gameFM.moneyValue>200)
		{	gun=Instantiate (mediumGun, new Vector3 (25051,10224, -29138) , Quaternion.identity) as Rigidbody;
			gameFM.moneyValue-=200;
		}
		else if(gameFM.moneyValue>100)
		{	gun=Instantiate (cheapGun, new Vector3 (25051,10224, -29138) , Quaternion.identity) as Rigidbody;
			gameFM.moneyValue-=100;
		}
	    
	}
}
