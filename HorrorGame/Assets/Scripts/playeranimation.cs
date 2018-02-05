using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimation : MonoBehaviour {
    Animator p;
    int moving = Animator.StringToHash("w_key");
    // Use this for initialization
    void Start () {
        p = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float move = Input.GetAxis("Translate");
        p.SetFloat("Speed", move);
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            p.SetBool(moving, true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            p.SetBool(moving, false);
        }
    }
}
