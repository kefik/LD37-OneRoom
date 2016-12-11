using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

    public int[] forLevels;

    public bool isForLevel(int level)
    {
        foreach (int forLevel in forLevels)
        {
            if (forLevel == level) return true;
        }
        return false;
    }
	
}
