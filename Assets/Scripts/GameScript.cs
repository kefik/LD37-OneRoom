using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

    public static int numGames = 0;

    private Vector3[] positions;
    private GameObject[] walls;
    // Use this for initialization
    void Start () {

        positions = new Vector3[4];
        walls = new GameObject[4];

        walls[0] = GameObject.Find("WallRight");
        walls[1] = GameObject.Find("WallLeft");
        walls[2] = GameObject.Find("WallFront");
        walls[3] = GameObject.Find("WallBack");

        for (int i = 0; i < 4; i++) {
            positions[i] = walls[i].transform.position;
            walls[i].GetComponent<WallMover>().enabled = false;
        }

        StartCoroutine(SlowStart());
    }

    IEnumerator SlowStart()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 4; i++)
        {
            walls[i].GetComponent<WallMover>().enabled = true;
        }
    }

    public void StartGame()
    {
        numGames++;
        print(numGames);
        Application.LoadLevel(Application.loadedLevel);
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
