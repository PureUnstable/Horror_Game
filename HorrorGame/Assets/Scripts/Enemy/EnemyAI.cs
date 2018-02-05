using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TrackPlayer(Transform t)
    {

    }

    void OnTriggerEnter (Collider col)
    {
        //Debug.Log("Touching something");
        if (col.gameObject.tag == "Player")
            TrackPlayer(col.transform);
    }
}
