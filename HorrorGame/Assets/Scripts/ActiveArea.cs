using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveArea : MonoBehaviour {

	public SphereCollider playerSphereCol;
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		col.gameObject.SetActive (true);
	}

	void OnTriggerExit(Collider col)
	{
		col.gameObject.SetActive (false);
	}
}
