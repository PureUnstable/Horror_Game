using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplayer : MonoBehaviour {
    public GameObject obj;
    public Quaternion rotation;
	// Use this for initialization
	void Start () {
        obj.transform.position = gameObject.transform.position;
        obj.transform.rotation = rotation;
	}
	
}
