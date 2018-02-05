using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_camara : MonoBehaviour {

	public Camera[] cutscene;
	public Camera current;

	// Use this for initialization
	void Start () {
		cutscene[0].enabled = false;
		cutscene[0].GetComponent<AudioListener> ().enabled = false;
		current.enabled = true;
		current.GetComponent<AudioListener> ().enabled = true;
	}
	void OnTriggerEnter(Collider collider)
	{
		collider.gameObject.SetActive(false);
		current.enabled = false;
		current.GetComponent<AudioListener> ().enabled = false;
		cutscene[0].enabled = true;
		cutscene[0].GetComponent<AudioListener> ().enabled = true;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
