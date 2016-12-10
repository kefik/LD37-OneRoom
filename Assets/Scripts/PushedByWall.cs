using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class PushedByWall : MonoBehaviour {

    public GameObject toBePushed;

    private SphereCollider collider;

    void Start()
    {
        if (toBePushed == null) toBePushed = gameObject;
        collider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        Move(other);
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay");
        Move(other);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
    }

    void Move(Collider other)
    {
        Debug.Log(toBePushed.name + " POSITION: " + toBePushed.transform.position);
        Debug.Log(other.gameObject + " POSITION: " + other.gameObject.transform.position);

        Vector3 movement = toBePushed.transform.position - other.gameObject.transform.position;

        Debug.Log("MAGNITUDE: " + movement.magnitude);

        movement = (movement.magnitude - collider.radius) * movement.normalized;

        Debug.Log("MAGNITUDE UPDATED: " + movement.magnitude);

        Debug.Log("MOVEMENT: " + movement);

        toBePushed.transform.localPosition += movement;
    }

}
