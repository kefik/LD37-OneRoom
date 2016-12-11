using UnityEngine;
using System.Collections;


[RequireComponent(typeof(TriggerSplash))]
public class CamMover : MonoBehaviour
{
    private TriggerSplash splash;
    private Collider[] walls;
    public Collider coll;
    public Transform look;
    void Start()
    {
        splash = GetComponent<TriggerSplash>();

        walls = new Collider[4];
        walls[0] = GameObject.Find("WallRight").GetComponent<Collider>();
        walls[1] = GameObject.Find("WallLeft").GetComponent<Collider>();
        walls[2] = GameObject.Find("WallFront").GetComponent<Collider>();
        walls[3] = GameObject.Find("WallBack").GetComponent<Collider>();
    }

    Vector2 GetInput()
    {
        Vector2 input = new Vector2
        {
            x = Input.GetAxis("Sideways"),
            y = Input.GetAxis("Forward")
        };
        //movementSettings.UpdateDesiredTargetSpeed(input);
        return input;
    }

    void Update()
    {

        Vector2 input = GetInput();

        if ( Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon )
        {
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = look.transform.forward * input.y + look.transform.right * input.x;
            //desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

            desiredMove.x = desiredMove.x * 1;
            desiredMove.z = desiredMove.z * 1;
            desiredMove.y = 0;
            if (coll.attachedRigidbody.velocity.sqrMagnitude <1*1)
            {
                coll.attachedRigidbody.velocity *= 0.8f;
                coll.transform.position += desiredMove/25;
               // coll.attachedRigidbody.AddForce(desiredMove, ForceMode.Impulse);
            }
        }
        bool dead = false;
        
        for(int i = 0; i < 4; i++)
        {
            if( coll.bounds.Intersects(walls[i].bounds) )
            {
                coll.attachedRigidbody.AddForce(walls[i].GetComponent<WallMover>().moveDirection/5.0f, ForceMode.Impulse);
            }
        }

        if( coll.bounds.Intersects(walls[0].bounds) && coll.bounds.Intersects(walls[1].bounds))
        {
            dead = true;
        }else if(coll.bounds.Intersects(walls[2].bounds) && coll.bounds.Intersects(walls[3].bounds))
        {
            dead = true;
        }
        

        if( dead )
        {
            GameObject.Find("DeathEndManager").GetComponent<DeathEndManager>().End();
        }
    }



}
