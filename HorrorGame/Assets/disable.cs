using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disable : MonoBehaviour {

    public GameObject parent;
    private AIPath dis;
	// Use this for initialization
	void Start () {
        dis = parent.GetComponent<AIPath>();
        dis.canMove = false;
	}
	
}
