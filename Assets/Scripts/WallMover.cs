﻿using UnityEngine;
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

    public Renderer m_renderer;

    public float currMovingDelay;

    public float moved;

    public float targetPosition;

    public bool isOver;

    private ParticleSystem particles;

    public AudioSource wallSound;

    public AudioSource clickSound;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        m_renderer = GetComponent<Renderer>();
        currMovingDelay = movingDelay;
        particles = GetComponentsInChildren<ParticleSystem>()[0];
        moved = 0;
    }
	
    // Update is called once per frame
    void Update () {
        moving = !IsVisibleFrom(m_renderer, mainCamera);

        

        if (moving || forceMove)
        {
            if (currMovingDelay > 0 && !forceMove)
            {
                currMovingDelay -= Time.deltaTime;
            } else {
                gameObject.transform.localPosition += moveDirection * moveSpeed * Time.deltaTime;
                moved += moveSpeed * Time.deltaTime;
                if (!isOver && moved > targetPosition)
                {
                    clickSound.Play();
                    isOver = true;
                }
            }
            particles.Play();
            if (!wallSound.isPlaying)
            {
                wallSound.Play();
            }
        } else
        {
            particles.Stop();
            wallSound.Stop();
            currMovingDelay = movingDelay + (float)r.NextDouble() * movingDelayRandom;
        }
	}

    public bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}