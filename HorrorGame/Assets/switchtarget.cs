using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchtarget : MonoBehaviour {
    public GameObject parent;
    public GameObject target;
    public float speed;
    private AIPath control;
	// Use this for initialization
	void Start () {
      
        if (gameObject.GetComponentsInParent<AIPath>().Length > 0)
        {
            control = gameObject.GetComponentInParent<AIPath>();
            control.target = target.transform;
            control.speed = speed;
            control.canMove = true;
        }
        else
        {
            control = parent.GetComponent<AIPath>();
            control.target = target.transform;
            control.speed = speed;
            control.canMove = true;
        }
           
    
    }
	
	
}
