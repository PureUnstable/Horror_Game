using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour {
    private BoxCollider cam;
    private RaycastHit hit;
    private Interactable focus;
    private Ray ray;
    private Interactable interactable;
    private bool interact;
    private GameObject coll;

    // Use this for initialization
    void Start()
    {
        interact = false;
        cam = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && interact)
        {
            print("Clicked");


            interactable = coll.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);
                coll.SendMessage("PickUp");

            }


        }
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }

    void CanPickup(bool can)
    {
        interact = can;
    }

    void ItemObject(GameObject col)
    {
        coll = col;
    }

}
