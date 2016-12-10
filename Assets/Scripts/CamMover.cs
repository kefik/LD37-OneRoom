using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(TriggerSplash))]
public class CamMover : MonoBehaviour
{
    public int moving = 0;

    public Hashtable movedBy;

    public Vector3 moveDirection;

    public float moveSpeed = 0.2f;

    private SphereCollider collider;

    private TriggerSplash splash;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        movedBy = new Hashtable();
        splash = GetComponent<TriggerSplash>();
    }

    void Update()
    {
        if (moving == 1)
        {
            gameObject.transform.localPosition += moveDirection * moveSpeed * Time.deltaTime;
        } else
        if (moving > 1)
        {
            GameObject.Find("DeathEndManager").GetComponent<DeathEndManager>().End();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (movedBy.ContainsKey(other.gameObject)) return;
        movedBy[other.gameObject] = true;
        ++moving;

        moveDirection = other.gameObject.GetComponent<WallMover>().moveDirection;
        moveSpeed = other.gameObject.GetComponent<WallMover>().moveSpeed;
    }

    void OnTriggerStay(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
        if (!movedBy.ContainsKey(other.gameObject)) return;
        movedBy.Remove(other.gameObject);
        --moving;
    }

}
