using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyanimation : MonoBehaviour {

    private Animator anim;
    private AIPath path;
    int trigger = Animator.StringToHash("cutscene_1");
    int trigger2 = Animator.StringToHash("cutscene_2");
    int trigger3 = Animator.StringToHash("scream");
    
    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        path = gameObject.GetComponent<AIPath>();
	}
	
	// Update is called once per frame
	void Update () {
        float m = path.speed;
        anim.SetFloat("speed", m);
        
        if(gameObject.transform.GetChild(2).gameObject.activeInHierarchy == true)
        {
            anim.SetTrigger(trigger);
        }
        if (gameObject.transform.GetChild(3).gameObject.activeInHierarchy == true)
        {
            anim.SetTrigger(trigger2);
        }
        if (gameObject.transform.GetChild(4).gameObject.activeInHierarchy == true)
        {
            anim.SetTrigger(trigger3);
        }
    }
}
