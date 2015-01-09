#pragma strict

function Start () {

}

function Update () {

}

function  OnCollisionEnter (col : Collision)
{
Debug.Log(col.gameObject.name);
}