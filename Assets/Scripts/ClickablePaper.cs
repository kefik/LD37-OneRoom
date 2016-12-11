using UnityEngine;
using System.Collections;

public class ClickablePaper : MonoBehaviour {

    public Transform unrolledPaper;

    // Use this for initialization
	void Start () {
	
	}

    void OnMouseDown()
    {
        print("clicked");
        Instantiate(unrolledPaper, this.transform.parent.position, Quaternion.identity);
        
        // this object was clicked - do something
        Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
