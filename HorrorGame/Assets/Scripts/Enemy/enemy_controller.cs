using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_controller : MonoBehaviour {

	private Transform target;
	private Transform mytransform;
	public float move_speed = 3;
	public float rotation_speed = 3;
	public float wander_radius;
	public float wander_timer;
	private NavMeshAgent agent;
	private float timer;
	private bool should_roam;
	private bool should_follow;
	private float dist_target;
	public AnimationState animation;

	void Awake()
	{
		mytransform = transform;
	}

	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent> ();
		timer = wander_timer;
		should_roam = false;
		should_follow = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			
		}
	}

	void OnTriggerExit(Collider other)
	{
		
	
	}

	void follow_script(Transform myTransform, Transform target)
	{
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,Quaternion.LookRotation(target.position - myTransform.position), rotation_speed*Time.deltaTime);
		myTransform.position += myTransform.forward * move_speed * Time.deltaTime;

	}

	void roam_script(Transform transform, NavMeshAgent agent,float wander_timer, float wander_radius)
	{
		timer += Time.deltaTime;

		if (timer >= wander_timer) {
			Vector3 newPos = RandomNavSphere (transform.position, wander_radius, -1);
			agent.SetDestination (newPos);
			timer = 0;
		}
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
	{
		Vector3 randDirection = Random.insideUnitSphere * dist;
		randDirection += origin;
		NavMeshHit navHit;

		NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
		return navHit.position;
	}

	// Update is called once per frame
	void Update () {
		
		dist_target = Vector3.Distance(mytransform.position, target.position);
		if (should_follow == true) {
			follow_script (mytransform, target);
		}
		if (should_roam == true) {	
			roam_script (mytransform, agent, wander_timer, wander_radius);
		}
	}

	public void PauseAnimation()
	{
		animation.speed = 0;
	}

	public void ResumeAnimation()
	{
		animation.speed = 1;
	}
}
