using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    SpriteRenderer sRender;
    public bool changeColour = false;
    float flashLength = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeColour)
            colourFlash();
    }

    void colourFlash()
    {
        flashLength -= Time.deltaTime;
        sRender.color = Color.red;
        if (flashLength <= 0)
        {
            sRender.color = Color.white;
            flashLength = 0.2f;
            changeColour = false;
        }
    }
}
