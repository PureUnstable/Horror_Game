using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public InputController inputController;

    public PlayerWalk pw;
    public Flashlight fl;
    public MouseLook ml;
    public InventoryUI iui;
	public AudioManager am;
	public LevelManager lm;
    public GameObject canvas;
	public GameObject optionsMenu;
	public GameObject hud;
    public bool isPaused;
    bool isCursorLocked;
    private Interactable interactable, focus;
    private bool interact;
    private GameObject coll;
    private bool iscrouched;
    private Vector3 defaultpos;
    void Awake()
    {
		if (inputController != null)
			Destroy (this);
		else
			inputController = this;
        isPaused = false;
        isCursorLocked = true;
        ml.lockCursor = true;
        iscrouched = false;
        Cursor.lockState = CursorLockMode.Locked;
        defaultpos = gameObject.transform.GetChild(0).transform.position;
       // iui.inventoryUI.SetActive(true);

    }

	// Update is called once per frame
	void Update ()
    {
       /* 
        if (Input.GetKeyDown(KeyCode.C) && iscrouched == false)
        {
            iscrouched = true;
            gameObject.transform.GetChild(0).transform.position = new Vector3(gameObject.transform.GetChild(0).transform.position.x, gameObject.transform.GetChild(0).transform.position.y - 1, gameObject.transform.GetChild(0).transform.position.z);
        }
        else if(Input.GetKeyDown(KeyCode.C) && iscrouched == true)
        {
            iscrouched = false;
            gameObject.transform.GetChild(0).transform.position = new Vector3(gameObject.transform.GetChild(0).transform.position.x, gameObject.transform.GetChild(0).transform.position.y + 1, gameObject.transform.GetChild(0).transform.position.z); ;
        }
        */
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

        if (Input.GetButtonDown("Charge"))
        {
            fl.ChargeLight();
        }

		if (Input.GetButtonDown("Pause") && !iui.inventoryUI.activeSelf)
        {
            if (!isCursorLocked) // Game is paused
            {
                /*if (!iui.inventoryUI.activeSelf)
                {
                    
                }*/
				Cursor.lockState = CursorLockMode.Locked;
				isCursorLocked = true;
				ml.lockCursor = true;
				Debug.Log("Cursor locked");
				//iui.inventoryUI.SetActive (true);
				optionsMenu.SetActive (false);
				hud.SetActive (true);
            }
            else // Game is not paused
            {
                Cursor.lockState = CursorLockMode.None;
                isCursorLocked = false;
                ml.lockCursor = false;
                Debug.Log("Cursor unlocked");
				//iui.inventoryUI.SetActive (false);
				optionsMenu.SetActive (true);
				hud.SetActive (false);
            }
        }

		if (Input.GetKeyDown(KeyCode.Tab) && !optionsMenu.activeSelf)
        {
			if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                isCursorLocked = false;
                ml.lockCursor = false;
                Debug.Log("Cursor unlocked");
                 iui.inventoryUI.SetActive(true);
               // TurnonCanvas();
                hud.SetActive (false);
            }
            else
            {
				/*if (!isCursorLocked)
                {*/
                    Cursor.lockState = CursorLockMode.Locked;
                    isCursorLocked = true;
                    ml.lockCursor = true;
                    Debug.Log("Cursor locked");
                   iui.inventoryUI.SetActive(false);
               // ClearCanvas();
				hud.SetActive (true);
                //}
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (pw.isGrounded && !pw.isJumping)
                pw.Jump();
        }
    }
    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }

    void ClearCanvas()
    {
        CanvasRenderer[] canvasr = canvas.GetComponentsInChildren<CanvasRenderer>();
        foreach(CanvasRenderer c in canvasr)
        {
            c.SetAlpha(0);
        }
    }

    void TurnonCanvas()
    {
        CanvasRenderer[] canvasr = canvas.GetComponentsInChildren<CanvasRenderer>();
        foreach (CanvasRenderer c in canvasr)
        {
            c.SetAlpha(128);
        }
    }

}
