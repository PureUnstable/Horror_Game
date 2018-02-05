using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour {

    bool isThrowing;
    bool isHeld;
    Transform playerHold;

	// Use this for initialization
	void Start () {
        isHeld = false;
        isThrowing = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        /* INSERT PICKUP HERE */

	}

    void OnTriggerEnter(Collider col)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        Debug.Log(hitColliders.Length);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log(hitColliders[i].gameObject.ToString());
            if(hitColliders[i].tag == "Enemy")
            {
                Debug.Log(hitColliders[i].gameObject.ToString() + " heard sound");
            }
            i++;
        }

        //Destroy(col.gameObject);
    }

    float GetSoundLevelFromDistance(Vector3 soundPosition, Vector3 enemyPosition)
    {
        float x = 0f;

        return x;
    }

}
