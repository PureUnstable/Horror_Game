using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    private AIPath b;

    // Use this for initialization
    void Start()
    {

        b = GetComponent<AIPath>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {

            b.canMove = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (b.canMove == false)
        {
            b.canMove = true;
        }
    }

}