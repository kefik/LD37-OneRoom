﻿using UnityEngine;
using System.Collections;

public class DeathEndManager : MonoBehaviour {

    public void End()
    {
        StartCoroutine(EndTheGame());
    }

    IEnumerator EndTheGame()
    {
        GameObject.Find("Main Camera").GetComponent<CamMover>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<CameraRotate>().freeze = true;
        GameObject.Find("Main Camera").GetComponent<TriggerSplash>().Splash();

        GameObject[] walls = new GameObject[4];
        walls[0] = GameObject.Find("WallRight");
        walls[1] = GameObject.Find("WallLeft");
        walls[2] = GameObject.Find("WallFront");
        walls[3] = GameObject.Find("WallBack");

        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<WallMover>().enabled = false;
            AudioSource ac = walls[i].GetComponent<AudioSource>();
            StartCoroutine(FadeOut(ac));
        }

        AudioSource s = GetComponent<AudioSource>();
        s.Play();

        yield return new WaitForSeconds(1);

        GameObject.Find("Phaser").GetComponent<Phaser>().StartPhase("Ending");

        yield return GameObject.Find("Phaser").GetComponent<Phaser>().WaitPhaseEnd();

        GameObject.Find("GameManager").GetComponent<GameScript>().StartGame();
    }
    
    IEnumerator FadeOut(AudioSource wallSound)
    {
        float startVolume = wallSound.volume;

        while (wallSound.volume > 0)
        {
            wallSound.volume -= startVolume * Time.deltaTime / 1;

            yield return null;
        }

        wallSound.Stop();
        wallSound.volume = startVolume;
    }

}
