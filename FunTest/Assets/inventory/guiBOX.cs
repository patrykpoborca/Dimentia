/*
 *Dimentia UI by Patryk Poborca 
 * GeekPpl
 * 2/24/14
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class guiBOX : MonoBehaviour {
	
	
	
	//**** utility classes
	
	public class bRect
	{
		public Rect spot; // saves size/coordinates
		public int ON; // whether being occupied
		public Vector2 coord; // the "coord" of the forloops
		public bRect(Rect r, Vector2 c)
		{
			coord = c;
			spot = r;
			ON = -1;
			
		}
		
	}


	//**** util functs

	//mouseX adjustment
	public float mouseX(float x, float width)
	{
		if ((Screen.width - x) <= (width + 10))
						return x - width;

		return x + 13;

		}

	// to "debug" you can expand x or y to 1000 is the error
	public float textMath(float size, bool x)
	{

		if (x) {
						if (size < 15) {
								return 7F * size;
										
						}
						if (size >= 15 && size <30)
								return 6F * size;


			return 1000;
				} else {
			if (size < 8) {
				return 20F * size;
						
			}
			if (size >=8 && size < 14)
				return 16F * size;

			return 1000F * size;
		}
	}

	public bool populate(_item.ITEM it) // use this to test when you attempt to pickup items
	{

		backpack.Add (it); // temporarily add
		for (int a = 0; a < backpack.Count; a++) 
		{

			backpack[backpack.Count-1].RECT.x = group[a].spot.x;
			backpack[backpack.Count-1].RECT.y = group[a].spot.y;
			if(canPlace(backpack.Count-1))
				return true; //rect has been placed, no more work needed
				}

			   backpack.RemoveAt(backpack.Count-1); // remove before returning, means no found possible slots
		return false; // couldn't place so false;
		}
	
	public void rotate(int a)
	{
		_item.ITEM temp;
		temp = backpack [a];
		backpack[a] =  backpack [a].rot ();
		if (canPlace (a)) {

			backpack.Add(temp);
			falsify(backpack[backpack.Count-1], backpack.Count-1); // revert placed values
			backpack.RemoveAt(backpack.Count-1);
						return;
				}
		else backpack[a] = temp;
			  


	}
	
	public bool canPlace(int a) //passed the index of equipped for current location to check if you can place
	{
		Rect zzz;
		Vector2 temp = new Vector2 (backpack [a].RECT.x + 20, backpack [a].RECT.y);
		int found = -1;
		for (int x=0; x< group.Count; x++) { 
			if(group[x].spot.Contains(temp)){
				found = x;
				break;
			}
		}
		//found object is occupied or
		
		if (found == -1 || (group [found].ON != -1 && group [found].ON != a))  // we found nothing that collides with the corner of the dragged square
			return false;
				
		List<Vector2> setV = new List<Vector2>();
		
		//now we check to see if the rest of the cells are filled up
		for (int x= 0; x < backpack[a].width; x++) {
			for(int y = 0; y < backpack[a].height; y++)
			{
				temp = new Vector2(group[found].coord.x + x, group[found].coord.y + y); //yC /xc reference
try{
				if(lookup[temp].ON != -1 && lookup[temp].ON != a) return false; // if "on" return false;
				//else add
				}
				catch (KeyNotFoundException e)
				{
					backpack[a].RECT = new Rect(backpack[a].rRECT);
					return false;
				}
				setV.Add(temp); //use this to avoid flipping values without having it all set
			}
		}
		
		//switch old values to false
		falsify (backpack [a], a);

		backpack[a].brecs.Clear();
		//finally we've avoided returning false entire time, time to set the "on" values before return true
		for (int x= 0; x < setV.Count; x++) {
			lookup [setV [x]].ON = a; //on to refer if we move on itself
			
			backpack[a].brecs.Add(new Vector2(setV[x].x, setV[x].y));
			
		}
		//move the baby into its spot
		
		backpack [a].rRECT.x = group [found].spot.x;
		backpack [a].rRECT.y = group [found].spot.y;
		backpack [a].RECT = new Rect (backpack [a].rRECT);
		
		
		
		return true; // we couldn't find any reason to return false in surrounding cells
	}

	public void falsify(_item.ITEM bpack, int a)
	{
		Debug.Log("-------------------");
		for(int aa =0 ;aa < group.Count; aa++)
			if( group[aa].ON != -1)Debug.Log(group[aa].ON + "   " + bpack.brecs.Count);

		Vector2 temp;
		for (int x=0; x < bpack.brecs.Count; x++) {
			temp = bpack.brecs[x];
			lookup[temp].ON = -1;
			
		}
	}
	//############## GLOBALS
	int W, H;
	Vector2 menuPos;
	int menuActive;	
	int current;
	int menuChoice;
	int menuClicked;
	bool antiRight;
	public bool dragging; int index; // used for keeping track of drags/resets
	public List<bRect> group; //the group of rectangles
	public Dictionary <Vector2, bRect> lookup; //used to look up elements in group (cpu effeciency)
	
	public Texture grid; // image for the grid of inventory
	public _item items; // item object var
	public List<_item.ITEM> backpack; //currently backpack items
	public int antiDouble;
	
	// Use this for initialization
	void Start () {
		menuClicked = -1;
		antiRight = true;
		menuChoice = -1;
		menuPos = new Vector2 ();
		menuActive = 0;
		current = 0;
		dragging = false;
		W = Screen.width; H = Screen.height;
		backpack = new List<_item.ITEM> (); // init equipped featuring items in inventory
		group = new List<bRect> (); //initialize the arrayList
		lookup = new Dictionary<Vector2, bRect>();
		int xC = 10; // used to show the count of grid boxes
		int yC = 10;
		//variables for rect
		int left = W - (xC * grid.width);
		int top = H - (yC * grid.height);
		Rect pass;
		antiDouble = 0;

		
		//*** populating grid
		bRect filler;
		for(int a=0; a < yC; a++)
		{
			
			for(int b=0; b < xC; b++)
			{ pass = new Rect(left + (b*grid.width),  (a*grid.height), grid.width, grid.height);
				
				
				if(xC * yC -1 >= group.Count )
				{
					filler = new bRect(pass, new Vector2(b, a)); // needed to be flipped
					group.Add(filler); 
					lookup.Add(new Vector2(b, a) , filler); // these should both reference the same memory loc
					//print (gridArray.Count);
				}
			}
			
			
		}
		//*** end of populating
		//NEEDS TO POPULATE ITEMS AFTER GRID!!!!!!!

		//populating items (Insert a "update" method, for whenever you add an item)
		/*backpack.Add (items._list["Star"]);
		backpack.Add (items._list["yellow"]);*/
		populate (items._list ["Star"]);
		populate (items._list ["yellow"]);
		
	}
	


	
	
	void OnGUI() {

		Rect temp;


		// keeps track of whether or not right click is down 

		if (Input.GetMouseButtonDown (1))
						antiRight = false;
		if (Input.GetMouseButtonUp (1))
						antiRight = true;
		// end of right click down

		
		Event M = Event.current;
		Vector2 mouse = M.mousePosition;
		_item tItem;
		
		// draws inventory grid ##############################################
		for (int a=0; a< group.Count; a++) 
		{
			temp = group[a].spot;
			GUI.DrawTexture(temp, grid , ScaleMode.ScaleToFit, true, 0F);
		}
		// #####################################################################
		

		// draws items on grid
		for (int a=0; a< backpack.Count; a++) {

			GUI.DrawTexture(backpack[a].RECT, backpack[a].IMAGE , ScaleMode.StretchToFill, true, 0F); // Draw info texture
		}
		// #################################################################### 


		string reference;
		/// Movement section of onGui
		GUI.TextArea (new Rect (0, 0, 100, 100), M.mousePosition.ToString ());
		string sTemp;

		for (int a=0; a< backpack.Count; a++) {
			temp = backpack[a].RECT; //checking all equipped draggable items
			
			if(Input.GetMouseButtonUp(0)) //avoids letting go of item when moving mouse fast
			{dragging = false;
				current = -1;
			}

			//draws text field info ########################################################
			sTemp =  backpack[a].print();
			float xx = mouseX(mouse.x, textMath(backpack[a].stringSize.x, true));

			string[] menuItems = new string[] {"rotate", "other stuff...", "exit"};  //add menu buttons here
			// DRAW MENU ITEM OR DRAW 


		
			//draws menu in a static position
			if(menuActive == 1 || menuActive == 2) // active has a 2-1 phase to avoid making the menu draw where the mouse is moving around to
			{
				if(menuActive == 2)
				{
				menuPos = new Vector2(mouse.x + 13, mouse.y);
					menuActive = 1;
				}

				menuChoice = GUI.SelectionGrid(new Rect(menuPos.x, menuPos.y, 100, 100), menuChoice, menuItems,1); 

			} // end of menu


			// chooses a selection
			if(menuActive != 0  && menuChoice > -1) // add 
			{

				switch(menuChoice)
				{
				case 0:
					rotate (menuClicked);
					break;
				case 1: 
				Debug.Log("Other");
					break;
				case 2:

					Debug.Log("exit");
					break;
				default:
					Debug.Log("error");
					break;

				}
				menuClicked = -1; 
				menuActive = 0; //turns off the state of menu
				menuChoice = -1; // -1 flags 

			}
			/*else if(menuActive != 0 && Input.GetMouseButtonDown(0) && menuChoice == -1) ///// THis is not working.
			{
				menuChoice = -2;
			}
			else if(menuChoice == -2 && menuActive != 0 )
			{
				menuActive = 0;
				menuChoice = -1;
			}*/
			//

			//hover selection

			if(temp.Contains(mouse))
			{

				//Debug.Log ("Size of yy" + backpack[a].stringSize.x);
				if(menuActive ==0) GUI.TextArea(new Rect(xx, mouse.y, textMath(backpack[a].stringSize.x, true), textMath(backpack[a].stringSize.y, false)), sTemp);

				if(antiDouble == 0 && Input.GetMouseButtonDown(1))
				{menuActive = 2;
					menuClicked = a;
					//antiDouble = 1;
					//rotate(a);
				}

			}

			// end of draw field ##########################################################


			if((dragging || temp.Contains(mouse))&& EventType.MouseDrag == M.type && antiRight)   
			{//Debug.Log("Mouse is: " + Input.GetMouseButtonDown(1).ToString());
				dragging = true;

				if (   (current == -1 || current == a)) //used to continue dragging as long asbutton down
				{
					

					if(current == -1) current =a;

					index = a;
					temp.x += Event.current.delta.x;
					temp.y += Event.current.delta.y;
					backpack[a].RECT = temp; 
						break;

				}
				
				dragging = true;
				
			}
			else // this is the "RETURN" state, if it doens't fit in grid.
			if(Input.GetMouseButtonUp(0)  ){
				if(index !=-1 )
				{
					if(!canPlace(index))
					{
						reference = backpack[index].itemStats["name"];
						backpack[index].RECT =  backpack[index].rRECT;
						break;
						
					}
				}
				
				
				index =-1;
				
			}
			
			
		}
		
		
	}
	// Update is called once per frame
	void Update () {
		
	}
}