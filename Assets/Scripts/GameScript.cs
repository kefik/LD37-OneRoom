﻿using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

    public static int numGames = 0;

    private Vector3[] positions;
    private GameObject[] walls;
    private bool won;
    public ParticleSystem endSystem;
    public AudioSource heavenSound;
    private bool hasMistake;
    // Use this for initialization
    void Start()
    {

        positions = new Vector3[4];
        walls = new GameObject[4];

        walls[0] = GameObject.Find("WallRight");
        walls[1] = GameObject.Find("WallLeft");
        walls[2] = GameObject.Find("WallFront");
        walls[3] = GameObject.Find("WallBack");

        for (int i = 0; i < 4; i++)
        {
            positions[i] = walls[i].transform.position;
            walls[i].GetComponent<WallMover>().enabled = false;
        }

        StartCoroutine(SlowStart());

        GameObject[] level = GameObject.FindGameObjectsWithTag("Level" + (numGames + 1));
        for (int i = 0; i < level.Length; i++)
        {
            SpriteRenderer r = level[i].GetComponent<SpriteRenderer>();
            if( r )
            {
                r.color = new Color(1, 1, 1, 1);
            }
        }
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

    public void SequenceMistake()
    {
        hasMistake = true;
    }

	
	// Update is called once per frame
	void Update () {

        bool allgood = !hasMistake;
        for (int i = 0; i < 4; i++)
        {
            if( !walls[i].GetComponent<WallMover>().isGood )
            {
                allgood = false;
            }
        }
        if( allgood && !won )
        {
            won = true;
            GoodEnding();
        }
    }

    public int NumGoodWalls()
    {
        int ngood = 0;
        for (int i = 0; i < 4; i++)
        {
            if (walls[i].GetComponent<WallMover>().isGood)
            {
                ngood++;
            }
        }
        return ngood;
    }

    void GoodEnding()
    {
        print("YOU MADE IT");
        endSystem.Play();
        heavenSound.Play();
        for (int i = 0; i < 4; i++)
        {
            if (walls[i].GetComponent<WallMover>().isGood == true)
            {
                walls[i].GetComponent<WallMover>().enabled = false;
                AudioSource ac = walls[i].GetComponent<AudioSource>();
                StartCoroutine(FadeOut(ac));
                StartCoroutine(MoveOut(walls[i], walls[i].GetComponent<WallMover>().moveDirection));
            }
        }
        StartCoroutine(MoveOut(GameObject.Find("Ceiling"), new Vector3(0, -1, 0)));
        StartCoroutine(MoveOut(GameObject.Find("Floor"), new Vector3(0, 0.005f, 0)));

    }

    IEnumerator MoveOut(GameObject wall, Vector3 dir)
    {
        float elapsed = 0;
        while (true)
        {
            wall.transform.localPosition -= dir  * Time.deltaTime * (1+elapsed/3);
            elapsed += Time.deltaTime;
            if (elapsed > 2)
                break;
            yield return null;
        }
        while (true)
        {
            wall.transform.localPosition -= dir * Time.deltaTime * (1 + elapsed*20);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut(AudioSource sound)
    {
        float startVolume = sound.volume;

        while (sound.volume > 0)
        {
            sound.volume -= startVolume * Time.deltaTime / 1;

            yield return null;
        }

        sound.Stop();
        sound.volume = startVolume;
    }
}
