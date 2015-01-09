#pragma strict
var Minicamera : Camera;
var Maincamera : Camera;
var State =0;
var LightHero : Light ;
var minFov: float = 15f;
var maxFov: float = 500f;
var sensitivity: float = 10f;

function Start () {
Minicamera.enabled=false;
Maincamera.enabled=true;
State=0;
LightHero.intensity=0;
}

function Update () {

if(State==1)
{
var fov = Minicamera.fieldOfView;
  fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
  fov = Mathf.Clamp(fov, minFov, maxFov);
  Minicamera.fieldOfView = fov;
  
}
if(Input.GetKeyDown(KeyCode.Tab))
{
if(State==0){
Minicamera.enabled=true;
Maincamera.enabled=false;
State=1;
LightHero.intensity=20;
}
else
{
Minicamera.enabled=false;
Maincamera.enabled=true;
State=0;
LightHero.intensity=0;
}
}

}