using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {
	public int MoneyValue;
	public GameObject GameFM;
	// Use this for initialization
	void Start () {
		GameFM = GameObject.Find ("gameFM");
	}
	void ActionWork()
	{
		GameFM.SendMessage ("UpdateMoney",MoneyValue,SendMessageOptions.DontRequireReceiver);
		MoneyValue = 0;
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {

	}
}
