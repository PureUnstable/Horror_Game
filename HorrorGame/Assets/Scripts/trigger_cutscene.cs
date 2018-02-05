using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_cutscene : MonoBehaviour {

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            print(gameObject.transform.childCount);
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            
          //  c.gameObject.GetComponent<InputController>().enabled = false;
         //   c.gameObject.GetComponent<PlayerWalk>().enabled = false;
           // c.gameObject.GetComponentInChildren<Camera>().enabled = false;
         //   c.gameObject.GetComponentInChildren<MouseLook>().enabled = false;
        
    }
        
    }
}
