using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour {

    public Light spotlight;
    public Light ambient;
    public float intensity;
    public float ambientIntensity;
    public bool isCharging;
	public Image[] chargeIcons;

	float fullPos, emptyPos;
	float top, bottol, left;
	int lastCharge;

    void Awake()
    {
        intensity = spotlight.intensity;
		lastCharge = 0;
    }

	void Start () {
        isCharging = false;
	}
	
	void Update () {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            intensity = spotlight.intensity;
            ambientIntensity = ambient.intensity;
            // Main light
            // Max intensity is 5
            // Ambient light
            // Max intensity is 2
            if (intensity > 0)
            {
                intensity -= 0.003f; //0.005
                ambientIntensity -= 0.004f;

                //intensity -= 0.02f;
                //ambientIntensity -= 0.008f;
            }
            spotlight.intensity = intensity;
            ambient.intensity = ambientIntensity;
        }			
	}

    // Charges light when pressing the "Charging" button
    // Called by InputController
    public void ChargeLight() // 4.75/0.25 & 4/0.2
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (spotlight.intensity < 2.85f)
            {
                spotlight.intensity += .15f;
            }
            if (ambient.intensity < 3.8f)
            {
                ambient.intensity += .2f;
            }
        }
    }

	public int ChargeUI(float charge)
	{
		float chargePercent = charge;
		if (chargePercent >= 2.25f)
			return 3;
		else if (chargePercent >= 1.5f)
			return 2;
		else if (chargePercent >= 0.75)
			return 1;
		else if (chargePercent > 0)
			return 0;
		else
			return -1;
	}

	void OnGUI()
	{
		int count = ChargeUI (intensity);
		if (count != lastCharge) {
			if (count == -1)
				for (int i = 0; i < 4; i++)
					chargeIcons [i].gameObject.SetActive (false);
			else 
			{
				for (int i = 0; i < count+1; i++)
					chargeIcons [i].gameObject.SetActive (true);
				for (int i = count+1; i < 4; i++)
					chargeIcons [i].gameObject.SetActive (false);
			}
		}

		lastCharge = count;
			
		
	}
}
