#pragma strict
var fukAudio : AudioClip;
var oncePlay = true;
var LightIncrease = false;
var Arclight : Light;
function Start () {

}

function Update () {
if(Input.GetKey(KeyCode.Escape)){
Application.LoadLevel("Level2Beginning");Time.timeScale=1;}

if(gameObject.transform.position.x==305)
{
if(oncePlay){audio.PlayOneShot(fukAudio);
Debug.Log("asd");
oncePlay=false;
LightIncrease=true;

}
if(LightIncrease){
Arclight.intensity+=0.05;
}
}
if(gameObject.transform.position.z==684)
{
Application.LoadLevel("Level2Beginning");
}

}