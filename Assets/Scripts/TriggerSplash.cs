using UnityEngine;
using System.Collections;
using System;

public class TriggerSplash : MonoBehaviour {

    public bool splash = false;

    public GameObject[] toTrigger;

    // Update is called once per frame
	void Update () {
        if (splash)
        {
            splash = false;
            splashArray(toTrigger);
        }
	
	}


    public void Splash()
    {
        splash = true;
    }

    private void splashArray(GameObject[] toTrigger)
    {
        foreach (GameObject go in toTrigger)
        {
            ParticleSystem system = go.GetComponent<ParticleSystem>();
            if (system != null)
            {
                system.Play();
            }
        }
    }

    private void splashParticles(GameObject go)
    {
        for (int i = 0; i < go.transform.childCount; ++i)
        {
            GameObject child = go.transform.GetChild(i).gameObject;
            ParticleSystem system = child.GetComponent<ParticleSystem>();
            if (system != null) {
                system.Play();
            }
            if (child.transform.childCount > 0)
            {
                splashParticles(child);
            }
        }
    }
}
