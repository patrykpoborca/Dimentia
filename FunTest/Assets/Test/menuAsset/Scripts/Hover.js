#pragma strict
/* Hover JavaScript file
 * Simple JS file to change the shade when mouse hovers over the object
 * No need to interact with any other external object so written with JS
 * @author Karthik Rajakumar Thiagarajan
 * @version 1.0, Feburary 2014
 */
function Start () {

}

function Update () {


}
  function OnMouseEnter() {
        
        renderer.material.color=Color.grey;
    }
      function OnMouseExit() {
        renderer.material.color=Color.white;
    }