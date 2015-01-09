#pragma strict
var AudioSound : AudioClip;
function Start () {

gameObject.audio.PlayOneShot(AudioSound);
}

function Update () {
if(gameObject.transform.position.x==338)
Application.LoadLevel("level1");
if(Input.GetKey(KeyCode.Escape)){
Application.LoadLevel("level1");Time.timeScale=1;}


}