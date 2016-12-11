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
        Transform a = (Transform) Instantiate(unrolledPaper, this.transform.parent.position, Quaternion.identity);
        a.parent = this.transform.parent.parent;
        a.localScale = this.transform.localScale/6;
      //  a.position -= new Vector3(0, 0.3f, 0);
        // this object was clicked - do something
        Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
