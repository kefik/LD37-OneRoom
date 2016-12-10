using UnityEngine;
using System.Collections;

public class WallMover : MonoBehaviour {

    private static System.Random r = new System.Random();

    public bool moving = false;

    public bool forceMove = false;

    public float movingDelay = 0.3f;

    public float movingDelayRandom = 0.3f;

    public Vector3 moveDirection;

    public float moveSpeed = 0.2f;

    public Camera mainCamera;

    public Renderer renderer;

    public float currMovingDelay;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        renderer = GetComponent<Renderer>();
        currMovingDelay = movingDelay;
    }
	
	// Update is called once per frame
	void Update () {
        moving = !IsVisibleFrom(renderer, mainCamera);
        if (moving || forceMove)
        {
            if (currMovingDelay > 0 && !forceMove)
            {
                currMovingDelay -= Time.deltaTime;
            } else { 
                gameObject.transform.localPosition += moveDirection * moveSpeed * Time.deltaTime;
            }
        } else
        {
            currMovingDelay = movingDelay + (float)r.NextDouble() * movingDelayRandom;
        }
	}

    public bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
