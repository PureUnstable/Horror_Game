using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour {

    public Transform playerTransform;
    public bool forward, backward, left, right;
    private float translation, strafe;
    private Animation a;
    public float strafeSpeed, transSpeed, jumpSpeed;
    public bool isJumping, isGrounded;
    public Rigidbody rb;
    private Animator p;
    int moving = Animator.StringToHash("w_key");

	// Use this for initialization
	void Start ()
    {
        isJumping = false;
        a = GetComponentInChildren<Animation>();
        p = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
      
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            translation = Input.GetAxis("Translate") * transSpeed * Time.deltaTime;
            strafe = Input.GetAxis("Strafe") * strafeSpeed * Time.deltaTime;
            transform.Translate(strafe, 0, translation);
        }
    }
  /*  
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //a.Play("walk");
            p.SetBool(moving, true);
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            p.SetBool(moving, false);
        }

    }
    */
    public void Jump()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Debug.Log("Trying to jump");
            rb.AddForce(Vector3.up * jumpSpeed);
            isJumping = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "environment")
        {
            isJumping = false;
            isGrounded = true;
        }
    }
}
