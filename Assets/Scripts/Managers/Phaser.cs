using UnityEngine;
using System.Collections;

public class Phaser : MonoBehaviour {

    public bool running = false;

    public string currentPhase;

    private ArrayList fades = new ArrayList();

    public void Update()
    {
        if (!running) return;

        foreach (Fade fade in fades)
        {
            if (fade.active) return;
        }

        Debug.Log("PHASE " + currentPhase + " ENDED");
        running = false;
    }

    public void StartPhase(string targetPhase)
    {
        Debug.Log("STARTING PHASE - " + targetPhase);

        int fadesAdded = 0;

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;

            if (!g.activeInHierarchy) continue;

            Phase phase = g.GetComponent<Phase>();
            if (phase == null) continue;

            if (!phase.isPhase(targetPhase)) continue;

            Fade fade = g.GetComponent<Fade>();
            if (fade == null) continue;

            fade.Activate();
            fades.Add(fade);

            ++fadesAdded;
        }

        if (fadesAdded > 0)
        {
            Debug.Log("PHASE " + targetPhase + " HAS " + fadesAdded + " FADES");
            currentPhase = targetPhase;
            running = true;
        }
    }

    /**
     * USE AS FOLLOWS:
     * yield return GameObject.Find("Phaser").GetComponent<Phaser>().WaitPhaseEnd();
     */
    public IEnumerator WaitPhaseEnd()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (!GameObject.Find("Phaser").GetComponent<Phaser>().running) break;
        }
    }
	
}
