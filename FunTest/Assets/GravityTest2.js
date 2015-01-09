#pragma strict

	var  gravity : float = -1f;
	
	function Update () 
	{	if(Input.GetKeyDown("t"))
		{gravity = -gravity;
		Physics.gravity=Vector3(0,5,0);
		}
		gameObject.rigidbody.velocity.y += gravity * Time.deltaTime;
		
	}
	
	
		
	