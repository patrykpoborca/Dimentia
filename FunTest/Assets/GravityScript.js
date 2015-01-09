#pragma strict
var speed = 6.0;

var jumpSpeed = 8.0;

var gravity = 20.0;

var inversiongravity = 0;

private var moveDirection = Vector3.zero;

private var grounded : boolean = false;
public var Rotate : boolean = false;

public var WantedRotation : Quaternion;

 

function FixedUpdate() {

    if (grounded) {

        // We are grounded, so recalculate movedirection directly from axes

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= speed;

        

        if (Input.GetButton ("Jump")) {

            moveDirection.y = jumpSpeed;

        }

    }

if (Input.GetButtonDown ("Fire1")) {

    

        if(inversiongravity == 0){

        inversiongravity = 1;

        Physics.gravity = Vector3(0, 10.0, 0);

        }

        else{

            inversiongravity = 0;

            Physics.gravity = Vector3(0, -10.0, 0);

        }

    }

    // Apply gravity

    if(inversiongravity == 1)

    {

        moveDirection.y -= gravity * Time.deltaTime;

    }

    else

    {

        moveDirection.y += gravity * Time.deltaTime;

    }

    // Move the controller

    var controller : CharacterController = GetComponent(CharacterController);

    var flags = controller.Move(moveDirection * Time.deltaTime);

    grounded = (flags & CollisionFlags.CollidedBelow) != 0;

    if(Input.GetButtonDown("Fire1"))

{

Rotate = true;

WantedRotation = transform.rotation;

WantedRotation.eulerAngles = new Vector3(180, 0, 0);

 

}

 

if(Rotate)

{

transform.rotation = Quaternion.Lerp(transform.rotation, WantedRotation, Time.deltaTime);

 

}

}

@script RequireComponent(CharacterController)