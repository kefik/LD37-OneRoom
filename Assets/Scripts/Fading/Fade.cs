using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class Fade : MonoBehaviour
{

    // Use this for initialization
    private Image image;

    private Color source;

    public Color target = new Color(0, 0, 0, 1);

    public float time = 1;

    public bool active = false;

    private float currTime;

    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        currTime -= Time.deltaTime;

        if (currTime < 0)
        {
            image.color = target;
            Deactivate();
            return;
        }

        image.color = new Color(Tween(image.color.r, target.r), Tween(image.color.g, target.g), Tween(image.color.b, target.b), Tween(image.color.a, target.a));
    }

    private bool IsZero(float one, float two)
    {
        return Mathf.Abs(one - two) < 0.01;
    }

    private float Tween(float curr, float target)
    {
        float diff = target - curr;

        float c = Mathf.Sin((time - currTime) / time);

        return curr + diff * c;
    }

    public void Activate()
    {
        active = true;
        currTime = time;
        source = image.color;
    }

    public void Deactivate()
    {
        active = false;
    }

}
