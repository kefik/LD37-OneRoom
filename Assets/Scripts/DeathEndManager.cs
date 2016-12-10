using UnityEngine;
using System.Collections;

public class DeathEndManager : MonoBehaviour {

	public void End()
    {
        GameObject.Find("Main Camera").GetComponent<CamMover>().enabled = false;
        GameObject.Find("Trigger Splash").GetComponent<TriggerSplash>().Splash();
        GameObject.Find("WallRight").GetComponent<WallMover>().enabled = false;
        GameObject.Find("WallLeft").GetComponent<WallMover>().enabled = false;
        GameObject.Find("WallFront").GetComponent<WallMover>().enabled = false;
        GameObject.Find("WallBack").GetComponent<WallMover>().enabled = false;

    }
}
