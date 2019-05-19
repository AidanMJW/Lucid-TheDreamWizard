using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollImageDown : MonoBehaviour
{
    public GameObject cameraRig;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    public float size;
    public float speed;

    Vector3 v1;
    Vector3 v2;

    void Start()
    {
        v2 = cameraRig.transform.position;
    }

    void Update()
    {

        image1.transform.localPosition = new Vector3(0, (image1.transform.localPosition.y - speed  * Time.deltaTime ) - getPlayerVerticleMagnitude(), 1) ;
        image2.transform.localPosition = new Vector3(0, (image2.transform.localPosition.y - speed * Time.deltaTime) - getPlayerVerticleMagnitude(), 1);
        image3.transform.localPosition = new Vector3(0, (image3.transform.localPosition.y - speed * Time.deltaTime) - getPlayerVerticleMagnitude(), 1);
        
        if (image1.transform.localPosition.y <= size * -2)
            image1.transform.localPosition = new Vector3(0, size, 0);

        if (image2.transform.localPosition.y <= size * -2)
            image2.transform.localPosition = new Vector3(0, size, 0);

        if (image3.transform.localPosition.y <= size * -2)
            image3.transform.localPosition = new Vector3(0, size, 0);

    
    }

    float getPlayerVerticleMagnitude()
    {
        v1 = cameraRig.transform.position;

        float t = v1.y - v2.y;
        t = t * Time.deltaTime;
        v2 = cameraRig.transform.position;

        return t;
    }
}
