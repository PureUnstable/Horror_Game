using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class object_physics : MonoBehaviour
{

    private Transform player;
    private Transform player_cam;
    public float throw_force = 10;
    public float pickup_dist = 2.5F;
    public float rotation_speed = 3;
    private bool can_pickup;
    private bool in_pickup_zone;
    private bool is_carried;
    private bool grounded;
    private int air_time = 0;
    private float max_height;
    public Sprite display;
    private GameObject ui_use;
    private SpriteRenderer render;
    private bool collide;
    private Transform last_hit;
    private bool has_hit;
    private Collider[] alert;
    private Collider[] detect;
    private bool message_sent;
    private bool disable_sent;
    private bool stop;

    void OnTriggerEnter(Collider other)
    {
        if (is_carried && other.gameObject.tag == "enviroment")
        {
            collide = true;

        }
    }

    void OnCollisionStay(Collision col)
    {
        if (is_carried == false && col.gameObject.tag == "environment")
        {
            grounded = true;
            has_hit = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        detect = Physics.OverlapSphere(gameObject.transform.position, (air_time * 0.1F) * (max_height * 0.25F));
        alert = Physics.OverlapSphere(gameObject.transform.position, (air_time * 0.1F) * (max_height * 0.675F));
        DebugExtension.DebugWireSphere(gameObject.transform.position, (air_time * 0.1F) * (max_height * 0.25F), 5); // will be detected
        DebugExtension.DebugWireSphere(gameObject.transform.position, (air_time * 0.1F) * (max_height * 0.675F), 5); // will attract to
        max_height = 0;
        air_time = 0;
        last_hit = col.gameObject.transform;
        if(col.gameObject.tag == "environment")
        {
            collide = true;
        }
    }



    void OnCollisionExit(Collision col)
    {
        if (is_carried == false)
        {
            grounded = false;
            message_sent = false;

        }
    }


    // Use this for initialization
    void Start()
    {
        player = (Transform)GameObject.FindGameObjectWithTag("Player").transform;
        player_cam = Camera.main.gameObject.transform;
        ui_use = new GameObject();
        render = ui_use.AddComponent<SpriteRenderer>();
        render.sprite = display as Sprite;
        render.flipX = true;
        ui_use.transform.localScale = new Vector3(0.07F, 0.07F, 1F);
        ui_use.transform.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        last_hit = null;
        render.enabled = false;
        message_sent = false;
        can_pickup = false;
        in_pickup_zone = false;
        is_carried = false;
        grounded = false;
        collide = false;
        disable_sent = false;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        ui_use.transform.localPosition = new Vector3(transform.position.x, transform.position.y + 0.25F, transform.position.z);
        ui_use.transform.rotation = Quaternion.Slerp(ui_use.transform.rotation, Quaternion.LookRotation(player.position - ui_use.transform.position), rotation_speed * Time.deltaTime);

        collide = false;

        if (alert != null && message_sent == false)
        {
            for (int i = 0; i < alert.Length; i++)
            {
                if (alert[i].tag == "Enemy")
                {
                    alert[i].SendMessage("Alert", null, SendMessageOptions.DontRequireReceiver);

                }
                //print (alert [i]);
            }
            message_sent = true;
        }

        if (last_hit != null && has_hit != false)
        {
            float temp = Mathf.Floor(gameObject.transform.position.y - last_hit.position.y);
            if (max_height < temp)
            {
                max_height = temp;
            }
            //print (max_height);
        }
        if (!stop)
        {
            if (dist <= pickup_dist && (is_carried == false) && in_pickup_zone)
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
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2F, Screen.height / 2F, Camera.main.nearClipPlane + 1));
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = player_cam;
                is_carried = true;
            }
        }
        if (is_carried)
        {
            can_pickup = false;
            if (!disable_sent)
            {
                sendMessage(true);
                disable_sent = true;
            }
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2F, Screen.height / 2F, Camera.main.nearClipPlane + 1));
            if (collide)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                is_carried = false;
                collide = false;
                disable_sent = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && is_carried)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            is_carried = false;
            disable_sent = false;
            grounded = false;
            sendMessage(false);
            GetComponent<Rigidbody>().AddForce(player_cam.forward * throw_force);
        }
        else if (Input.GetMouseButtonDown(1) && is_carried)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            is_carried = false;
            disable_sent = false;
            sendMessage(false);
        }
        if (!grounded)
        {
            air_time++;
        }

    }

    void sendMessage(bool send)
    {
        GameObject[] sendlist = GameObject.FindGameObjectsWithTag("moveable");
        for (int i = 0; i < sendlist.Length; i++)
        {
            sendlist[i].SendMessage("DisablePickup", send, SendMessageOptions.DontRequireReceiver);
        }
    }

    void DisablePickup(bool d)
    {
        stop = d;
        render.enabled = !d;
        print(stop + gameObject.name);
    }

    void CanPickup(bool setpick)
    {
        in_pickup_zone = setpick;
    }

    void Deactivate()
    {
        Destroy(ui_use);
    }

}
