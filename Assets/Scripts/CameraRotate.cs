using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    public int degrees = 10;

    public Transform target;

    public bool freeze = false;

    public bool invertPitch = true;
    public bool invertYaw = false;

    public float pitch;
    public float yaw;

    public float minPitch = -50;
    public float maxPitch = 90;

    void Start()
    {
        if (target == null)
        {
            target = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze) return;
        if (Input.GetMouseButton(0))
        {
            pitch += (invertPitch ? -1 : 1) * Input.GetAxis("Mouse Y") * degrees;
            yaw += (invertYaw ? -1 : 1) * Input.GetAxis("Mouse X") * degrees;

            while (pitch < -180) pitch += 360;
            while (pitch > 180) pitch -= 360;
            if (pitch < minPitch) pitch = minPitch;
            if (pitch > maxPitch) pitch = maxPitch;
            while (yaw < -180) yaw += 360;
            while (yaw > 180) yaw -= 360;

            Quaternion q = Quaternion.Euler(new Vector3(pitch, yaw, 0));

            transform.rotation = q;

            

        }

        /*
        if (!Input.GetMouseButton(0))
            transform.RotateAround(target.position, Vector3.up, degrees * Time.deltaTime);
        }
        */
    }

}
