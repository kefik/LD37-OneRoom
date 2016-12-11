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

    public Renderer m_renderer;

    public float currMovingDelay;

    public float moved;

    public float targetPosition;

    public bool isGood;
    public bool isOver;

    private ParticleSystem particles;

    public AudioSource wallSound;

    public AudioSource clickSound;

    public AudioSource wallDifSound;

    public float overLimit = 0.5f;

    public bool justfmove;

    public int numberInSequence;

    public float targetSpeed = 0.2f;

    public bool firstPlay = true;

    private GameScript manager;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        m_renderer = GetComponent<Renderer>();
        currMovingDelay = movingDelay;
        particles = GetComponentsInChildren<ParticleSystem>()[0];
        manager = GameObject.Find("GameManager").GetComponent<GameScript>();
        moved = 0;
    }

    void OnDisable()
    {
        if (particles) {
            particles.Stop();
        }
    }
	
    // Update is called once per frame
    void Update () {
        moving = justfmove || !IsVisibleFrom(m_renderer, mainCamera);

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
                    if (manager.NumGoodWalls() == numberInSequence)
                    {
                        if (firstPlay) {
                            GameObject.Find("Phaser").GetComponent<Phaser>().StartPhase("Hint");
                        }
                        print("CLING CLING");
                        clickSound.Play();
                    }else{

                    }
                    isOver = true;
                    isGood = true;
                    moveSpeed = 0.018f;
                }

                if( moveSpeed < targetSpeed)
                {
                    moveSpeed += 0.1f * Time.deltaTime;
                    moveSpeed = Mathf.Min(moveSpeed, targetSpeed);
                }

                if( isGood && moved > targetPosition + overLimit)
                {
                    isGood = false;
                    manager.SequenceMistake();
                }
                
            }

            particles.Play();
            if (currMovingDelay < 0 || forceMove)
            {
                if ((!isOver || isGood) && !wallSound.isPlaying)
                {
                    wallSound.Play();
                }

                if ((isOver && !isGood) && !wallDifSound.isPlaying)
                {
                    wallDifSound.Play();
                }
            } else
            {
                particles.Stop();
                wallSound.Stop();
                wallDifSound.Stop();
            }
        } else
        {
            particles.Stop();
            wallSound.Stop();
            wallDifSound.Stop();
            currMovingDelay = movingDelay + (float)r.NextDouble() * movingDelayRandom;
        }
	}

    public bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
