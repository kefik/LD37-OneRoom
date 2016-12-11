using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class Fade : MonoBehaviour
{

    // Use this for initialization
    private Image image;

    public Color target = new Color(0, 0, 0, 1);

    public float speed = 1;

    public bool active = false;

    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        if (IsZero(target.r, image.color.r) && IsZero(target.g, image.color.g) && IsZero(target.b, image.color.b) && IsZero(target.a, image.color.a))
        {
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
        float max = target - curr;
        float change = Mathf.Sign(target - curr) * Time.deltaTime * speed;

        if (curr >= target)
        {
            change = Mathf.Max(max, change);
        }
        else
        {
            change = Mathf.Min(max, change);
        }

        return curr + change;
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

}
