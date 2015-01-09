/*
 *Dimentia UI by Patryk Poborca 
 * GeekPpl
 * 2/24/14
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _item : MonoBehaviour {
	
	public Texture Grid;
	public Dictionary<string, ITEM> _list;
	
	
	public class ITEM
	{
		
		public Texture IMAGE; // the item's image
		public Texture IMAGE2; // the rotated image
		public Rect RECT; // current position
		public Rect rRECT; // "safe" return, last "snapped" position
		public Dictionary<string, string> itemStats;
		public int width; // in terms of grid
		public int height; // in terms of grid
		public List<Vector2> brecs; //keeps track of item's inventory location
		public Vector2 stringSize; // dimensions of itemstats 
		public ITEM(Texture I, Texture IR, Rect S, Dictionary<string, string> istats, int w, int h )
		{

			brecs = new List<Vector2>();
			height = h; width = w;
			itemStats =  istats; // list of the "key" and the "value" of each item, every item has a NAME and TYPE, based on TYPE it has
			//certain attributes so cost, description, etc.
			IMAGE = I;
			IMAGE2 = IR;
			RECT = S;    
			rRECT = new Rect(S.x, S.y, S.width, S.height);
			print ();//has to be after itemstats
		}

		public string print()
		{
			bool maxed = false;
			int xx = 0;
			int yy = 0;
			string rVal = "";
			foreach (KeyValuePair<string, string>  x in itemStats) {
				xx++;
				maxed = false;
				rVal = rVal + x.Key + ": " + x.Value + "\n";
				//if(rVal.Length >100) Debug.Log ("Size of yy" + yy);
					for( int b = 0 ; b < rVal.Length; b+= 21)
					{
					if(b!= 0){ xx++; maxed = true;}
					}
					if(maxed && (20 > yy)) yy = 20;
					else 
				if(rVal.Length > yy) yy = rVal.Length;
				
				//Debug.Log("HERE's size : " + rVal.Length);
			}
			yy += 2; //tack on the indentation added to the max string
			stringSize = new Vector2 (yy, xx); // columns \ rows


			return rVal;
		
		}

		public ITEM rot() // returns a sideways copy
		{
			//Debug.Log ("Texture 1: " + IMAGE.ToString());
			//Debug.Log ("texture 2: " + IMAGE2.ToString ());
			Rect R = RECT;
			Rect k = new Rect (R.x, R.y, R.height, R.width);
			ITEM rVal = new ITEM (IMAGE2, IMAGE, k, itemStats, height, width); // switches the textures files to rotate the image.
			return rVal;
		}
	}

	public void addition(string key,string k, Dictionary<string, string> dat)
	{

		dat.Add (key, k);

		/*
		List<string> parser = new List<string>();
		if (key.Length + k.Length <= 20) {
			dat.Add(key, k);	
			return;
				}
		int increment =  k.IndexOf(" ", 15);
		for (int a=0; a < k.Length; a+= increment) {
			if(a != 0) increment =  k.IndexOf(" ", a + 15);
			if(a+ increment >= k.Length || k.IndexOf(" ", a + 15) == -1)
			{parser.Add(k.Substring(a, k.Length - a ));
				break;
			}
			else
			{

				parser.Add(k.Substring(a, increment));

			}

		}

		for (int a=0; a < parser.Count; a++) 
		{
			if(a ==0)
				dat.Add(key, parser[a]);
			else dat.Add(key + "hack" + a, parser[a]);
		}
		*/
		}

	
	// Use this for initialization
	void Start () 
	{
		_list = new Dictionary<string, ITEM>();
		int width = 1; //width/height of item
		int height = 4;
		Rect spot = new Rect (200, 10, width * Grid.width, height * Grid.height); // implement a get "free" method in "inventory.cs"

		Texture IM = (Texture) Resources.LoadAssetAtPath("Assets/STAR.PNG", typeof(Texture));
		Texture IM2 = (Texture) Resources.LoadAssetAtPath("Assets/STAR2.PNG", typeof(Texture));
		Dictionary<string, string> dat = new Dictionary<string, string>();
		addition ("name", "Star", dat);
		addition ("description", "this is a descriptionkhi this should be paragraph two increment that keeps going on and then on and on then even more this is the end, dat", dat);
		_list.Add("Star" ,new ITEM(IM, IM2, spot, dat, width, height));
		//****************** nextItem
		width = 2; //width/height of item
		height = 2;
		spot = new Rect (10, 200, width * Grid.width, height * Grid.height);
		IM = (Texture) Resources.LoadAssetAtPath("Assets/yellow.PNG", typeof(Texture));
		IM2 = (Texture) Resources.LoadAssetAtPath("Assets/purple.PNG", typeof(Texture));
		dat = new Dictionary<string, string>();
		addition ("name", "yellow", dat);
		_list.Add("yellow" ,new ITEM(IM, IM2, spot, dat, width, height));

		//****************** nextItem
		width = 2; //width/height of item
		height = 1;
		spot = new Rect (10, 200, width * Grid.width, height * Grid.height);
		IM = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/gun.PNG", typeof(Texture));
		IM2 = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/gun2.PNG", typeof(Texture));
		dat = new Dictionary<string, string>();
		addition ("name", "Portal Gun", dat);
		addition ("description", "This is a gun that may or may not fire portals.", dat);
		_list.Add("Portal Gun" ,new ITEM(IM, IM2, spot, dat, width, height));

		//****************** nextItem
		width = 2; //width/height of item
		height = 1;
		spot = new Rect (10, 200, width * Grid.width, height * Grid.height);
		IM = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/mp5k.PNG", typeof(Texture));
		IM2 = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/mp5k2.PNG", typeof(Texture));
		dat = new Dictionary<string, string>();
		addition ("name", "MP5K", dat);
		addition ("description", "A gun that fires bullets, faster.", dat);
		_list.Add("MP5K" ,new ITEM(IM, IM2, spot, dat, width, height));

		//****************** nextItem
		width = 2; //width/height of item
		height = 1;
		spot = new Rect (10, 200, width * Grid.width, height * Grid.height);
		IM = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/pistol.PNG", typeof(Texture));
		IM2 = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/pistol2.PNG", typeof(Texture));
		dat = new Dictionary<string, string>();
		addition ("name", "Pistol", dat);
		addition ("description", "A gun that fires bullets.", dat);
		_list.Add("Pistol" ,new ITEM(IM, IM2, spot, dat, width, height));

		//****************** nextItem
		width = 1; //width/height of item
		height = 1;
		spot = new Rect (10, 200, width * Grid.width, height * Grid.height);
		IM = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/box.PNG", typeof(Texture));
		IM2 = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/box2.PNG", typeof(Texture));
		dat = new Dictionary<string, string>();
		addition ("name", "Wooden Crate", dat);
		addition ("description", "Yup it's a box.", dat);
		_list.Add("Wooden Crate" ,new ITEM(IM, IM2, spot, dat, width, height));

		//****************** nextItem
		width = 2; //width/height of item
		height = 2;
		spot = new Rect (10, 200, width * Grid.width, height * Grid.height);
		IM = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/arc.PNG", typeof(Texture));
		IM2 = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/arc2.PNG", typeof(Texture));
		dat = new Dictionary<string, string>();
		addition ("name", "Arc Reactor", dat);
		addition ("description", "A very powerful reactor.", dat);
		_list.Add("Arc Reactor" ,new ITEM(IM, IM2, spot, dat, width, height));

		//****************** nextItem
		width = 1; //width/height of item
		height = 1;
		spot = new Rect (10, 200, width * Grid.width, height * Grid.height);
		IM = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/money.PNG", typeof(Texture));
		IM2 = (Texture) Resources.LoadAssetAtPath("Assets/itemDict/money2.PNG", typeof(Texture));
		dat = new Dictionary<string, string>();
		addition ("name", "Money", dat);
		addition ("description", "Such money. Wow.", dat);
		_list.Add("Money" ,new ITEM(IM, IM2, spot, dat, width, height));


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}