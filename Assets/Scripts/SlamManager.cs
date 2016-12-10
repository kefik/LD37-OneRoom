using UnityEngine;
using System.Collections;

public class SlamManager : MonoBehaviour {

    public float movedTreshold = 8;

    public float moveSpeedMulti = 1.1f;

    public float moveSpeedCap = 5;

    public GameObject wallLeft;
    public GameObject wallRight;
    public GameObject wallFront;
    public GameObject wallBack;

    public float moved = 0;

    private WallMover moverLeft;
    private WallMover moverRight;
    private WallMover moverFront;
    private WallMover moverBack;    

    // Use this for initialization
    void Start () {
        moverLeft = wallLeft.GetComponent<WallMover>();
        moverRight = wallRight.GetComponent<WallMover>();
        moverFront = wallFront.GetComponent<WallMover>();
        moverBack = wallBack.GetComponent<WallMover>();
    }
	
	// Update is called once per frame
	void Update () {
        moved = moverLeft.moved + moverRight.moved;
        if (moved > movedTreshold)
        {
            Slam();
        }
        if (moved < moverFront.moved + moverBack.moved)
        {
            moved = moverFront.moved + moverBack.moved;
            if (moverFront.moved + moverBack.moved > movedTreshold)
            {
                Slam();
            }
        }
    }

    void Slam()
    {
        IncreaseSpeed(moverLeft);
        IncreaseSpeed(moverRight);
        IncreaseSpeed(moverFront);
        IncreaseSpeed(moverBack);        
    }

    void IncreaseSpeed(WallMover mover)
    {
        mover.forceMove = true;
        mover.moveSpeed *= 1 + ((moveSpeedMulti-1) + Time.deltaTime);
        if (mover.moveSpeed > moveSpeedCap) mover.moveSpeed = moveSpeedCap;
    }
}
