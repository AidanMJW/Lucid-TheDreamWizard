using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public List<ScrollImage> scrollImages = new List<ScrollImage>();
    GameObject cameraRig;
    Rigidbody2D playerRigBody;

    Vector3 v1;
    Vector3 v2;

    float test;

    void Start()
    {
        cameraRig = GameObject.FindGameObjectWithTag("CamRig");

        v1 = cameraRig.transform.position;
        v2 = cameraRig.transform.position;
    }


    void Update()
    {
        test = (getPlayerHorizontalMagnitude());

        if (scrollImages.Count > 0)
        {
            for (int i = 0; i < scrollImages.Count; i++)
            {
                shuffleImage(scrollImages[i]);
                followCamera(scrollImages[i]);
            }
        }

        
    }


    void followCamera(ScrollImage s)
    {
        s.imageHolder.transform.position = new Vector3(s.imageHolder.transform.position.x + test * s.scrollSpeed, s.imageHolder.transform.position.y, s.imageHolder.transform.position.z);
    }

    float  getPlayerHorizontalMagnitude()
    {
        v1 = cameraRig.transform.position;

        float t = v1.x - v2.x;
        v2 = cameraRig.transform.position;

        return t;
    }

    void shuffleImage(ScrollImage s)
    {
        Vector3 CameraPos = new Vector3(cameraRig.transform.position.x,0,0);
        Vector3 rightImage = new Vector3(s.images[2].transform.position.x, 0, 0);
        Vector3 leftImage = new Vector3(s.images[0].transform.position.x, 0, 0);

        if (Vector3.Distance(CameraPos, rightImage) < 1)
            shuffleRight(s);

        if (Vector3.Distance(CameraPos, leftImage) < 1)
            shuffleLeft(s);

    }

    void shuffleLeft(ScrollImage s)
    {
        GameObject[] shuffle = new GameObject[3] { s.images[2], s.images[0], s.images[1] };
        s.images = shuffle;

        Vector3 newPos = shuffle[1].transform.position;
        newPos.x -= s.imageSize;
        s.images[0].transform.position = newPos;
    }

    void shuffleRight(ScrollImage s)
    {
        GameObject[] shuffle = new GameObject[3] { s.images[1], s.images[2], s.images[0] };
        s.images = shuffle;

        Vector3 newPos = s.images[1].transform.position;
        newPos.x += s.imageSize;
        s.images[2].transform.position = newPos;
    }
}

[System.Serializable]
public class ScrollImage
{
    public GameObject imageHolder;
    public GameObject[] images = new GameObject[3];

    public float imageSize;
    public float scrollSpeed;

    

    
}
