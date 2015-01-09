#pragma strict

function Start () {
Time.timeScale=.2;
}

function Update () {
if(gameObject.transform.position.z==80){
Application.LoadLevel("levelZero");
Time.timeScale=1;}
if(Input.GetKey(KeyCode.Escape)){
Application.LoadLevel("levelZero");Time.timeScale=1;}
}