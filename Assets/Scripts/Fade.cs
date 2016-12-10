using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    // Use this for initialization
    SpriteRenderer r;
    public float alp;
    void Start () {
        r = GetComponent<SpriteRenderer>();
        
	}
	
	// Update is called once per frame
	void Update () {
	     if( r.color.a > 0)
        {
            r.color = new Color(1f, 1f, 1f, r.color.a - Time.deltaTime/2);
            alp = r.color.a;
        }
        else
        {
            this.enabled = false;
        }
	}
}
