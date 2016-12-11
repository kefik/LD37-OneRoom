using UnityEngine;
using System.Collections;

public class Phase : MonoBehaviour {

    public string[] phases;

    public bool isPhase(string target)
    {
        foreach (string phase in phases)
        {
            if (target == phase) return true;
        }
        return false;
    }

}
