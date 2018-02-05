using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Reticle : MonoBehaviour {

	public Texture2D crosshair;
	private Vector2 window_size;
	private Rect crosshair_rect;
	private BoxCollider selectionzone;
	// Use this for initialization
	void Start () {
		if (GetComponent<BoxCollider> () == null) {
			selectionzone = gameObject.AddComponent (typeof(BoxCollider)) as BoxCollider;
		} else {
			selectionzone = GetComponent<BoxCollider> ();
		}
		selectionzone.isTrigger = true;
		selectionzone.size = new Vector3 (0.1F,0.1F,6.5F);
		selectionzone.center = new Vector3 (0F,0F,1.5F);
		crosshair = new Texture2D (2,2);
		window_size = new Vector2 (Screen.width, Screen.height);
		CalculateRect ();
	}
		
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "moveable") {
			gameObject.SendMessage ("ItemObject", col.gameObject);
			gameObject.SendMessage ("CanPickup", true);
			col.gameObject.SendMessage ("CanPickup", true);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "moveable") {
		//	gameObject.SendMessage ("ItemObject", null);
			gameObject.SendMessage ("CanPickup", false);
			col.gameObject.SendMessage ("CanPickup", false);
		}
	}

	// Update is called once per frame
	void Update () {
		if (window_size.x != Screen.width || window_size.y != Screen.height) {
			CalculateRect ();
		}
	}

	void CalculateRect()
	{
		crosshair_rect = new Rect ((window_size.x-crosshair.width)/2F, (window_size.y-crosshair.height)/2F , crosshair.width, crosshair.height);
	}



	void OnGUI(){

		GUI.DrawTexture (crosshair_rect, crosshair);
	
	}

}
