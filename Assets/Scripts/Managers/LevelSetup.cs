using UnityEngine;
using System.Collections;

public class LevelSetup : MonoBehaviour {

    public int maxLevel = 20;

    public int currentLevel = 1;

    public int lastLevelSetup = 0;

    private bool killed = false;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("LevelSetup").Length > 1)
        {
            killed = true;
            GameObject.Destroy(gameObject);
            return;
        }
        Debug.Log("LEVEL SETUP - AWAKE");
    }

    void Start()
    {
        Debug.Log("LEVEL SETUP - START");
        GameObject.DontDestroyOnLoad(gameObject);
        SetupLevel();
    }

    void OnLevelWasLoaded(int level)
    {
        if (killed) return;
        if (currentLevel < maxLevel)
        {
            currentLevel += 1;
        }
        SetupLevel();
    }
   
    void SetupLevel () {
        if (killed) return;
        if (lastLevelSetup == currentLevel) return;

        Debug.Log("SETTING UP THE LEVEL " + currentLevel);

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;

            Level level = g.GetComponent<Level>();
            if (level == null) continue;

            g.SetActive(level.isForLevel(currentLevel));
        }

        lastLevelSetup = currentLevel;
    }

}
