using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEffects : MonoBehaviour
{
    Renderer rend;
    float i;
    public float rate = 1.5f;
    public Color colourStart, colourEnd;


    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        // Blend towards the current target colour
        i += Time.deltaTime * rate;
        // Animate the Shininess value
        Color myColor = Color.Lerp(colourStart, colourEnd, Mathf.PingPong(i * 2, 1));

        // If we've got to the current target colour, choose a new one
        if (i >= 1)
        {
            i = 0;
        }


        rend.material.SetColor("_SolidOutline", myColor);
    }
}
