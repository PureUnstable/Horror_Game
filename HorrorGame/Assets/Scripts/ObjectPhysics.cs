using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhysics : MonoBehaviour
{

    public Transform player;
    public Transform player_cam;
    public float throw_force = 10;
    public float pickup_dist = 2.5F;
    public float rotation_speed = 3;
    bool can_pickup = false;
    bool is_carried = false;
    bool grounded = false;
    int air_time = 0;
    public Sprite display;
    private GameObject ui_use;
    private SpriteRenderer render;
    private bool collide = false;


    void OnTriggerEnter(Collider other)
    {
        if (is_carried && other.gameObject.tag == "environment")
        {
            collide = true;
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (is_carried == false && col.gameObject.tag == "environment")
        {
            grounded = true;

        }
    }

    void OnCollisionEnter(Collision col)
    {
        print(air_time);
        Physics.OverlapSphere(gameObject.transform.position, air_time * 0.1F);
        DebugExtension.DebugWireSphere(gameObject.transform.position, air_time * 0.1F, 5);

        air_time = 0;
    }



    void OnCollisionExit(Collision col)
    {
        if (is_carried == false)
        {
            grounded = false;
        }
    }


    // Use this for initialization
    void Start()
    {
        ui_use = new GameObject();
        render = ui_use.AddComponent<SpriteRenderer>();
        render.sprite = display as Sprite;
        render.flipX = true;
        ui_use.transform.localScale = new Vector3(0.07F, 0.07F, 1F);
        ui_use.transform.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        render.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        ui_use.transform.localPosition = new Vector3(transform.position.x, transform.position.y + 0.75F, transform.position.z);
        ui_use.transform.rotation = Quaternion.Slerp(ui_use.transform.rotation, Quaternion.LookRotation(player.position - ui_use.transform.position), rotation_speed * Time.deltaTime);
        if (dist <= pickup_dist && is_carried == false)
        {
            can_pickup = true;
            render.enabled = true;
        }
        else
        {
            can_pickup = false;
            render.enabled = false;
        }
        if (can_pickup && Input.GetButtonDown("Use"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = player_cam;
            is_carried = true;
        }

        if (is_carried)
        {
            if (collide)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                is_carried = false;
                collide = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && is_carried)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            is_carried = false;
            grounded = false;
            GetComponent<Rigidbody>().AddForce(player_cam.forward * throw_force);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            is_carried = false;
        }
        if (!grounded)
        {
            air_time++;
        }


    }

}