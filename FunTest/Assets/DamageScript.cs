using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {
	public GameObject HealthBar;
	// Use this for initialization
	void Start () {
	
	}
	public void ApplyDamage(float damage)
	{
	//	Debug.Log ("Hey");
		HealthBar.SendMessage("ApplyDamage", 1);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
