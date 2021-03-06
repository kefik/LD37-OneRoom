﻿using UnityEngine;
using System.Collections;

public class LampaEffects : MonoBehaviour {

    private Light llight;
    private bool scaryTime = false;
    private bool scaryTimeIncrease = false;

    // Use this for initialization
    void Start ()
    {
        llight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(scaryTime)
        {
            float delta = Time.deltaTime * Random.Range(0, 255) / 50;
            if (scaryTimeIncrease)
            {
                llight.intensity = llight.intensity + delta;
            }else
            {
                llight.intensity = llight.intensity - delta;
            }
            llight.intensity = Mathf.Clamp(llight.intensity, 0, 12);
            if( Random.Range(0, 255) > 220f)
            {
                scaryTimeIncrease = !scaryTimeIncrease;
            }
        }
	}

    public void Scarytime()
    {
        scaryTime = true;
    }
}
