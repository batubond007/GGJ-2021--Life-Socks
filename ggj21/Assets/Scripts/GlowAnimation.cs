using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowAnimation : MonoBehaviour
{
    private Color c;
    private float a = .5f;
    private bool goingUp;

    private void Start()
    {
        c = GetComponent<SpriteRenderer>().color;
        goingUp = true;
    }
    private void Update()
    {
        c.a = goingUp ? .01f + c.a : c.a - .01f;

        if (c.a >= 1f) 
            goingUp = false;
        else if (c.a <= 0.1)
            goingUp = true;

        GetComponent<SpriteRenderer>().color = c;
    }
}
