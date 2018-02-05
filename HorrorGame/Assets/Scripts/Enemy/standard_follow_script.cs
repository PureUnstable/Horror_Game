using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class standard_follow_script : MonoBehaviour {

	private Transform target;
	public float maxdist = 10;
	public float mindist = 3;
	public float move_speed = 3;
	public float rotation_speed = 3;
	private bool player_triggered = false;
	private Transform myTransform;
	void Awake()
	{
		myTransform = transform;
	}


	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag("Player").transform;
	
	}



	// Update is called once per frame
	void Update () {
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			Quaternion.LookRotation(target.position - myTransform.position), rotation_speed*Time.deltaTime);
		if (Vector3.Distance (myTransform.position, target.position) <= maxdist) {
			myTransform.position += myTransform.forward * move_speed * Time.deltaTime;
		}
	}
}
